close all; clear variables; clc;
rng(42);

%% Functions that generate random values in the range [-1, 1] * scale
random_matrix = @(rows, cols, scale) (rand(rows, cols) - 0.5) * 2 * scale;
random_row_vector = @(cols, scale) random_matrix(1, cols, scale);
random_col_vector = @(rows, scale) random_matrix(rows, 1, scale);
random_value = @(scale) random_matrix(1,1,scale);

%% Function to generate random quaternions
aux_random_quaternion = @(axis, angle) [sin(angle / 2) * (axis * 1 / norm(axis)); cos(angle / 2)];
random_quaternion = @() aux_random_quaternion(random_col_vector(3, 5), random_value(1) * pi);

%% Rotation Matrix
Ru_aux = @(u, v, w, s)...  
[
    s * s + u * u - v * v - w * w,  2 * (u * v - s * w),            2 * (u * w + s * v),            0;
    2 * (u * v + s * w),            s * s - u * u + v * v - w * w,  2 * (v * w - s * u),            0;
    2 * (u * w - s * v),            2 * (v * w + s * u),            s * s - u * u - v * v + w * w,  0;
    0,                              0,                              0,                              1;
];
Ru = @(q) Ru_aux(q(1), q(2), q(3), q(4));
R = @(q) 1 / dot(q, q) * Ru(q);

%% Premultiplication
Xc = @(x, q) R(q) * x;

%% Skew Matrix
C_aux = @(x, y, z) [
    +0, -z, +y;
    +z, +0, -x;
    -y, +x, +0;
];
C = @(x) [
    C_aux(x(1), x(2), x(3)), zeros(3, 1); 
    zeros(1, 4)
];

%% Gradients
homogeneous_cross = @(a, b) [cross(a(1:3), b(1:3)); 0];
aux_rotationalGradient = @(xc, p, t, xi, omega) homogeneous_cross(xc, t - p) + C(xc) * ((xc + t - p) * xi * omega);
rotationalGradient = @(x, p, t, q, xi, omega) aux_rotationalGradient(Xc(x, q), p, t, xi, omega);

aux_translationalGradient = @(distance, xi, omega) (distance + (xi * omega * distance));
translationalGradient = @(x, p, t, q, xi, omega) aux_translationalGradient((Xc(x, q) + t - p), xi, omega);

%% Local Error
g = @(xc, p, t) xc + t - p;
h = @(xc, p, t, intersection_weight) intersection_weight * (norm(xc + t - p)^2);
aux_local_error = @(xc, p, t, intersection_weight) transpose(g(xc, p, t)) * g(xc, p, t) + h(xc, p, t, intersection_weight);
local_error = @(x, p, t, q, xi, omega) aux_local_error(Xc(x,q), p, t, xi * omega);

%% Model Points: each column is a point
X = [
    -1, 2, +3, 1;
    +5, 3, +4, 1;
    -2, 8, +2, 1;
    +7, 9, -1, 1 ]';
N = size(X, 2);

%% Static Points
P = X + [
    random_matrix(3, 3, 1), zeros(3,1); 
    zeros(1, 3), 1
];

%% Parameters
omegas = [0.2, 0.5, 1.0, 1.5];
xis = [0, 1];

t = [random_col_vector(3, 5); 0];
q = random_quaternion();
%% Compute the error and the gradients
fprintf('t: new Vector4D(%+015.15e %+015.15e %+015.15e %+015.15e)\n', t);
fprintf('q: new QuaternionD(%+015.15e, %+015.15e, %+015.15e, %+015.15e)\n', q);

for xi = xis
    for omega = omegas
        
        fprintf('xi: %d\n', xi);
        fprintf('omega: %d\n', omega);

        t_gradient = [0; 0; 0; 0];
        q_gradient = [0; 0; 0; 0];
        
        error = 0;
        for i = 1:size(X, 2)
            x = X(:,i);
            p = P(:,i);
            
            error = error + local_error(x, p, t, q, xi, omega);
            t_gradient = t_gradient + translationalGradient(x, p, t, q, xi, omega);
            q_gradient = q_gradient +    rotationalGradient(x, p, t, q, xi, omega);
        end
        
        error = 1 / N * error;
        t_gradient = 1 / (2 * N) * t_gradient;
        q_gradient = 1 / N * q_gradient;
        
        % Fix final values
        t_gradient(4) = 1;
        q_gradient(4) = 0;
        
        fprintf('error: %+015.15e\n', error);
        fprintf('translational gradient: new Vector4D(%+015.15e %+015.15e %+015.15e %+015.15e)\n', t_gradient);
        fprintf('rotational gradient: new QuaternionD(%+015.15e %+015.15e %+015.15e %+015.15e)\n\n', q_gradient);
    end
end
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
rotationalGradient = @(xc, p, t, xi, omega_d, omega_i) homogeneous_cross(xc, t - p) * (omega_d + xi * omega_i);

aux_translationalGradient = @(distance, xi, omega_d, omega_i) (distance * (xi * omega_i + omega_d));
translationalGradient = @(xc, p, t, xi, omega_d, omega_i) aux_translationalGradient((xc + t - p), xi, omega_d, omega_i);

%% Local Error
aux_local_error = @(distance, distance_weight, intersection_weight) (distance_weight + intersection_weight) * dot(distance, distance);
local_error = @(xc, p, t, xi, omega_d, omega_i) aux_local_error(xc + t - p, omega_d, xi * omega_i);

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
    zeros(1, 4)
];

%% Parameters
omegas_i = [1.0, 0.2];
omegas_d = [1.0, 0.3];
xis = [0, 1];

ts = [
    zeros(4, 1)';
    [random_col_vector(3, 5); 0]'
]';
qs = [
    [0; 0; 0; 1]';
    random_quaternion()'
]';

%% Init
XC = nan(size(X));

fprintf('P:\n');
fprintf('new Vector4D(%+015.15e %+015.15e %+015.15e %+015.15e)\n', P);
fprintf('\n');

%% Compute the error and the gradients
for t = ts
    for q = qs
        for xi = xis
            for omega_i = omegas_i
                for omega_d = omegas_d
                    
                    %Premultiply the model points
                    for i = 1:size(X, 1)
                        XC(:, i) = Xc(X(:, i), q);
                    end
                    XC(end, :) = ones(1, size(XC, 2));
                    
                    fprintf('XC:\n');
                    fprintf('new Vector4D(%+015.15e %+015.15e %+015.15e %+015.15e)\n', XC);
                    
                    fprintf('t: new Vector4D(%+015.15e %+015.15e %+015.15e %+015.15e)\n', t);    
                    fprintf('q: new QuaternionD(%+015.15e, %+015.15e, %+015.15e, %+015.15e)\n', q);
                    fprintf('xi: %1.1d\n', xi);
                    fprintf('omega_i: %1.1f\n', omega_i);
                    fprintf('omega_d: %1.1f\n', omega_d);

                    t_gradient = [0; 0; 0; 0];
                    q_gradient = [0; 0; 0; 0];

                    error = 0;
                    for i = 1:size(X, 2)
                        xc = XC(:,i);
                        p = P(:,i);

                        error = error + local_error(xc, p, t, xi, omega_d, omega_i);
                        t_gradient = t_gradient + translationalGradient(xc, p, t, xi, omega_d, omega_i);
                        q_gradient = q_gradient +    rotationalGradient(xc, p, t, xi, omega_d, omega_i);
                    end

                    error = (1 / (N * 4)) * error;
                    t_gradient = 1 / (2 * N) * t_gradient;
                    q_gradient = 1 / N * q_gradient;

                    % Fix final values
                    q_gradient(4) = 0;

                    fprintf('error: %+015.15e\n', error);
                    fprintf('translational gradient: new Vector4D(%+015.15e %+015.15e %+015.15e %+015.15e)\n', t_gradient);
                    fprintf('rotational gradient: new QuaternionD(%+015.15e %+015.15e %+015.15e %+015.15e)\n\n', q_gradient);
                end
            end
        end
    end
end
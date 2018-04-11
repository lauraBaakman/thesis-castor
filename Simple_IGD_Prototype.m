% A prototype for using iterative gradient descent to find the rotation and
% translation that minimizes the squared error as proposed by
%    "Wheeler, M. D., and K. Ikeuchi. "Iterative estimation of rotation and
%    translation using the quaternion: School of Computer Science."
%    (1995)".

close all; clear variables; clc;
rng(42);

%%Translation Vector and Rotation Matrix
t = [3, 4, 7];
TrMat = eye(4);
TrMat(1:3, end) = t';
% RMat = makehgtform('xrotate',pi/16,'yrotate',pi/12, 'zrotate', pi/8);
RMat = [+0.892399100832523, -0.369643810614386, +0.258819045102521, 0;
        +0.421979810690146, +0.886804577034556, -0.188442780494429, 0;
        -0.159865206355897, +0.277382579526975, +0.947365832385646, 0;
        +0,                 +0,                 +0,                 1;];

%%Model Points: each column is a point
X = [
    -1, 2, +3, 1;
    +5, 3, +4, 1;
    -2, 8, +2, 1;
    +7, 9, -1, 1 ]';
N = size(X, 2);

%% Transformation Matrix 1, should result in a Transformation Matrix that is the Identity
% M = eye(4);

%% Transformation Matrix 2, P is translated w.r.t. X with t.
% M = TrMat;

%% Transformation Matrix 3, P is rotated w.r.t. X with R
% M = RMat;

%% Transformation Matrix 4, P is rotated with R, and translated with T, w.r.t. X.
M = TrMat * RMat;

%% Compute the scale of the data
compute_scale = @(data) max(max(data(1:end -1, :), [], 2) - min(data(1:end -1, :), [], 2));
scale = compute_scale(X);

%% Compute Static Points
noise_vector = @(dimension, scale) [((rand(dimension, 1) * 2) - 1) * scale; 0];

P = nan(size(X));
for i = 1:size(X, 1)
    P(:, i) = M * X(:,i) + noise_vector(3, 0.001);    
end

%% Configuration
max_iterations = 5000;
learning_rate = 0.001;

%% Anonymous Functions
has_converged = @(error, iteration) error <= 0.00001 || iteration > max_iterations;

Ru = @(u, v, w, s)...  
[
    s * s + u * u - v * v - w * w,  2 * (u * v - s * w),            2 * (u * w + s * v),            0;
    2 * (u * v + s * w),            s * s - u * u + v * v - w * w,  2 * (v * w - s * u),            0;
    2 * (u * w - s * v),            2 * (v * w + s * u),            s * s - u * u - v * v + w * w,  0;
    0,                              0,                              0,                              1;
];
R = @(q) 1 / dot(q, q) * Ru(q(1), q(2), q(3), q(4));

Xc = @(x, q) R(q) * x;

homogeneous_cross = @(a, b) [cross(a(1:3), b(1:3)); 1];

quaternion_multiplication_aux = @(u1, s1, u2, s2) [(s1 * u2 + s2 * u1 + cross(u1, u2)); s1 * s2 - dot(u1, u2)];
quaternion_multiplication = @(q1, q2) quaternion_multiplication_aux(q1(1:3), q1(4), q2(1:3), q2(4));

compute_local_error = @(t, xc, p) dot((xc + t - p), (xc + t - p));

%% Partial Derivatices
partialToT = @(t, xc, p) +2 * (xc + t - p);
partialToR = @(t, xc, p) -4 * homogeneous_cross(xc, t - p);

%% Initialization
t_current = [0, 0, 0, 0]';
q_current = [0, 0, 0, 1]';

M_actual = eye(4);

XC = nan(size(X));

iteration = 1;

errors = nan(max_iterations + 1,1);

%% Iteration
while 1
    t_previous = t_current;
    q_previous = q_current;
    
    %Premultiply the model points
    for i = 1:size(X, 1)
        XC(:, i) = Xc(X(:, i), q_current);
    end
    XC(end, :) = ones(1, size(XC, 2));
    
    % Compute Error
    error = 0;
    for i = 1:size(X, 2)
        error = error + compute_local_error(t_current, XC(:, i), P(:,i));
    end
    error = 1/N * error;
    errors(iteration) = error;
    
    % Check for convergence
    if(has_converged(error, iteration)) 
        break;
    end
    
    % Compute the gradient of the error w.r.t. to t and q
    t_change = [0, 0, 0, 0]';
    q_change = [0, 0, 0, 0]';
    for i = 1:size(X, 2)
        t_change = t_change + partialToT(t_current, XC(:, i), P(:,i));
        q_change = q_change + partialToR(t_current, XC(:, i), P(:,i));
    end
    t_change = (1 / (2 * N)) * t_change;
    q_change = (1 / (2 * N)) * q_change;
    
    % Normalize the effect of scale on gradient directions
    t_change = t_change / scale;
    q_change = q_change / (scale * scale);
    
    % Update t_current and T_actual
    t_current = t_previous - learning_rate * t_change;
    T_actual = eye(4);
    T_actual(:, end) = t_current;
    
    % Update q_current and R_actual
    q_current = [-1 * learning_rate * q_change(1:3) ; 1];
    R_actual = R(q_current);
    
    % Update M_actual
    M_actual = T_actual * R_actual;
    
    % Update iteration count
    iteration = iteration + 1;
end

plot(1:(max_iterations + 1), errors);

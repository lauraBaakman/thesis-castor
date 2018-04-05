% A prototype for using iterative gradient descent to find the rotation and
% translation that minimizes the squared error as proposed by "Wheeler, M.
% D., and K. Ikeuchi. "Iterative estimation of rotation and translation
% using the quaternion: School of Computer Science." (1995)".

close all; clear variables; clc;

%%Translation Vector and Rotation Matrix
t = [3, 4, 7];
TrMat = makehgtform('translate', t);
RMat = makehgtform('xrotate',pi/2,'yrotate',pi/4, 'zrotate', pi/8);

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
M = TrMat;

%% Transformation Matrix 3, P is rotated w.r.t. X with R
% M = RMat;

%% Transformation Matrix 4, P is rotated with R, and translated with T, w.r.t. X.
% M = TrMat * RMat;

%% Compute Static Points
P = nan(size(X));
for i = 1:size(X, 1)
    P(:, i) = M * X(:,i);    
end


% Auxilaries
has_converged = @(M_actual, M_expected) (sum(M_actual(:) == M_expected(:)) == size(M_actual, 1) * size(M_actual, 2));

Ru = @(u, v, w, s)...  
[
    s * s + u * u - v * v - w * w,  2 * (u * v - s * w),            2 * (u * w + s * v),            0;
    2 * (u * v + s * w),            s * s - u * u + v * v - w * w,  2 * (v * w - s * u),            0;
    2 * (u * w - s * v),            2 * (v * w + s * u),            s * s - u * u - v * v + w * w,  0;
    0,                              0,                              0,                              1;
];
R = @(q) 1 / dot(q, q) * Ru(q(1), q(2), q(3), q(4));

Xc = @(x, q) R(q) * x;

compute_local_error = @(q, t, x, p) (R(q) * x + t - p).^2;

partialToT = @(q, t, x, p) 2 * (Xc(x, q) + t - p);

t_current = [0, 0, 0, 0]';
q_current = [0, 0, 0, 1]';

M_actual = eye(4);

learning_rate = 1;

while 1
    t_previous = t_current;
    q_previous = q_current;
    
    % Compute Error
    error = 0;
    for i = 1:size(X, 2)
        error = error + compute_local_error(q_current, t_current, X(:, i), P(:,i));
    end
    error = 1/N .* error;
    fprintf('Error: [%5.5f, %5.5f, %5.5f, %5.5f]\n', error);
    
    % Check for convergence
    if(has_converged(M_actual, M)) 
        break;
    end
    
    % Compute the gradient of the error w.r.t. to t and q
    t_change = [0, 0, 0, 0]';
    for i = 1:size(X, 2)
        t_change = t_change + partialToT(q_current, t_current, X(:, i), P(:,i));
    end
    t_change = (1 / (2 * N)) * t_change;
    
    % Update t_current and T_actual
    t_current = t_previous - learning_rate * t_change;
    T_actual = eye(4);
    T_actual(:, end) = t_current;
    
    % Update q_current and R_actual
    R_actual = eye(4);
    
    % Update M_actual
    M_actual = T_actual * R_actual;
end

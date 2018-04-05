% A prototype for using iterative gradient descent to find the rotation and
% translation that minimizes the squared error as proposed by "Wheeler, M.
% D., and K. Ikeuchi. "Iterative estimation of rotation and translation
% using the quaternion: School of Computer Science." (1995)".

%%Translation Vector and Rotation Matrix
t = [3, 4, 7];
T = makehgtform('translate', t);
R = makehgtform('xrotate',pi/2,'yrotate',pi/4, 'zrotate', pi/8);

%%Model Points: each column is a point
X = [
    -1, 2, 3, 0;
    +5, 3, 4, 0;
    -2, 8, 2, 0;
    +7, 9, -1, 0 ]';

%% Transformation Matrix 1, should result in a Transformation Matrix that is the Identity
M = eye(4);

%% Transformation Matrix 2, P is translated w.r.t. X with t.
% M = T;

%% Transformation Matrix 3, P is rotated w.r.t. X with R
% M = R;

%% Transformation Matrix 4, P is rotated with R, and translated with T, w.r.t. X.
% M = T * R;

%% Compute Static Points
P = nan(size(X));
for i = 1:size(X, 1)
    P(:, i) = M * X(:,i);    
end


% Auxilaries
convergence = @(M_actual, M_expected) (sum(M_actual(:) == M_expected(:)) == size(M_actual, 1) * size(M_actual, 2));

t_current = eye(4);

M_actual = eye(4);

while 1
    if(convergence(M_actual, M)) 
        break;
    end
end

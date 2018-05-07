deg2rad = @(deg) (pi/180).*deg;
applyTransform = @(matrix, vector) matrix * [vector, 1]';

original_position = [+1.000000, +1.000000, -1.000000];
no_rotation_translation = [1.500000, 1.700000, -1.900000];
rotation_no_translation = [1.724745, 0.158919, -0.000000];
rotation_translation = [2.224745, 0.858919, -0.900000];

alfa = deg2rad(90);
beta = deg2rad(45);
gamma = deg2rad(-30);


T0 = eye(4);

T1 = eye(4);
T1(1:3, 4) = [0.5, 0.7, -0.9];

% Rotate around x with alfa, around y with beta, around c with gamma in
% radians
R = eye(4);
R(1, 1) = +cos(gamma) * cos(beta);
R(1, 2) = -sin(gamma) * cos(alfa) + cos(gamma) * sin(beta) * sin(alfa);
R(1, 3) = +sin(gamma) * sin(alfa) + cos(gamma) * sin(beta) * cos(alfa);

R(2, 1) = + sin(gamma) * cos(beta);
R(2, 2) = + cos(gamma) * cos(alfa) + sin(gamma) * sin(beta) * sin(alfa);
R(2, 3) = - cos(gamma) * sin(alfa) + sin(gamma) * sin(beta) * cos(alfa);

R(3,1) = -sin(beta);
R(3,2) = cos(beta) * sin(alfa);
R(3,3) = cos(beta) * cos(alfa);

% Transformation Matrix rotation and translation
M_no_rotation_translation = T1;
M_rotation_no_translation = T0 * R;
M_rotation_translation = T1 * R;
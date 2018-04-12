close all; clear variables; clc;
rng(42);

%% Quaternion Methods
to_unit_quaternion = @(q) 1 / dot(q, q) * q;

axis_angle_to_quaternion = @(x, y, angle) [sin(angle / 2) * [x, y, sqrt(1 - x^2 - y^2)], cos(angle / 2)];
axis_angle_to_unit_quaternion = @(x, y, angle) to_unit_quaternion(axis_angle_to_quaternion(x, y, angle));

random_matrix = @(rows, cols, scale) (rand(rows, cols) -0.5) * scale;
random_row_vector = @(cols, scale) random_matrix(1, cols, scale);
random_col_vector = @(rows, scale) random_matrix(1, rows, scale);
random_scalar = @(scale) random_matrix(1,1, scale);

%% Rotation Matrix Methods
Ru_aux = @(u, v, w, s)...  
[
    s * s + u * u - v * v - w * w,  2 * (u * v - s * w),            2 * (u * w + s * v),            0;
    2 * (u * v + s * w),            s * s - u * u + v * v - w * w,  2 * (v * w - s * u),            0;
    2 * (u * w - s * v),            2 * (v * w + s * u),            s * s - u * u - v * v + w * w,  0;
    0,                              0,                              0,                              1;
];
Ru = @(q) Ru_aux(q(1), q(2), q(3), q(4));
R = @(q) 1 / dot(q, q) * Ru(q);


%% Variables
A = eye(4);
A(1:3, 1:3) = round((rand(3,3) -0.5) * 100);

%% Rotation Matrix Generation
unit_quaternion = axis_angle_to_unit_quaternion(random_scalar(1), random_scalar(1), random_scalar(2) * pi);
unitRotationMatrix = Ru(unit_quaternion);

quaternion = axis_angle_to_quaternion(random_scalar(1), random_scalar(1), random_scalar(2) * pi);
rotationMatrix = R(quaternion);

%% Equals Matrix
B = round(random_matrix(4,4, 100));

%% Multiplication Matrix
C = round(random_matrix(4,4, 100));

%% Multiplication Vector
a = B(:,1);
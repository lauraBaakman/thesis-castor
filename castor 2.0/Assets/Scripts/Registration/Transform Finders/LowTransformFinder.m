% Setup
clear variables; rng(42); clc;

%% Expected Result
expected_translation = [-3.0, +2.0, -0.5];
expected_rotation = [0, 0, 0];

%% Transform Finding Variables

staticPoints = [
     01.8000, 32.8000, 41.2000;
     42.5000, 08.6000, 34.7000;
     46.7000, 35.3000, 15.9000;
     33.9000, 01.6000, 47.5000;
     37.9000, 13.8000, 01.7000;
     37.2000, 02.3000, 21.9000;
     19.6000, 04.9000, 19.1000;
];

numCorrespondences = length(staticPoints);

modelPoints = staticPoints + repmat(expected_translation, numCorrespondences, 1);

modelNormals = rand(7, 3);
for i = 1:numCorrespondences
   modelNormals(i, :) = modelNormals(i, :) / norm(modelNormals(i, :)); 
end

%%  Prepare Matrices and Vectors
A = NaN(numCorrespondences, 6);
b = NaN(numCorrespondences, 1);

% Equation 8 en 10
for i = 1:length(modelPoints)  
    n = modelNormals(i, :);
    s = staticPoints(i, :);
    d = modelPoints(i, :);
    
    A(i, :) = [cross(s,n), n];
    b(i) = dot(n, d) - dot(n, s);
end

%% Compute xOpt
xOpt = pinv(A) * b;

%% Compute Transformation Matrix
% Angles in radians
alfa = xOpt(1);
beta = xOpt(2);
gamma = xOpt(3);

% Translation
tOpt = xOpt(4:6);

T = eye(4);
T(1:3, 4) = tOpt';

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
    
M = T * R;



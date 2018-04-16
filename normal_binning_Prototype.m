%% Test the binning of normals
close all; clc; clear variables;

rng(42);


%% Globals
% Split of the longitude
N = 5;

% Split of the latitude
M = 9;

% Number of normals
num_normals = 5;


%% Random Generator Functions
normalize = @(vector) 1 / norm(vector) * vector;

%% Find K and J
get_k = @(longitude) floor((N * longitude) / (2 * pi) - 1);
get_j = @(latitude) floor(M / 2 * (sin(latitude) + 1) - 1); 

%% Find latitude and longitude given the position
aux_get_longitude = @(x, y, z) atan( y / x);
get_longitude_from_position = @(position) aux_get_longitude(position(1), position(2), position(3));
get_longitude_from_k = @(k) (2 * k * pi) / N;

aux_get_latitude = @(x, y, z) acos ( z / sqrt(x^2 + y^2 + z^2));
get_latitude_from_position = @(position) aux_get_latitude(position(1), position(2), position(3));
get_latitude_from_j = @(k) asin(-1 + (2 * j) / M);

%% Generate Colors
compute_R = @(k) (256 / (N - 1)) * k;
compute_G = @(j) (256 / (M - 1)) * j;
compute_B = @(j, k) (256 / (N + M - 2)) * (j + k);

%% Conversion between Radians and Degrees
rad2deg = @(rad) rad./(pi/180);
deg2rad = @(deg) (pi/180).*deg;

%% Generate test normals
normals = nan(num_normals, 3);
for i = 1:num_normals
   normals(i, :) = normalize(rand(3,1)); 
end

colors = nan(num_normals, 3);

%% Bin Test Normals
for i = 1:size(normals, 1)
    normal = normals(i, :);
    
    [longitude, latitude, ~] = cart2sph(normal(1), normal(2), normal(3));
    
    k = get_k(longitude);
    j = get_j(latitude);
   
    fprintf('longitude: %2.5f, latitude: %2.5f, j: %d, k: %d\n', rad2deg(longitude), rad2deg(latitude), j, k);    
    
    r = compute_R(k);
    g = compute_G(j);
    b = compute_B(j, k);
    
    colors(i, :) = [r, g, b];
end

scatter3(normals(:, 1), normals(:, 2), normals(:, 3), 50, colors, 'filled');

% vertices = [
%     1, +2, 3;
%     3, +5, 9;
%     2, -2, 5;
%     3, -4, 6;
%     3, -3, 7];

% vertices = [
%     1, 2, 3;
%     2, 4, 3;
%     3, 9, 3;];

% vertices = [
%     1, +2, 3;
%     3, +5, 9;
%     2, -2, 5;];

vertices = [
%     2, 3, 4; %0
    7, 3, 5; %1
    6, 4, 9; %2
    4, 6, 7; %3
];

normal = [0, 0, 0];

x = 1; y = 2; z = 3;

for i = 1:length(vertices)
    current = vertices(i, :);
    next = vertices(mod(i, length(vertices)) + 1, :);

    normal(x) = normal(x) + (current(y) - next(y)) * (current(z) + next(z));
    normal(y) = normal(y) + (current(z) - next(z)) * (current(x) + next(x));
    normal(z) = normal(z) + (current(x) - next(x)) * (current(y) + next(y));
end

normal
normal = normal ./ norm(normal)
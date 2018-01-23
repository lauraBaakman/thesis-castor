cubeLeft = [0.5, 2.0, 0.3;
                2.0, 2.0, 0.3;
                0.5, 6.0, 0.3;
                2.0, 6.0, 0.3;
                0.5, 2.0, 2.3;
                2.0, 2.0, 2.3;
                0.5, 6.0, 2.3;
                2.0, 6.0, 2.3];
cubeRight = [   0.7, 1.2, 0.5;
                1.2, 1.2, 0.5;
                0.7, 4.2, 0.5;
                1.2, 4.2, 0.5;
                0.7, 1.2, 1.7;
                1.2, 1.2, 1.7;
                0.7, 4.2, 1.7;
                1.2, 4.2, 1.7];
            
pyramid = [ 0.9, 3.0, 0.4;
            2.5, 3.0, 0.4;
            2.5, 3.0, 2.0;
            0.9, 3.0, 2.0;
            1.7, 5.0, 1.2];
            
            
            
cubeCubeDist = pdist2(cubeLeft, cubeRight);

[~, indices] = sort(cubeCubeDist(:));
for i = 1:length(indices)
    [x, y] = ind2sub(size(cubeCubeDist), indices(i));
    fprintf(sprintf('cubeLeft: %d cubeRight:%d\n', x, y));
end


cubePyramidDist = pdist2(cubeLeft, pyramid);
[~, indices] = sort(cubePyramidDist(:));
for i = 1:length(indices)
    [x, y] = ind2sub(size(cubePyramidDist), indices(i));
    fprintf(sprintf('cubeLeft: %d pyramid:%d\n', x, y));
end

pyramidCubeDist = pdist2(pyramid, cubeRight);
[~, indices] = sort(pyramidCubeDist(:));
for i = 1:length(indices)
    [x, y] = ind2sub(size(pyramidCubeDist), indices(i));
    fprintf(sprintf('pyramid: %d cube:%d\n', x, y));
end
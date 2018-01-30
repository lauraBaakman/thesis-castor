staticPoints = [
    1.0, 2.0, 3.0; 
    3.4, 4.5, 5.6; 
    9.1, 2.3, 3.4
];

modelPoints = [
    2.0, 3.0, 4.0;
    6.7, 7.8, 8.9;  
    4.5, 5.6, 6.7;
];

distances = sum((staticPoints - modelPoints).^2, 2);
mean(distances)
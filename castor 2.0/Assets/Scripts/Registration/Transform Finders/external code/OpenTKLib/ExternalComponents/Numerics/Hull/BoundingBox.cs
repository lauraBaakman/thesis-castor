﻿using System;

namespace NLinear
{
    public struct BoundingBox<T>
       where T : IEquatable<T>

    {
        Numeric<T> X1, Y1, Z1, X2, Y2, Z2;
        public MinStruct<Numeric<T>> Min;
        public MaxStruct<Numeric<T>> Max;


        public BoundingBox(Numeric<T> x1, Numeric<T> y1, Numeric<T> z1, Numeric<T> x2, Numeric<T> y2, Numeric<T> z2)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.Z1 = z1;
            this.X2 = x2;
            this.Y2 = y2;
            this.Z2 = z2;

            Min = new MinStruct<Numeric<T>>(X1, Y1, Z1, X2, Y2, Z2);
            Max = new MaxStruct<Numeric<T>>(X1, Y1, Z1, X2, Y2, Z2);
        }
    }
    public struct MinStruct<T> //: IEquatable<MinStruct<T>> 
       where T : IEquatable<T>
    {
        //Numeric<T> X1, Y1, Z1, X2, Y2, Z2;
        public Numeric<T> X, Y, Z;
        public MinStruct(Numeric<T> x1, Numeric<T> y1, Numeric<T> z1, Numeric<T> x2, Numeric<T> y2, Numeric<T> z2)
        {
            X = x2;
            if (x1 < x2)
                X = x1;
            Y = y2;
            if (y1 < y2)
                Y = y1;
            Z = z2;
            if (z1 < z2)
                Z = z1;



        }
    }
    public struct MaxStruct<T> //: IEquatable<MaxStruct<T>>
       where T : IEquatable<T>
    {
        //Numeric<T> X1, Y1, Z1, X2, Y2, Z2;
        public Numeric<T> X, Y, Z;
        public MaxStruct(Numeric<T> x1, Numeric<T> y1, Numeric<T> z1, Numeric<T> x2, Numeric<T> y2, Numeric<T> z2)
        {
            X = x2;
            if (x1 > x2)
                X = x1;
            Y = y2;
            if (y1 > y2)
                Y = y1;
            Z = z2;
            if (z1 > z2)
                Z = z1;


        }
    }

}

﻿namespace NLinear
{
   
    public class Edge
    {
        public int Condition = -1;
        public VertexHull p1;
        public VertexHull p2;
        public Edge() { }
        public Edge(VertexHull P1, VertexHull P2)
        {
            p1 = P1; p2 = P2;
        }
    }
   
}

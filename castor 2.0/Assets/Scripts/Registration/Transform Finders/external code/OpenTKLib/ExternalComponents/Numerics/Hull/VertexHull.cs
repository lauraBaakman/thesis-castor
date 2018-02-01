namespace NLinear
{
    public class VertexHull
    {
        public Vector3<double> Vector;
        public int Condition = -1;
        public double Radius = 0;
        public VertexHull() { }
        public VertexHull(Vector3<double> pt, double radius)
        {
            this.Vector = pt; 
            Radius = radius;
        }
    }
  
   
}

using UnityEngine;

namespace Registration
{
    public class Point
    {

        private Vector3 position;

        public Point(Vector3 position)
        {
            this.Position = position;
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}
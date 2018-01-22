using UnityEngine;
using System.Collections;

namespace Registration
{

    /// <summary>
    /// Correspondence, containing the two points that are considered paired in an 
    /// ICP iteration.
    /// </summary>
    public class Correspondence
    {
        private readonly Vector3 ModelPoint;
        private readonly Vector3 StaticPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Correspondence"/> class. 
        /// </summary>
        /// <param name="modelPoint">The point of the correspondence from the model mesh..</param>
        /// <param name="staticPoint">The point of the correspondence from the static mesh.</param>
        public Correspondence(Vector3 modelPoint, Vector3 staticPoint)
        {
            ModelPoint = modelPoint;
            StaticPoint = staticPoint;
        }

        public override string ToString()
        {
            return "Correspondence (" + ModelPoint + ", " + StaticPoint + ")";
        }
    }
}

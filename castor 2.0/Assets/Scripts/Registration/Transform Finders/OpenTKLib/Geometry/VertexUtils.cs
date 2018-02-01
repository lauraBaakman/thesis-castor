using System.Collections.Generic;

namespace OpenTKLib
{
    /// <summary>
    /// Compare vertices based on their indices in the model
    /// </summary>
    public class VertexComparerIndexInModel : IComparer<Vertex>
    {
        public static readonly VertexComparerIndexInModel IndexInModel = new VertexComparerIndexInModel();

        public int Compare(Vertex x, Vertex y)
        {
            return x.IndexInModel.CompareTo(y.IndexInModel);
        }
    }
    
}

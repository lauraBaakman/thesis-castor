using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Registration
{
    /// <summary>
    /// Find the correspondencecs using an adaption of the method proposed in 
    /// Chen, Yang, and Gérard Medioni. "Object modelling by registration of 
    /// multiple range images." Image and vision computing 10.3 (1992): 145-155.
    /// 
    /// This method shoots rays from the staticpoints, along the normal of that 
    /// point, if that ray intersects with the model fragment the intersection 
    /// point and the static point form a correspondence.
    /// </summary>
    public class NormalShootingCorrespondenceFinder : ICorrespondenceFinder
    {
        public List<Correspondence> Find(ReadOnlyCollection<Point> staticPoints, ReadOnlyCollection<Point> modelPoints)
        {
            throw new System.ArgumentException("This method cannot find correspondences between two sets of points.");
        }

        public List<Correspondence> Find(ReadOnlyCollection<Point> staticPoints, SamplingInformation modelSamplingInformation)
        {
            Correspondence correspondence;

            Debug.Log("NormalShootingCorrespondenceFinder:Find");

            List<Correspondence> correspondences = new List<Correspondence>();
            foreach (Point staticPoint in staticPoints)
            {
                correspondence = FindCorrespondence(staticPoint, modelSamplingInformation);
                if (correspondence != null) correspondences.Add(correspondence);
            }
            return correspondences;
        }

        private Correspondence FindCorrespondence(Point staticPoint, SamplingInformation modelSamplingInformation)
        {
            return null;
        }

        private Point FindIntersection(Point staticPoint, Mesh modelMesh)
        {
            return null;
        }
    }
}
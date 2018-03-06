
using UnityEngine;
using NUnit.Framework;

using Registration;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class CorrespondencesTests
    {
        private List<Correspondence> correspondenceList;
        private CorrespondenceCollection correspondences;

        [SetUp]
        public void Init()
        {
            correspondenceList = new List<Correspondence>
            {
                Auxilaries.RandomCorrespondence(),
                Auxilaries.RandomCorrespondence(),
                Auxilaries.RandomCorrespondence(),
                Auxilaries.RandomCorrespondence()
            };

            correspondences = new CorrespondenceCollection();
            foreach (Correspondence correspondence in correspondenceList)
            {
                correspondences.Add(correspondence);
            }
        }

    }
}
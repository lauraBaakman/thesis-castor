using UnityEngine;
using System.Collections;

namespace Registration
{
    public class Settings : MonoBehaviour
    {
        private IPointSelector _pointSelector;

        public IPointSelector PointSelector
        {
            get
            {
                return _pointSelector;
            }

            set
            {
                _pointSelector = value;
            }
        }

        public Settings()
        {
            PointSelector = new SelectAllPointsSelector();
        }
    }
}


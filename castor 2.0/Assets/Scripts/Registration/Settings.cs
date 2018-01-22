using UnityEngine;
using System.Collections;

namespace Registration
{
    public class Settings : MonoBehaviour
    {
        private IPointSelector pointSelector;

        Settings(){
            pointSelector = new SelectAllPointsSelector();
        }
    }
}


using UnityEngine;
using System.Collections.Generic;

public class ICPController : MonoBehaviour, Registration.IICPListener {

    public void OnICPPointsSelected( List<Vector3> points )
    {
        Debug.Log("Received points!");
    }
}

using UnityEngine;
using System.Collections.Generic;

public class ICPController : Registration.IICPListener
{
    void Start()
    {
        Debug.Log("Hi!");
    }

    public void OnICPPointsSelected(List<Vector3> points)
    {
        Debug.Log("Received points!");
    }
}

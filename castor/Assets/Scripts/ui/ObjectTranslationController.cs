using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTranslationController : TransformController {

    public override void ToggleActivity(bool toggle)
    {
        Debug.Log("ObjectTranslationController:ToggleActivity");
    }
}

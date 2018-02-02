using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;

[ExecuteInEditMode]
public class PlayFromScene : EditorWindow
{
    [MenuItem("Edit/Play-Stop, But From Prelaunch Scene %0")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene("Assets/Scenes/start.unity");
        EditorApplication.isPlaying = true;
    }
}
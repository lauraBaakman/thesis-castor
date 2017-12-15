using UnityEngine;

public static class GameObjectExtensions
{

    //source https://answers.unity.com/answers/641022/view.html
    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }
}


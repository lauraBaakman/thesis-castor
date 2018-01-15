using System;
using System.Reflection;
using UnityEngine;


/// <summary>
/// Component extensions contains extensions for Unity Components.
/// </summary>
public static class ComponentExtensions
{
    /// <summary>
    /// Creates a copy of a Component.
    /// //source https://answers.unity.com/answers/641022/view.html
    /// </summary>
    /// <returns>The copy of this component.</returns>
    /// <param name="comp">The component to copy.</param>
    /// <param name="other">The component the values of comp should be copied to.</param>
    /// <typeparam name="T">The type of component.</typeparam>
    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch (NotImplementedException)
                {
                }
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }
}


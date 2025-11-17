using System.Runtime.CompilerServices;
using UnityEngine;

public static class Logger
{
#if UNITY_EDITOR
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(
        string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string methodName = "")
    {
        string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
        Debug.Log($"[{className}.{methodName}] {message}");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(
        string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string methodName = "")
    {
        string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
        Debug.LogWarning($"[{className}.{methodName}] {message}");
    }
#endif
}
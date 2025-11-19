using UnityEngine;

public static class GameObjectExtensions
{
    public static void Toggle(this GameObject go)
    {
        if (go == null)
        {
            Logger.Log("GameObject is null");
            return;
        }

        bool active = !go.activeSelf;
        Logger.Log($"{go.name} → {(active ? "활성화" : "비활성화")}");
        go.SetActive(active);
    }
}

public static class TransformExtensions
{
    public static T FindChild<T>(this Transform t, string name) where T : Component
    {
        T[] children = t.GetComponentsInChildren<T>(true);

        foreach (var child in children)
        {
            if (child.name == name)
            {
                return child;
            }
        }

        Logger.Log($"{name} 컴포넌트 탐색 실패");
        return null;
    }
}
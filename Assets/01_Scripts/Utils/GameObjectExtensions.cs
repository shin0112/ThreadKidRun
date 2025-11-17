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

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var playerStats = (PlayerStats)target; //targer as PlayerStats;

        EditorGUILayout.ObjectField("스크립트",MonoScript.FromMonoBehaviour((PlayerStats)target),typeof(PlayerStats),false); 
        EditorGUILayout.HelpBox("주인공 캐릭터 스탯",MessageType.Info);

        playerStats.hp = EditorGUILayout.IntField("HP", playerStats.hp);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(playerStats.isGodMode ? "무적모드" : "일반모드"))
        {
            playerStats.isGodMode = !playerStats.isGodMode;
        }
        if(GUILayout.Button("데이터 초기화"))
        {
            playerStats.InitPlayerData();
        }
        EditorGUILayout.EndHorizontal();
    }
}

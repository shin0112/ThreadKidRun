using GameName.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AudioManager 테스트용 스크립트
/// 키보드 입력으로 사운드를 테스트합니다.
/// </summary>
public class AudioTest : MonoBehaviour
{
    private void Update()
    {
        // 1번 키: BGM 재생
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.Instance.PlayBGM("MainTheme");
            Debug.Log("[테스트] 1번 키 - BGM 재생");
        }

        // 2번 키: BGM 정지
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.Instance.StopBGM();
            Debug.Log("[테스트] 2번 키 - BGM 정지");
        }

        // 3번 키: SFX 재생
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioManager.Instance.PlaySFX("Jump");
            Debug.Log("[테스트] 3번 키 - SFX 재생");
        }

        // 4번 키: 마스터 볼륨 50%
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioManager.Instance.SetMasterVolume(0.5f);
            Debug.Log("[테스트] 4번 키 - 마스터 볼륨 50%");
        }

        // 5번 키: 마스터 볼륨 100%
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AudioManager.Instance.SetMasterVolume(1f);
            Debug.Log("[테스트] 5번 키 - 마스터 볼륨 100%");
        }
    }

    // 화면에 조작법 표시
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label("=== AudioManager 테스트 ===");
        GUILayout.Label("1: BGM 재생");
        GUILayout.Label("2: BGM 정지");
        GUILayout.Label("3: SFX 재생");
        GUILayout.Label("4: 마스터 볼륨 50%");
        GUILayout.Label("5: 마스터 볼륨 100%");
        GUILayout.EndArea();
    }
}

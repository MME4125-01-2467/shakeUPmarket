using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void StartButton()
    {
        Time.timeScale = 1;
        //재시작 하는 경우 이전 플레이어 오브젝트를 삭제해준다.
        Destroy(GameObject.Find("player"));
        SceneManager.LoadScene("main");
    }
}
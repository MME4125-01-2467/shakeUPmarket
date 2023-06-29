using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartButton()
    {
        //스타트버튼을 클릭하면 timeScale이 1로 바뀌면서 시간이 흐르고 main 씬으로 들어가게 된다.
        Time.timeScale = 1;
        SceneManager.LoadScene("main");
    }
}

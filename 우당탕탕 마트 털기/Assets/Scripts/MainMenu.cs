using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void MenuButton()
    {
        //메뉴버튼이 클릭되면 시간이 흐르도록 timeScale가 1로 설정되고 StartMenu 씬으로 넘어간다.
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
        if(GameObject.Find("player")!=null){
            Destroy(GameObject.Find("player"));
        }
    }
}

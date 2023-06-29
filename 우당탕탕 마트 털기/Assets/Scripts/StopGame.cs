using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopGame : MonoBehaviour
{
    public Sprite Go_img;
    public Sprite Stop_img;
    Image thisImg;
    public int state;
    public GameObject Base, Logo, Restart, Menu;

    void Start(){
        Base.SetActive(false);
        Logo.SetActive(false);
        Restart.SetActive(false);
        Menu.SetActive(false);
        thisImg = GetComponent<Image>();
        state = 0;
    }

    public void StopButton()
    {
        if(state == 0){
            //일시정지 버튼을 누르면 이미지가 재생 이미지로 바뀌고 시간이 멈춘다.
            Time.timeScale = 0;
            Base.SetActive(true);
            Logo.SetActive(true);
            Restart.SetActive(true);
            Menu.SetActive(true);
            thisImg.sprite = Go_img;
            state = 1;
        }
        else if(state == 1){
            //다시 재생 버튼을 누르면 재시작 메뉴들이 비활성화 되고 시간이 다시 흐르게 된다.
            Time.timeScale = 1;
            Base.SetActive(false);
            Logo.SetActive(false);
            Restart.SetActive(false);
            Menu.SetActive(false);
            thisImg.sprite = Stop_img;
            state = 0;
        }
    }
}
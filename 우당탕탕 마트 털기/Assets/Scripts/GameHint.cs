using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHint : MonoBehaviour
{
    public GameObject Hint;
    public Button btnHint;

    void Start()
    {
        //게임 시작과 동시에 힌트 이미지를 비활성화 시킨다.
        Hint.SetActive(false);
        btnHint.onClick.AddListener(ShowHint);
    }

    void ShowHint(){
        //힌트 버튼이 클릭되면 힌트 이미지를 활성화시킨다.
        Hint.SetActive(true);
        //5초 뒤 이미지를 비활성화 하는 함수를 실행한다.
        Invoke("HideHint", 5);
    }

    void HideHint(){
        Hint.SetActive(false);
    }
}

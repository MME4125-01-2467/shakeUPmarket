using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class cartTrigger : MonoBehaviour
{
    public GameObject GO_Inventory;
    public GameObject GO_Player;
    public List<GameObject> Inventory = new List<GameObject>(); //인벤토리 리스트

    public Text ScriptTxtName;
    public Text ScriptTxtPrice;
    
    private void printItem() //인벤토리 안에 있는 오브젝트를 출력하는 함수
    {

        if(Inventory.Count != 0 && ScriptTxtPrice != null && ScriptTxtName != null){
            ScriptTxtName.text="";
            ScriptTxtPrice.text="";

            for(int i = Inventory.Count - 1; i >= 0; i--)
            {
                string itemName = Inventory[i].GetComponent<Object>().KName;
                ScriptTxtName.text = ScriptTxtName.text + itemName + "\n";


                string itemPrice = Inventory[i].GetComponent<Object>().Price + "원\n";
                if(itemName.Length > 9)
                { // 아이템 이름이 9글자가 넘어가면, 줄바꿈이 자동으로 진행되므로 itemprice text 창에서도 띄어쓰기를 한번 해준다.
                    itemPrice = itemPrice+"\n";
                    print( itemPrice );

                }
                ScriptTxtPrice.text = ScriptTxtPrice.text + itemPrice;
        }
        }
    }
        
    
    private void OnTriggerEnter(Collider other)
    { // 카트 안으로 콜라이더가 들어올 때
        if(other.gameObject.CompareTag("item"))
        { //카트 안으로 들어온 오브젝트 태그가 아이템일 때
            Inventory.Add(other.gameObject);
            printItem();
        }

        if(other.gameObject.CompareTag("goal"))
        { //카트 안으로 들어온 오브젝트 태그가 골일 때
            //GameEnd 씬을 불러온다.
            SceneManager.LoadScene("GameEnd");
            for(int i = Inventory.Count - 1; i >= 0; i--)
            { //Invetory 리스트에 있던 오브젝트들을 Inventory 게임 오브젝트로 옮긴다
                print("옮기다" +  Inventory[i].GetComponent<Object>().Name);
                Inventory[i].transform.parent = GO_Inventory.transform;
            }
            //씬은 바뀌어도, 플레이어는 바뀌지 않도록 DontDestroyOnLoad를 선언한다.
            DontDestroyOnLoad(GO_Player);
        }
    }

    private void OnTriggerExit(Collider other)
    { // 카트 안으로 콜라이더가 나갈 때
        for(int i = Inventory.Count - 1; i >= 0; i--)
        { 
	        if(Inventory[i] == other.gameObject)
            { // 해당 게임 오브젝트를 찾고 지운다.
                Inventory.Remove(Inventory[i]);
                printItem();
            }
        }
    }

}

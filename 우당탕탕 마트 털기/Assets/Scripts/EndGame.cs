using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public GameObject[] Inventory;
    public Vector3 StartPos;

    public GameObject Restart, Menu;

    public Text ScriptTxtName, ScriptTxtPrice, ScriptTotalName, ScriptTotal;

    int i;

    public Text Best;
    
    // Start is called before the first frame update
    
    void Start()
    {
        Restart.SetActive(false);
        Menu.SetActive(false);

        //아이템들을 찾아, 인벤토리 배열에 넣는다.
        Inventory = GameObject.FindGameObjectsWithTag("item");
        i=0;
    }

    // Update is called once per frame
    int[] score;
    void Rank(GameObject Scores, int total){
        score = Scores.GetComponent<Score>().Score1;
        int newScore = total;
        print(newScore);
        for(int i = 0; i <3;  i++){
            if(score[i] < newScore)
            {
                for(int j = 1; j>=i; j--){
                    score[j+1] = score[j]; 
                }
                score[i] = newScore;
                break;
            }
         }
    }

    float currTime;
    int total = 0;
    void Update(){
        // 시간이 흐르게 만들어준다.
        currTime += Time.deltaTime;
        // 오브젝트를 몇초마다 생성할 것인지 조건문으로 만든다. 여기서는 10초로 했다.
        if (currTime > 2)
        {
            if(i < Inventory.Length)
            {
                //배열에 있는 오브젝트를 차례로 떨어트리고, 정보를 영수증에 띄운다.
                print(Inventory.Length+":"+i+":"+Inventory[i].name);
                Inventory[i].transform.position = StartPos;

                int p = int.Parse(Inventory[i].GetComponent<Object>().Price);
                total += p;
                ScriptTxtName.text += Inventory[i].GetComponent<Object>().KName + "\n";
                ScriptTxtPrice.text += Inventory[i].GetComponent<Object>().Price + "\n";
                if(Inventory[i].GetComponent<Object>().KName.Length>9){
                    ScriptTxtPrice.text+= "\n";
                }

                i++;

            }

            else if (i == Inventory.Length)
            {
                //배열에 있는 오브젝트를 전체 출력했을 경우, 합계를 띄운다.
                ScriptTotalName.text = "합계 :";
                ScriptTotal.text = total.ToString();
                Restart.SetActive(true);
                Menu.SetActive(true);
                Rank(GameObject.Find("Scores"),total);
                i++;
                Best.text  ="";
                for(int j = 0; j<3; j++){
                    if(score[j] != -1){
                        Best.text = Best.text+ (j+1) + " : " + score[j]+ "\n";
                    }
                }
            }
            currTime = 0;
        }    
    }
    
}




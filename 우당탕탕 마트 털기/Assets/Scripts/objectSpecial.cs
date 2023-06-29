using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

[System.Serializable]
//아이템 클래스
public class Item{
    public GameObject obj;
    public string No, Name, Price, KName;
    public bool Special = false;
    
    public Item(string _No, string _Name, string _Price, string _KName){
        No = _No;
        Name = _Name;
        Price = _Price;
        KName = _KName;
    }
    public Item(Item copy){ //생성자1
        No = copy.No;
        Name = copy.Name;
        Price = copy.Price;
        KName = copy.KName;
    }
    public void addGameObject(GameObject addObj){ //생성자2
        obj = addObj;
    }
}

public class objectSpecial : MonoBehaviour
{
    public TextAsset itemText; 
    public string No, Name, Price;

    Material objSpecial;
    Renderer renderers;
    List<Material> materialList = new List<Material>();
    
    //게임에 있는 아이템 종류

    public List<Item> itemList;
    // Start is called before the first frame update
    void Start()
    {
        //item price txt 파일을 읽어 아이템 번호, 이름, 가격, 한국이름 정리하기
        string[] line = itemText.text.Substring(0, itemText.text.Length - 1).Split('\n');
        for(int i =0; i< line.Length; i++){
            //txt 파일을 열로 잘라서 값을 배열로저장한다.
            string [] row = line[i].Split('\t');
            //아이템 객체를 생성해, 아이템 종류에 추가한다.
            itemList.Add(new Item(row[0], row[1], row[2], row[3]));
        }

        //Scene에 있는 모든 item 태그 GameObject 불러오기
        GameObject [] objList = GameObject.FindGameObjectsWithTag("item");
        print(objList.Length);

        //Scene에 있는 오브젝트 중 40개의 오브젝트에 spcial 속성 부여하기
        objSpecial = new Material(Shader.Find("Unlit/objSpecial"));
        int size = 40;
        int [] numList = new int[size];
        
        //오브젝트 40개 뽑기
        for (int i = 0; i<size; i++){
            numList[i] = Random.Range(0, objList.Length);
            if(i>0){
                for(int j = 0; j<i; j++){
                    while(numList[j] == numList[i]){
                        numList[i] =Random.Range(0, objList.Length);
                    }
                }
            }

        }

        //오브젝트 뽑고 material 추가하기
        for (int i = 0; i<size; i++){
            objList[numList[i]].tag = "itemSpecial";

            renderers = objList[numList[i]].GetComponent<Renderer>();
            materialList.Clear();
            materialList.AddRange(renderers.sharedMaterials);
            materialList.Add(objSpecial);

            renderers.materials = materialList.ToArray();
        }

        for(int i = 0; i<objList.Length; i++){

            //오브젝트의 이름 구하기 (중복되는 경우, 추가적인 숫자가 붙기 때문에 붙은 숫자를 제거하는 작업)
            string str = objList[i].name; 
            string[] words = str.Split(' ');
            string[] item = words[0].Split('(');
            //아이템 리스트에서 이름이 같은 아이템이 있을 경우, 해당 정보를 게임 오브젝트에 전달한다.
            for(int j = itemList.Count - 1; j >= 0; j--){
                if(string.Equals(item[0],itemList[j].Name))
                {
                    if(objList[i].GetComponent<Object>() == null){
                        objList[i].AddComponent<Object>();
                        objList[i].GetComponent<Object>().No = itemList[j].No;
                        objList[i].GetComponent<Object>().Name = itemList[j].Name;
                        objList[i].GetComponent<Object>().Price = itemList[j].Price;
                        objList[i].GetComponent<Object>().KName = itemList[j].KName;
                        objList[i].GetComponent<Object>().Special = itemList[j].Special;
                        
                        //오브젝타가 special한 속성을 가지고 있을 경우
                        if(objList[i].CompareTag("itemSpecial"))
                        {
                            //가격과 이름을 재설정해준다.
                            objList[i].GetComponent<Object>().Price = "10000";
                            objList[i].GetComponent<Object>().KName = "특별한"+itemList[j].KName;
                            objList[i].tag = "item";
                        }
                    }
                }
            }
    
        }
        
    }
}
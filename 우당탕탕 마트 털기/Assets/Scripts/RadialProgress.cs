using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RadialProgress : MonoBehaviour
{
    public Image LoadingBar;
    public Image Red;
    float currentValue;
    float currentValuRed;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Stop").GetComponent<StopGame>().state == 0){
        if (currentValue < 60){
            currentValue += speed * Time.deltaTime;
            currentValuRed += speed * Time.deltaTime;
            if(currentValue>49){
                if(currentValuRed>2){
                    StartCoroutine(ShowRedscreen());
                    currentValuRed = 0;
                }
            }
            else if(currentValue>54){
                if(currentValuRed>1){
                    StartCoroutine(ShowRedscreen());
                    currentValuRed = 0;
                }
            }

        }
        else{
            SceneManager.LoadScene("GameOver");
        }

        LoadingBar.fillAmount = (1 - currentValue / 60);
        }
        else{
            
        }
    }
    IEnumerator ShowRedscreen(){
        yield return new WaitForSeconds(1f);
        Red.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(1f);
        Red.color = Color.clear;
    }
}
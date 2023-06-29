using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public GameObject Scores;

    void Start()
    {

        if(GameObject.Find("Scores")==null)
        {
            (GameObject.Find("Scores1")).name = "Scores";
        }
        else
        {
            Destroy(GameObject.Find("Scores1"));
        }
        Scores = GameObject.Find("Scores");
        DontDestroyOnLoad(Scores);
    }

}

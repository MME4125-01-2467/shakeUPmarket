
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

using Debug = UnityEngine.Debug;



public class EnemyWalking : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem explosion;

    Vector3 position;
    int mode; 
    int recent;
    Animator animator;
    
    bool bump;
    long time;
    Stopwatch sw = new Stopwatch();
    Stopwatch sw1 = new Stopwatch();
    int count;


    [SerializeField] GameObject Player;

    public static bool freeze;
    Coroutine coroutine;
    Vector3 FXposition;


    void Awake()
    {
        animator = GetComponent<Animator>();
        count = 0;
        moveSample.freeze = false;
    }


    void Start()
    {
        position = transform.position;
        mode =1;
        bump = false;
        transform.forward = new Vector3(0, 0, -1);
        
    }


    void Update()
    {
        
        if (mode == 1)
        {
            transform.forward = new Vector3(0, 0, -1);
            position.z -= 0.4f * Time.deltaTime;
            transform.position = position;
        }
        if (mode == 2)
        {
            transform.forward = new Vector3(0, 0, 1);
            position.z += 0.4f * Time.deltaTime;
            transform.position = position;

        }
       
        if (mode == 0)
        {
            transform.position = position;
        }
        
        if (bump)
        {
            coroutine=StartCoroutine(Bump());
            time = sw.ElapsedMilliseconds;
            if (time > 1000) { StopMethod();
                
                bump = false;
                animator.SetBool("isWalking", true);
                if (recent == 1) mode = 2;
                else{ mode = 1;}
                


                sw.Reset();
                sw1.Start();
            }
        }

        if (sw1.ElapsedMilliseconds > 1500) { 
            moveSample.freeze = false; sw1.Reset(); count = 0;
        }



    }

   
    IEnumerator Bump()
    {
        
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            animator.SetBool("isWalking", false);
            mode = 0;
            
        }
        yield return null;
    }

    void StopMethod()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
   
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().gameObject.CompareTag("Wall1"))
        {
            mode = 2;
        }
         if (collision.GetComponent<Collider>().gameObject.CompareTag("Wall2"))
        {
            mode = 1;
        }
        if (collision.GetComponent<Collider>().gameObject.CompareTag("Player"))
        {
    
            if(collision.gameObject.name == "Cube_front"||collision.gameObject.name == "Cube_back"){
                if(GameObject.Find("Cube_front").transform.localPosition.z - GameObject.Find("Cube_front").transform.localPosition.z > 0){recent = 2;}
                else{ recent = 1;}
            }

            else if(collision.gameObject.name == "Cube_right"||collision.gameObject.name == "Cube_left"){
                print("bump");
                print(GameObject.Find("Cube_right").transform.localPosition.x - GameObject.Find("Cube_left").transform.localPosition.x);
                if(GameObject.Find("Cube_right").transform.localPosition.x - GameObject.Find("Cube_left").transform.localPosition.x > 0){ recent = 1;}
                else{recent = 2;}
            }
        
            


            print(recent);
            count++;
            sw.Start();
            FXposition = transform.position;
            FXposition.z -= 1;
            FXposition.y = 1f;

            //transform.Rotate(new Vector3(0,1,0) * Time.deltaTime);

            if (count == 1)
            {
            Instantiate(explosion, FXposition, Quaternion.identity);

            bump = true;
                moveSample.freeze = true;

            
            }
            
            
        }
    }


}
     

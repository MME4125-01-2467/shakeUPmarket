
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using Debug = UnityEngine.Debug;



public class Janitor : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem explosion;
    Vector3 position;
    int mode; //enum
    Animator animator;
    bool bump;
    long time;
    Stopwatch sw = new Stopwatch();
    Stopwatch sw1 = new Stopwatch();
    int count;

    Vector3 player;
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
        mode = 1;
        bump = false;
        transform.forward = new Vector3(-1, 0, 0);

    }


    void Update()
    {


        if (mode == 1)
        {
            transform.forward = new Vector3(-1, 0, 0);
            position.x -= 0.15f * Time.deltaTime;
            transform.position = position;
        }
        if (mode == 2)
        {
            transform.forward = new Vector3(1, 0, 0);
            position.x += 0.15f * Time.deltaTime;
            transform.position = position;

        }
        if (mode == 0)
        {
            //position.z += 0.4f * Time.deltaTime;
            transform.position = position;

        }
        if (bump)
        {
            transform.LookAt(Player.transform);
            coroutine = StartCoroutine(Bump());
            time = sw.ElapsedMilliseconds;
            if (time > 1000)
            {
                StopMethod();
                bump = false;
                animator.SetBool("isWalking", true);
                mode = 2;
                sw.Reset();
                sw1.Start();
            }
        }
        
        if (sw1.ElapsedMilliseconds > 1500) { moveSample.freeze = false; sw1.Reset(); count = 0; }

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
            count++;
            sw.Start();
            FXposition = transform.position;
            FXposition.z -= 1;
            FXposition.y = 1f;
            if (count == 1)
            {
                Instantiate(explosion, FXposition, Quaternion.identity);

                bump = true;
                moveSample.freeze = true;
            }
        }
    }
}


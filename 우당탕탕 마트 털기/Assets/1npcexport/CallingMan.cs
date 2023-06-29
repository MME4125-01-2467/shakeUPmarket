using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CallingMan : MonoBehaviour
{
    [SerializeField]
    public ParticleSystem explosion;

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
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        count = 0;
        moveSample.freeze = false;
    }


    void Start()
    {

        bump = false;


    }


    void Update()
    {

        if (bump)
        {
            coroutine = StartCoroutine(Bump());
            time = sw.ElapsedMilliseconds;
            if (time > 1000)
            {
                StopMethod();
                bump = false;
                animator.SetBool("isWalking", true);
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

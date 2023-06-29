using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]
public class EnemyWalking2 : MonoBehaviour
{
    public float pursuitSpeed;
    public float wanderSpeed;
    public float currentSpeed;

    public float directionChangeInterval;
    //public bool followPlayer;

    Coroutine moveCoroutine;
    BoxCollider BoxCollider;
    Rigidbody rb;
    Animator animator;

    Transform targetTransform = null;
    Vector3 endPosition;
    float currenAngle = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
        currentSpeed = wanderSpeed;
        StartCoroutine(WanderRoutine());
    }

    void Update()
    {
        Debug.DrawLine(rb.position, endPosition, Color.red);
    }

    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPoint();

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);

            }
            moveCoroutine = StartCoroutine(Move(rb, currentSpeed));

            yield return new WaitForSeconds(directionChangeInterval);
        }
    }


    void ChooseNewEndPoint()
    {
        currenAngle += Random.Range(0, 360);
        currenAngle = Mathf.Repeat(currenAngle, 360);
        endPosition += Vector3FromAngle(0);
    }

    Vector3 Vector3FromAngle(float inputAngleDegrees)
    {
        float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
    }

    public IEnumerator Move(Rigidbody rigidBodyToMove, float speed)
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;

            }

            if (rigidBodyToMove != null)
            {
                animator.SetBool("isWalking", true);
                Vector3 newPosition = Vector3.MoveTowards(rigidBodyToMove.position, endPosition, speed * Time.deltaTime);

                rb.MovePosition(newPosition);
                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("isWalking", false);
    }
    /*
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb, currentSpeed));
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            targetTransform = null;
        }
    }
    
    

    void OnDrawGizmos()
    {
        if (BoxCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, BoxCollider.radius);
        }
    }
    */
}

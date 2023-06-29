using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] LayerMask layer;
    [SerializeField] float radius;
    [SerializeField] Collider[] col;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("enemyaround", 0, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void enemyaround()
    {
        col = Physics.OverlapSphere(player.transform.position, radius, layer);
    }
}

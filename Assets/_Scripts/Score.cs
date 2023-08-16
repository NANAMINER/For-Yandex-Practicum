using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float speed = 5f;
    public float attractionForce = 5f;
    public float attractionRadius = 5f;
    private Rigidbody rb;
    private bool is_see_player = false;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (is_see_player)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            transform.Translate(directionToPlayer * attractionForce * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < attractionRadius)
            {
                is_see_player = true;
            }
        }



    }
}

    

    

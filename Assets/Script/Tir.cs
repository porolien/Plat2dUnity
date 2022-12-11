using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    public Rigidbody2D rb;

    public int DestroyBullet = 5;

    private Transform targetPlayer;
    private Transform EnnemyTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EnnemyTransform = GetComponent<Transform>();
    }

    void Update()
    {
        //rb.AddForce(new Vector2(1, 0));
        if (targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        if ((targetPlayer.position.x  + targetPlayer.position.y <= 10) && ((targetPlayer.position.x < 10) && (targetPlayer.position.y < 10)))
        {
            

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {          
                Destroy(gameObject);
        }
        else
        {

        }
    }

   
}

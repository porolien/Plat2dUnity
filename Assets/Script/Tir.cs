using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    public Rigidbody2D rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        rb.AddForce(new Vector2(5, 2), ForceMode2D.Force);



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {          
                Destroy(gameObject);
        }
    }
}

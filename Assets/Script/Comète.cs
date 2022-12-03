using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comète : MonoBehaviour
{
    private Transform targetPlayer;
    private Transform CometTransform;
    private Rigidbody2D rb = null;
    public float SpawnRange;
    private bool hasExplose;
    private void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CometTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if ((( CometTransform.position.x - targetPlayer.position.x) < SpawnRange) && !hasExplose)
        {
            CometSpawn();  
        }
    }

    void CometSpawn()
    {
        rb.velocity = new Vector2(-15, -15);
    }

    void Explosion()
    {
        hasExplose = true;
        rb.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("wall");
            Explosion();
        }
    }
}

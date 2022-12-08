using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Comète : MonoBehaviour
{
    private Transform targetPlayer;
    private Transform CometTransform;
    private Rigidbody2D rb = null;
    public PlayerMovement playerMovement;
    public float SpawnRange;
    private bool hasExplose;
    public float speed;
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
        else if (hasExplose)
        {
            StartCoroutine(CometBurn());
        }
    }

    void CometSpawn()
    {
        rb.velocity = new Vector2(speed, speed);
    }

    void Explosion()
    {
        hasExplose = true;
        rb.velocity = new Vector2(0, 0);
    }

    IEnumerator CometBurn()
    {
        yield return new WaitForSeconds(3);
        Destroy (gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement.Die();
        }
        if (collision.gameObject.tag == "Wall")
        {
            
            Explosion();
        }
    }
}

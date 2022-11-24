using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    Rigidbody2D rb = null;
    public int Hp;
    public float speed = 0;
    SpriteRenderer renderer = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-1 * speed, 0);
        if (-1 * speed != 0)
        {
            renderer.flipX = -1 * speed < 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {

            Destroy(collision.gameObject);
            Hp = Hp - 1;
            if (Hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("yay");
            speed = speed * -1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.x < 0)
        {
            Debug.Log(collision.GetContact(0).normal.y);
            speed = speed * -1;
            renderer.flipX = -1 * speed < 0;
        }
    }
}

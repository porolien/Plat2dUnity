using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Ennemy
{
    private Transform targetPlayer;
    private Transform EnnemyTransform;
    private Rigidbody2D rb = null;
    public float speed = 0;
    private bool hole = false;
    private bool wall = false;
    private bool ChasePlayer = false;
    private bool WalkOnWall = false;
    SpriteRenderer renderer = null;
    // Start is called before the first frame update
    void Start()
    {
        EnnemyTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        if (ChasePlayer)
        {

            if (((targetPlayer.position.x < EnnemyTransform.position.x) && (targetPlayer.position.x - EnnemyTransform.position.x < -30)) || ((targetPlayer.position.x > EnnemyTransform.position.x) && (EnnemyTransform.position.x - targetPlayer.position.x < -30)))
            {
                ChasePlayer = false;
                speed = 5;
            }
            if (WalkOnWall)
            {
                rb.velocity = new Vector2(0, 5);
            }
            else
            {
                ChasePlayers();

            }
        }
        else
        {
            if (((targetPlayer.position.x < EnnemyTransform.position.x) && (targetPlayer.position.x - EnnemyTransform.position.x > -20)) || ((targetPlayer.position.x > EnnemyTransform.position.x) && (EnnemyTransform.position.x - targetPlayer.position.x > -20)))
            { 
                ChasePlayer = true;
            }
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (-1 * speed != 0)
        {
            renderer.flipX = speed < 0;
        }
    }
    void ChasePlayers()
    {
        speed = 15;
        if (targetPlayer.position.x < EnnemyTransform.position.x)
        {
            if (speed > 0)
            {
                speed = speed * -1;
            }
        }
        else
        {
            if (speed < 0)
            {
                speed = speed * -1;
            }
        }
        if (hole || wall)
        {

            rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
            hole = false;
            wall = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {

            TakeDamage(collision);
        }
        if (collision.tag == "Hole")
        {
            hole = true;
        }
        if (collision.tag == "Wall")
        {
            wall = true;
        }
        else
        {
            speed = speed * -1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.GetContact(0).normal.x > 0.5)
        {

            speed = speed * -1;
            renderer.flipX = -1 * speed < 0;
        }
        if (collision.GetContact(0).normal.x < -0.5)
        {

            speed = speed * -1;
            renderer.flipX = -1 * speed < 0;
        }
        if (((collision.GetContact(0).normal.x < 0) || (collision.GetContact(0).normal.x > 0)) && ChasePlayer)
        {

            WalkOnWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (WalkOnWall)
        {
            WalkOnWall = false;
        }
    }
}

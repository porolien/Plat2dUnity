using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunEnnemy : Ennemy
{
    private Rigidbody2D rb = null;
    private bool waitTimerBefore = false;
    private bool HowToMove = true;
    public float speed = 0;

    public Transform ProjectilePosition;
    public float AtkSpeedProjectile;
    public GameObject Projectile;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {   
        if (!waitTimerBefore)
        {
            StartCoroutine(TimerBeforeAtack());
        }
    }

    private void Attack()
    {
        GameObject newProjectile1 = Instantiate(Projectile, new Vector2(ProjectilePosition.position.x, ProjectilePosition.position.y), Quaternion.identity);
        newProjectile1.GetComponent<Rigidbody2D>().velocity = new Vector2(AtkSpeedProjectile * -1 , AtkSpeedProjectile * -1);
        newProjectile1.transform.rotation = Quaternion.Euler(0, 0, -45);
        GameObject newProjectile2 = Instantiate(Projectile, new Vector2(ProjectilePosition.position.x, ProjectilePosition.position.y), Quaternion.identity);
        newProjectile2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, AtkSpeedProjectile * -1);
        GameObject newProjectile3 = Instantiate(Projectile, new Vector2(ProjectilePosition.position.x, ProjectilePosition.position.y), Quaternion.identity);
        newProjectile3.GetComponent<Rigidbody2D>().velocity = new Vector2(AtkSpeedProjectile * 1, AtkSpeedProjectile * -1);
        newProjectile3.transform.rotation = Quaternion.Euler(0, 0, 45);
    }
    private void Move()
    {
            StartCoroutine(MovementPatern());
       
    }
    IEnumerator MovementPatern()
    {
        int movement = 1;
        if (HowToMove)
        {
            movement = -1;
            HowToMove = false;
        }
        else
        {
            HowToMove = true;
        }
        Attack();
        rb.velocity = new Vector2(movement * speed , -speed * 1.7f) ;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed * 1.25f, -speed * 1.53f);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed * 1.5f, -speed * 1.4f);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed * 1.75f, -speed * 1.25f);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed * 2, -speed * 1.1f);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed * 2, -speed * -1.1f);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed * 1.75f, -speed * -1.25f);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed * 1.5f, -speed * -1.4f);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed * 1.25f, -speed * -1.53f);
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(movement * speed , -speed * -1.7f);
        yield return new WaitForSeconds(0.2f);
        Attack();
        rb.velocity = new Vector2(0, 0);

    }
    IEnumerator TimerBeforeAtack()
    {
        waitTimerBefore = true;
        //yield return new WaitForSeconds(1);
       // Attack();
        yield return new WaitForSeconds(3);
        Move();
        yield return new WaitForSeconds(2);
        waitTimerBefore = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {

            TakeDamage(collision);
        }
    }
    }

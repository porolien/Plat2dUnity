using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShoot : Ennemy
{
    Transform EnnemyTransform;
    public Transform ProjectilePosition;
    public float AtkSpeedProjectile;
    public GameObject Projectile;
    private Transform targetPlayer;
    public float ymax = 15;
    private bool attackWaiting = false;
    void Start()
    {
        EnnemyTransform = GetComponent<Transform>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        int checkX = 0;
        int checkY = 0;
        if ((targetPlayer.position.x < EnnemyTransform.position.x) && (targetPlayer.position.x - EnnemyTransform.position.x > -ymax)) 
        {   
            checkX = -1;
        }
        if ((targetPlayer.position.x > EnnemyTransform.position.x) && (EnnemyTransform.position.x - targetPlayer.position.x > -ymax))
        {
            checkX = 1;
        }
        if ((targetPlayer.position.y < EnnemyTransform.position.y) && (targetPlayer.position.y - EnnemyTransform.position.y > -ymax))
        {
            checkY = 1;
        }
        else if ((targetPlayer.position.y > EnnemyTransform.position.y) && (EnnemyTransform.position.y - targetPlayer.position.y > -ymax))
        {
            checkY = -1;
        }
        if (checkX != 0 && checkY != 0 && attackWaiting == false)
        {
            Attack(checkX, checkY);
        }
        
    }
    

    private void Attack(int checkX, int checkY)
    {
        GameObject newProjectile = Instantiate(Projectile, new Vector2(ProjectilePosition.position.x, ProjectilePosition.position.y), Quaternion.identity);
        StartCoroutine(MortarProjectile(newProjectile, checkX, checkY));
        
        
    }
    private IEnumerator MortarProjectile(GameObject Projectile, int Direction, int checkY)
    {
        attackWaiting = true;
        float DifferenceYPosition = targetPlayer.position.y - EnnemyTransform.position.y;
        float DifferenceXPosition = targetPlayer.position.x - EnnemyTransform.position.x;
        float DifferenceYPlayerEnnemy = DifferenceYPosition / ymax; 
        float Range = (DifferenceYPlayerEnnemy * 0.33f) +1;
        float attackDuration = ((targetPlayer.position.x - EnnemyTransform.position.x) / 5) + 1;
        if(Direction < 0)
        {
            attackDuration = ((EnnemyTransform.position.x - targetPlayer.position.x) / 5) + 1;
        }
        float earlyDuration = (attackDuration / 6) * Range;
        float lastDuration = 1.33f - earlyDuration;
        float height = (25 + DifferenceYPosition ) / 2;
        float earlySpeedY = height * (3 * earlyDuration);
        float lastSpeedY = -(25 - height) * (3 * lastDuration);
        float earlySpeedX = DifferenceXPosition / attackDuration * earlyDuration;
        float lastSpeedX = DifferenceXPosition / attackDuration * lastDuration; 
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(earlySpeedX, earlySpeedY * 1.2f);
        yield return new WaitForSeconds(earlyDuration);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(earlySpeedX, earlySpeedY);
        yield return new WaitForSeconds(earlyDuration);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(earlySpeedX, earlySpeedY * 0.8f);
        yield return new WaitForSeconds(earlyDuration);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(lastSpeedX, lastSpeedY * 0.8f);
        yield return new WaitForSeconds(lastDuration);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(lastSpeedX, lastSpeedY);
        yield return new WaitForSeconds(lastDuration);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(lastSpeedX, lastSpeedY * 1.2f);
        yield return new WaitForSeconds(lastDuration);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        attackWaiting = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {

            if (collision.tag == "PlayerProjectile")
            {

                TakeDamage(collision);
            }
        }
    }
}

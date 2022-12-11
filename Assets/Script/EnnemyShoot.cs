using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShoot : MonoBehaviour
{


    public int Hp;

    private Transform targetPlayer;
    private Transform EnnemyTransform;
    private Rigidbody2D rb = null;
    public float speed = 0;
    private bool hole = false;
    private bool wall = false;
    private bool ChasePlayer = false;
    private bool WalkOnWall = false;
    SpriteRenderer renderer = null;
    public float AtkSpeedProjectile;
    public GameObject Projectile;
    public Transform ProjectilePosition;
    public int direction;
    private bool Tir = false; 

    void Start()
    {
        
    }


    void Update()
    {
        GameObject newProjectile = Instantiate(Projectile, new Vector2(ProjectilePosition.position.x, ProjectilePosition.position.y), Quaternion.identity);
        StartCoroutine(ChangeProjectileDirection(newProjectile, direction));

        StartCoroutine(Shoot());
      

    }


    IEnumerator Shoot()
    {
        if (((targetPlayer.position.x < EnnemyTransform.position.x) && (targetPlayer.position.x - EnnemyTransform.position.x < -10)) || ((targetPlayer.position.x > EnnemyTransform.position.x) && (EnnemyTransform.position.x - targetPlayer.position.x < -10)))
        {
            Tir = true;
            Debug.Log("Ahhhh");
        }

        if (Tir = true)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }


    private IEnumerator ChangeProjectileDirection(GameObject Projectile, int Direction)
    {
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(AtkSpeedProjectile * Direction * Time.fixedDeltaTime, 5f);
        Debug.Log(Projectile.transform.localScale.x * Direction + Projectile.transform.localScale.y + Projectile.transform.localScale.z);
        Projectile.transform.rotation = Quaternion.Euler(0, 0, 45);
        yield return new WaitForSeconds(0.4f);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(AtkSpeedProjectile * Direction * Time.fixedDeltaTime, 0f);
        Projectile.transform.rotation = Quaternion.Euler(0, 0, 18);
        yield return new WaitForSeconds(0.05f);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(AtkSpeedProjectile * Direction * Time.fixedDeltaTime, 0f);
        Projectile.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(0.05f);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(AtkSpeedProjectile * Direction * Time.fixedDeltaTime, 0f);
        Projectile.transform.rotation = Quaternion.Euler(0, 0, -18);
        yield return new WaitForSeconds(0.05f);
        Projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(AtkSpeedProjectile * Direction * Time.fixedDeltaTime, -5f);
        Projectile.transform.rotation = Quaternion.Euler(0, 0, -45);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Projectile")
        {

            Destroy(collision.gameObject);
            Hp = Hp - 1;
            if (Hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

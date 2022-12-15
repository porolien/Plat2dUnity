using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    
    Rigidbody2D rb = null;
    [SerializeField] private PlayerInput playerInput = null;
    public PlayerInput PlayerInput => playerInput;
    Transform myTransform;
    SpriteRenderer renderer = null;

    public DeadStoryteller DeadCounter;

    public Transform ProjectilePosition;
    public float AtkSpeedProjectile;
    public GameObject ProjectileSand;
    public GameObject ProjectileIce;
    public int mana;

    Vector2 movement = Vector2.zero;
    public float speed = 0;
    public float dashTime = 0.1f;
    public float test = 0;
    private int dashKeyRight;
    private int dashKeyLeft;
    private int dashKeyDown;
    private bool dashFinish = true;
    private bool hasJumped = true;
    private int ChangeJumpDirection = 0;
    private bool StopMovement;
    private bool JumpWall = false;
    private bool IsSliding = false;
    private bool IsBuried = false;
    private float SensMouvement;
    private bool frozen;
    private bool Moving = false;
    private int frozedCount = 0;
    private bool CanBreakIce = false;

    public Animator animator;


    //private bool IsWaiting
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      
       
        if (dashFinish == true)
        {
            if (dashKeyRight == 2)
            {
                rb.velocity = new Vector2(speed * 5, rb.velocity.y);
                StartCoroutine(DashTiming());
            }
            else if (dashKeyLeft == 2)
            {
                rb.velocity = new Vector2(speed * -5, rb.velocity.y);
                StartCoroutine(DashTiming());
            }
            else if (dashKeyDown == 2)
            {
                CanBreakIce = true;
                rb.velocity = new Vector2(rb.velocity.x, speed * -5);
                StartCoroutine(DashTiming());
            }
            else if (JumpWall)
            {
                rb.velocity = new Vector2(movement.x * speed, -5);
            }
            else if (StopMovement == false)
            {
                if (IsSliding)
                {

                    //rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
                    /*    int ChangeDirectionForce = 1;
                        if(speed < 0)
                        {
                            ChangeDirectionForce = -1;
                        }
                        rb.AddForce(new Vector2(5 * ChangeDirectionForce, 0), ForceMode2D.Impulse);
                        StartCoroutine(FrozedForces());
                    */
                    rb.velocity = new Vector2(movement.x * speed*1.2f, rb.velocity.y);
                }
                if (IsBuried)
                {
                    rb.velocity = new Vector2(movement.x * speed/3, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
                    if (movement.x != 0)
                    {
                        renderer.flipX = movement.x < 0;
                    }
                }
            }
        }

    }
    void OnJump(InputValue jumpValue)
    {
        if (!hasJumped)
        {
            Moving = true;
            if (IsBuried)
            {
                rb.velocity = new Vector2(rb.velocity.x, 1);
                rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
                Debug.Log(rb.velocity);
                hasJumped = true;
            }
            else if (ChangeJumpDirection == 0)
            {
                float pressed = jumpValue.Get<float>();

                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
                hasJumped = true;
                IsSliding = false;
            }
            
            else
            {
                StopMovement = true;
                JumpWall = false;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(ChangeJumpDirection * 10, 15), ForceMode2D.Impulse);
                renderer.flipX = ChangeJumpDirection < 0;
                hasJumped = true;
                ChangeJumpDirection = 0;
                StartCoroutine(StopMovementTiming());
            }
        }
    }
    private IEnumerator StopMovementTiming()
    {
        yield return new WaitForSeconds(0.3f);
        StopMovement = false;
    }
    void OnMovement(InputValue moveValue)
    {
        StartCoroutine(DashCoroutine(moveValue));
        if (IsSliding)
        {
              switch (moveValue.Get<Vector2>().x)
              {

                  case -1:
                    Moving = true;
                    frozedCount++;
                    speed = 10;
                      break;
                  case 0:
                    frozedCount++;
                    Moving = false;
                    if (SensMouvement != 0)
                    {
                        movement.x = SensMouvement;
                    }
                      break;
                  case 1:
                    Moving = true;
                    frozedCount++;
                    speed = 10;
                      break;
              }
            SensMouvement = moveValue.Get<Vector2>().x;
            if (Moving)
            {
                movement = moveValue.Get<Vector2>();
            }
            StartCoroutine(FrozedForces());
           
           
           // movement = moveValue.Get<Vector2>() * 1.5f;
            
        }
        else
        {
            movement = moveValue.Get<Vector2>();
        }
    }
    void OnAttack(InputValue attackValue)
    {

        if (mana != 0)
        {
            if (mana == 5 && !IsInvoking("RegenMana"))
            {
                InvokeRepeating("RegenMana", 2f, 2f);
            }
            int direction = 1;
            if (renderer.flipX)
            {
                direction = -1;
            }
            GameObject newProjectile = Instantiate(ProjectileSand, new Vector2(ProjectilePosition.position.x, ProjectilePosition.position.y), Quaternion.identity);
            StartCoroutine(ChangeProjectileDirection(newProjectile, direction));
            mana = mana - 1;
            Debug.Log(mana);
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


    void OnChangeDay()
    {

    }
    void RegenMana()
    {
        if (mana < 5)
        {
            mana = mana + 1;
        }
        else
        {
            CancelInvoke("RegenMana");
        }
    }
    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        DeadCounter.mort = DeadCounter.mort  +1;

        animator.SetBool(DeadCounter.mort, true);
    }
    private IEnumerator DashTiming()
    {
        dashFinish = false;
        yield return new WaitForSeconds(0.2f);
        dashFinish = true;
        dashKeyRight = 0;
        dashKeyLeft = 0;
        dashKeyDown = 0;
        CanBreakIce = false;
    }
    private IEnumerator DashCoroutine(InputValue moveValue)
    {
        if (moveValue.Get<Vector2>().x < 0)
        {
            dashKeyRight = 0;
            dashKeyDown = 0;
            dashKeyLeft++;
        }
        if (moveValue.Get<Vector2>().x > 0)
        {
            dashKeyLeft = 0;
            dashKeyDown = 0;
            dashKeyRight++;
        }
        if (moveValue.Get<Vector2>().y < 0)
        {
            dashKeyRight = 0;
            dashKeyLeft = 0;
            dashKeyDown++;
        }
        yield return new WaitForSeconds(0.3f);
        dashKeyRight = 0;
        dashKeyDown = 0;
        dashKeyLeft = 0;
    }
    private IEnumerator FrozedForces()
    {
        if (!frozen)
        {
            int thisFrozedCount = frozedCount; 
            float SlidingWhileNotMoving = 0.6f;
            frozen = true;
            if (Moving)
            {
                speed = 10;
                SlidingWhileNotMoving = 0f;
            }
            else
            {
                speed = 7;
            }
            
            yield return new WaitForSeconds(SlidingWhileNotMoving);
            if (Moving || frozedCount != thisFrozedCount)
            {
                SlidingWhileNotMoving = 0;
            }
            else
            {
                SlidingWhileNotMoving = 0.4f;
                speed = 5;
            }
            yield return new WaitForSeconds(SlidingWhileNotMoving);
            if (Moving || frozedCount != thisFrozedCount)
            {
                SlidingWhileNotMoving = 0;
            }
            else
            {
                SlidingWhileNotMoving = 0.2f;
                speed = 2;
            }
            yield return new WaitForSeconds(SlidingWhileNotMoving);
            if (Moving || frozedCount != thisFrozedCount)
            {
                SlidingWhileNotMoving = 0;
            }
            else
            {
                SlidingWhileNotMoving = 0f;
                speed = 0;
            }
            frozen = false;
        }
        
    }
    private IEnumerator ExitSlindingTiming()
    {
        yield return new WaitForSeconds(0.2f);
        if (frozen)
        {
            IsSliding = false;
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsSliding = false;
        if(collision.gameObject.tag == "BreakableIce" && CanBreakIce)
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.name == "FrozedGround")
        {
            IsSliding = true;
        }

        if (collision.gameObject.tag == "Ennemy")
        {
            Die();
        }
        else
        {
            if (collision.GetContact(0).normal.y > 0.8f)
            {
                hasJumped = false;
                ChangeJumpDirection = 0;
                JumpWall = false;
            }
            else if (collision.GetContact(0).normal.x < 0 && !(collision.GetContact(0).normal.y > 0.8f))
            {
                hasJumped = false;
                ChangeJumpDirection = -1;
                JumpWall = true;
            }
            else if (collision.GetContact(0).normal.x > 0 && !(collision.GetContact(0).normal.y > 0.8f))
            {
                hasJumped = false;
                ChangeJumpDirection = 1;
                JumpWall = true;
            }
        }

        if (collision.gameObject.tag == "Projectile")
        {
            Die();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "FrozedGround")
        {
            StartCoroutine(ExitSlindingTiming());
            
        }
    }
    private void OnTriggerEnter2D(Collider2D triggered)
    {
        if (triggered.gameObject.name == "Quicksands")
        {
            hasJumped = false;
            IsBuried = true;
            InvokeRepeating("JumpBuried", 0.2f, 0.2f);
        }
    }
    private void OnTriggerExit2D(Collider2D triggered)
    {
        Debug.Log("sands");
        if (triggered.gameObject.name == "Quicksands")
        { 
            IsBuried = false;
            CancelInvoke();
        }
    }

    private void JumpBuried()
    {
        if (IsBuried)
        {
            hasJumped = false;
        }
    }

}

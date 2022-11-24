using System.Collections;
using System.Collections.Generic;
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

    public Transform ProjectilePosition;
    public float AtkSpeedProjectile;
    public GameObject ProjectileSand;
    public GameObject ProjectileIce;
    public int mana;

    Vector2 movement = Vector2.zero;
    public float speed = 0;
    public float dashTime = 0.1f;
    private int dashKeyRight;
    private int dashKeyLeft;
    private bool dashFinish = true;
    private bool hasJumped = true;
    private int ChangeJumpDirection = 0;
    private bool StopMovement;
    private bool JumpWall = false;
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
                rb.velocity = new Vector2(speed * -5 , rb.velocity.y);
                Debug.Log(rb.velocity);
                StartCoroutine(DashTiming());
            }
            else if (JumpWall)
            {
                rb.velocity = new Vector2(movement.x * speed, -);
            }
            else if (StopMovement == false)
            {
                rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
                if (movement.x != 0)
                {
                    renderer.flipX = movement.x < 0;
                }
            }
        }
        
    }
    void OnJump(InputValue jumpValue)
    {
        if (!hasJumped)
        {
            if (ChangeJumpDirection == 0)
            {
                float pressed = jumpValue.Get<float>();

                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
                hasJumped = true;
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
    void OnMovement(InputValue moveValue)
    {
        StartCoroutine(DashCoroutine(moveValue));
        movement = moveValue.Get<Vector2>();
    }
    void OnAttack(InputValue attackValue)
    {
        
        if (mana != 0)
        {
            if (mana == 5 && !IsInvoking("RegenMana"))
            {
                InvokeRepeating("RegenMana", 2f, 2f);
            }
            int direction()
            {
                if (transform.localScale.x < 0f)
                {
                    return -1;
                }
                else
                {
                    return +1;
                }
            }
            GameObject newprojectile = Instantiate(ProjectileSand, ProjectilePosition.position, Quaternion.identity);
            newprojectile.GetComponent<Rigidbody2D>().velocity = new Vector2(AtkSpeedProjectile * direction() * Time.fixedDeltaTime, 0f);
            newprojectile.transform.localScale = new Vector2(newprojectile.transform.localScale.x * direction(), newprojectile.transform.localScale.y);
            mana = mana -1;
            Debug.Log(mana);
        }
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
    void Die() 
    {
        Debug.Log("ouch");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator DashTiming()
    {
        dashFinish = false;
        yield return new WaitForSeconds(0.2f);
        dashFinish = true;
        dashKeyRight = 0;
        dashKeyLeft = 0;
    }
    private IEnumerator DashCoroutine(InputValue moveValue)
    {
        if (moveValue.Get<Vector2>().x < 0)
        {
            dashKeyRight = 0;
            dashKeyLeft++;
        }
        if (moveValue.Get<Vector2>().x > 0)
        {
            dashKeyLeft = 0;
            dashKeyRight++;
        }
            yield return new WaitForSeconds(0.3f);
        dashKeyRight = 0;
        dashKeyLeft = 0;
    }
    private IEnumerator StopMovementTiming()
    {
        yield return new WaitForSeconds(0.3f);
        StopMovement = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
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

    }

}

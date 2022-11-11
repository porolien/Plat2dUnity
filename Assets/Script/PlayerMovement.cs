using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb = null;
    [SerializeField] private PlayerInput playerInput = null;
    public PlayerInput PlayerInput => playerInput;
    Vector2 movement = Vector2.zero;
    public float speed = 0;
    public float dashTime = 0.1f; 
    private int dashKeyRight;
    private int dashKeyLeft;
    private bool dashFinish = true;
    private bool hasJumped;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
            else
            {
                rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
            }
        }
        
    }
    void OnJump(InputValue jumpValue)
    {
        if (!hasJumped)
        {
            float pressed = jumpValue.Get<float>();

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            hasJumped = true;
        }
    }
    void OnMovement(InputValue moveValue)
    {
        StartCoroutine(DashCoroutine(moveValue));
        movement = moveValue.Get<Vector2>();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.8f)
        {
            hasJumped = false;
        }

    }

}

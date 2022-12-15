using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float fallDelay = 1f;
    private bool isFalling = false;
    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetContact(0).normal.y < -0.8)
        {
            Invoke("Fall", fallDelay);
            Debug.Log(other.GetContact(0).normal.y);
        }

        if (isFalling && (other.gameObject.tag != "Player" && other.gameObject.tag != "Ennemy"))
        {
            isFalling = false;
            Destroy(gameObject);
        }
    }

    void Fall()
    {
        isFalling = true;
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.velocity = new Vector2(0f, -3);
    }
}

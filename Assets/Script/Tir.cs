using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    Rigidbody rb;
    public void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(10, 15), ForceMode2D.Impulse);
    }

    private void Update()
    {

    }
}
        
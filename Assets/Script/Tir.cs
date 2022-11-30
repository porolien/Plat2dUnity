using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    Rigidbody rb;
    public int H;
    public int L;
    [SerializeField] private bool bullet;


    public void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(H, L), ForceMode2D.Impulse);
        
    }

    private void Update()
    {

    }
}
        
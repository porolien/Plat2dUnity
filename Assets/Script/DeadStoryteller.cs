using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeadStoryteller : MonoBehaviour
{
    public int mort = 0;

    public GameObject Die;

    public TMP_Text CounterDead;

    void Start()
    {

    }
    void Update()
    {
        CounterDead.text =""+ mort;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            mort++;      
        }
    }
}

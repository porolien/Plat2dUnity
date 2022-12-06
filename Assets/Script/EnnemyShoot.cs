using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShoot : MonoBehaviour
{

    public int Hp;

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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

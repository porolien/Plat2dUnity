using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    protected int Hp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected void TakeDamage(Collider2D TheCollision)
    {
        Destroy(TheCollision.gameObject);
        Hp = Hp - 1;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

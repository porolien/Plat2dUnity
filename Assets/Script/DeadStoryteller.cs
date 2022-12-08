using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeadStoryteller : MonoBehaviour
{
    public int mort = 0;

    public TMP_Text CounterDead;

    Animation animation;

    void Start()
    {
        animation = GetComponent<Animation>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("ui");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("treg");

    }
    void Update()
    {
        CounterDead.text =""+ mort;

       
    }
}

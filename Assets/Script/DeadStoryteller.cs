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
        CounterDead.text = "" + PlayerPrefs.GetInt("deathCount");

    }
    public void ShowDeath()
    {
        PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount") + 1);
        CounterDead.text = "" + PlayerPrefs.GetInt("deathCount");
    }
}

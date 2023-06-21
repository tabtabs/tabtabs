using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timebar : MonoBehaviour
{
    Animator anim;
    public float timeer;
    void Start()
    {
        anim = GetComponent<Animator>();
        timeer = 9.0f;
    }

    
    void Update()
    {
        timeer -= Time.deltaTime;

        if (timeer<=13.0f&&timeer>=12.0f)
        {
            anim.SetTrigger("Timebar12");
        }
        else if (timeer<=12.0f&&timeer>=11.0f)
        {
            anim.SetTrigger("Timebar11");
        }
        else if (timeer<=11.0f&&timeer>=10.0f)
        {
            anim.SetTrigger("Timebar10");
        }
        else if (timeer<=10.0f&&timeer>=9.0f)
        {
            anim.SetTrigger("Timebar9");
        }
        else if (timeer <= 9.0f && timeer >= 8.0f)
        {
            anim.SetTrigger("Timebar8");
        }
        else if (timeer <= 8.0f && timeer >= 7.0f)
        {
            anim.SetTrigger("Timebar7");
        }
        else if (timeer <= 7.0f && timeer >= 6.0f)
        {
            anim.SetTrigger("Timebar6");
        }
        else if (timeer <= 6.0f && timeer >= 5.0f)
        {
            anim.SetTrigger("Timebar5");
        }
        else if (timeer <= 5.0f && timeer >= 4.0f)
        {
            anim.SetTrigger("Timebar4");
        }
        else if (timeer <= 4.0f && timeer >= 3.0f)
        {
            anim.SetTrigger("Timebar3");
        }
        else if (timeer <= 3.0f && timeer >= 2.0f)
        {
            anim.SetTrigger("Timebar2");
        }
        else if (timeer <= 2.0f && timeer >= 1.0f)
        {
            anim.SetTrigger("Timebar1");
        }
        else if (timeer <= 1.0f && timeer >= 0.0f)
        {
            anim.SetTrigger("Timebar0");
        }
    }
}


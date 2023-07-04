using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{






    public virtual void DoSomething()
    {
        Debug.Log("Hello");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        DoSomething();
    }


}

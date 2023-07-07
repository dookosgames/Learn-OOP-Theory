using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Enemy : MonoBehaviour
{

    public float _MoveSpeed;

    [Header("Amount this enemy damages")]
    [SerializeField] float _DamageAmount;

  
    public virtual void MoveUp()
    {
        transform.position += transform.up*_MoveSpeed*Time.deltaTime;
    }
    public virtual void MoveRight()
    {
        transform.position += transform.right * _MoveSpeed * Time.deltaTime;
    }
    public virtual void MoveLeft()
    {
        transform.position += -transform.right * _MoveSpeed * Time.deltaTime;
    }


    //Despawn

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Vehicles>(out Vehicles comp))
        {
            comp.Damage(_DamageAmount);
        }
    }

    
}

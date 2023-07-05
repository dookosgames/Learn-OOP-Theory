using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float _MoveSpeed;

  
    public virtual void MoveUp()
    {
        transform.position += transform.up*_MoveSpeed*Time.deltaTime;
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Vehicles>())
        {
            Debug.Log("Player hit");
        }
    }

}

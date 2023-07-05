using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Fish : Enemy
{
    [SerializeField] float _RightMax;
    [SerializeField] float _LeftMax;


    private void Update()
    {
        MoveUp();
    }


    //Add side to side motion to up movement
    public override void MoveUp()
    {
        //Move up
        base.MoveUp();


        float randomMove = Random.Range(_LeftMax, _RightMax);
        ////Add right and left movemnt too
        //transform.position += transform.right * randomMove * Time.deltaTime;

        transform.position = transform.position + transform.right*Mathf.Sin(Time.time*_LeftMax)*_RightMax;
    }
}

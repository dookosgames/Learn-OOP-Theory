using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;



//INHERITANCE
public class Fish : Enemy
{
    [SerializeField] float _distanceToMove;
    [SerializeField] float _ocilationSpeed;


    private void Update()
    {
        MoveUp();
    }


    //POLYMORPHISM
    //Add side to side motion to up movement
    public override void MoveUp()
    {
        //Move up
        base.MoveUp();

        ////Add right and left movemnt too
        //transform.position += transform.right * randomMove * Time.deltaTime;

        transform.position =transform.position+ transform.right*Mathf.Cos(Time.time*RandomSpeed())*RandomMag();



        //ABSTRACTION
        //Funcitons to get random floats for COS
        float RandomSpeed() { float speed = Random.Range(-_ocilationSpeed, _ocilationSpeed); return speed; }
        float RandomMag() { float mag = Random.Range(-_distanceToMove, _distanceToMove); return mag; }
    }

}

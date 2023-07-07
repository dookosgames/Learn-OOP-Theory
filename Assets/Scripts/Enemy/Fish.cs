using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;



//INHERITANCE
public class Fish : Enemy
{
    [SerializeField] float _distanceToMove;
    [SerializeField] float _ocilationSpeed;
    [SerializeField] float _moveSpeedRange;

    private void Start()
    {
        //Funcitons to get random floats for COS
        RandomizeSpeed();
        _ocilationSpeed= Random.Range(-_ocilationSpeed, _ocilationSpeed);
        _distanceToMove = Random.Range(-_distanceToMove, _distanceToMove);
    }

    private void Update()
    {
        MoveRight();
    }
    

    //POLYMORPHISM
    //Adds up wobble to motion
    public override void MoveRight()
    {
        base.MoveRight();

        transform.position = transform.position + transform.up * Mathf.Sin(Time.time * _ocilationSpeed) * _distanceToMove;

    }

    //Randomize Move Speed from Parent classe "Enemy"
    private void RandomizeSpeed()
    {
        _MoveSpeed = Random.Range(0, _moveSpeedRange);
    }

}

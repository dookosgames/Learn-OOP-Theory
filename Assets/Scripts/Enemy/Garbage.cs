using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : Enemy
{

    [SerializeField] float _moveSpeedRange;
    [SerializeField] float _RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Funcitons to get random floats for COS
        RandomizeSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUp();
    }


    public override void MoveUp()
    {
        base.MoveUp();

        //rotate the garbage
        transform.Rotate(0, 0,_RotationSpeed);
    }




    //Randomize Move Speed from Parent classe "Enemy"
    private void RandomizeSpeed()
    {
        _MoveSpeed = Random.Range(0, _moveSpeedRange);
        _RotationSpeed = Random.Range(-_RotationSpeed, _RotationSpeed);
    }
}

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



//37 feet per 1 unity measure

enum HullType
{
    sphere,cylinder,rectangle
}

public class Vehicles : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BackgroundScroller bgScroll;

    [Header("Basic Specs")]
    [SerializeField] float _health;
    [SerializeField] float _mass;
    [SerializeField] float _hullStrength;
    [SerializeField] float _currentDepth;


    [Header("Hull Specs")]
    [SerializeField] HullType _hullType;

    [Header ("Cosmetics")]
    [SerializeField] Sprite _designSprite;
    [SerializeField] TextMeshProUGUI _depthSubDisplay;
    [SerializeField] ParticleSystem _Bubble1;
    [SerializeField] ParticleSystem _Bubble2;
    [SerializeField] ParticleSystem _Bubble3;


    public float GetHealth { get=> _health; }
    public float GetMass { get=>_mass;}
    public Sprite GetDesignSprite { get => _designSprite; }


    //physic stats    
    private float _psiPerFoot = .445f;
    private float _startingPsi = 14.7f;
    

  
    private void Update()
    {

        //Input for drop

        if (Input.GetKeyDown(KeyCode.Space)) { gameObject.GetComponent<Rigidbody2D>().gravityScale = 1; }


        if (gameObject.transform.position.y > 0) { return; }

        _currentDepth = (gameObject.GetComponent<SpriteRenderer>().bounds.center.y + (bgScroll._bgCounter * (bgScroll.yLength - bgScroll._startPos.y))) * bgScroll._feetPerUnit; //gets the current depth of the sub by checking for the bg tile currently displayed and the Y value
        _depthSubDisplay.text = _currentDepth.ToString("00");
       
        
      

    }

    //Amount of damage the hull takes based on the depth of the sub
    public void HullIntegrityLoss(float depth)
    {   
        _hullStrength -=  -depth * _psiPerFoot;
    }

  

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            //hull starts to take damage once in water
            HullIntegrityLoss(_currentDepth);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterSurface"))
        {
            //bubbles begin
            _Bubble1.Play();
            _Bubble2.Play();
            _Bubble3.Play();
        }
    }

}

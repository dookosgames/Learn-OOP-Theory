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

    public float GetHealth { get=> _health; }
    public float GetMass { get=>_mass;}
    public Sprite GetDesignSprite { get => _designSprite; }


    //physic stats    
    private float _psiPerFoot = .445f;
    private float _startingPsi = 14.7f;
    private float _feetPerUnity = 37f;


    

    private void Update()
    {
        //tracks the current depth of the sub
        if (transform.position.y < 0)
        {
            _currentDepth = -transform.position.y * _feetPerUnity;
            _depthSubDisplay.text = _currentDepth.ToString("00");
        }
        
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
            HullIntegrityLoss(_currentDepth);
        }
    }


    
}

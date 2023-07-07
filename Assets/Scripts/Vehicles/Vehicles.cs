
using System;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;



//37 feet per 1 unity measure

public class Vehicles : MonoBehaviour
{

    [Header("References")]
    [SerializeField] BackgroundScroller bgScroll;
    [SerializeField] Rigidbody2D rb;

    [Header("Basic Specs")]
    [SerializeField] float _health;
    [SerializeField] float _mass;
    [SerializeField] float _hullStrength;
    [SerializeField] float _currentDepth;

    [Header ("Cosmetics")]
    [SerializeField] Sprite _designSprite;
    [SerializeField] TextMeshProUGUI _depthSubDisplay;
    [SerializeField] ParticleSystem _Bubble1;
    [SerializeField] ParticleSystem _Bubble2;
    [SerializeField] ParticleSystem _Bubble3;

    [Header("Movement")]
    [SerializeField] float _moveSpeed;

    //GETS
    public float GetHealth { get=> _health; }
    public float GetHull { get => _hullStrength; }
    public float GetMass { get=>_mass;}
    public Sprite GetDesignSprite { get => _designSprite; }
    
   

    //physic stats    
    private float _psiPerFoot = .445f;
    private float _startingPsi = 14.7f;


    private void OnEnable()
    {
        GameManager.A_GameState += DropSub;
    }

    private void OnDisable()
    {
        GameManager.A_GameState -= DropSub;
    }


    //listens for game to be in play to drop sub
    private void DropSub(GameState state)
    {
        if (state == GameState.playing) { gameObject.GetComponent<Rigidbody2D>().gravityScale = 1; }
        else { gameObject.GetComponent<Rigidbody2D>().gravityScale = 0; }

    }

    private void Update()
    {

        if (gameObject.transform.position.y > 0) { return; }

        _currentDepth = (gameObject.GetComponent<SpriteRenderer>().bounds.center.y + (bgScroll._bgCounter * (bgScroll.yLength - bgScroll._startPos.y))) * bgScroll._feetPerUnit; //gets the current depth of the sub by checking for the bg tile currently displayed and the Y value
        _depthSubDisplay.text = _currentDepth.ToString("00");


        //Input to detect Movement
        if (Input.GetKeyDown(KeyCode.A)) { MoveLeft(); }
        else if (Input.GetKeyDown(KeyCode.D)) { MoveRight(); }

    }



    //Damage Taken
    public void Damage(float dam)
    {
        if (_hullStrength > 0) { _hullStrength -= dam; }
        else if (_health > 0) { _health -= dam; }

        if (_health<=0) {  }
    }


    //ABSTRACTION
    //Left Right Move
    public virtual void MoveRight(){ rb.AddForce(Vector2.right*_moveSpeed,ForceMode2D.Impulse); }
    public virtual void MoveLeft() { rb.AddForce(Vector2.left * _moveSpeed, ForceMode2D.Impulse); }

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

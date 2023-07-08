
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

    //ENCAPSULATION
    public float GetHealth { get=> _health; }
    public float GetHull { get => _hullStrength; }
    public float GetMass { get=>_mass;}
    public Sprite GetDesignSprite { get => _designSprite; }


    //Action: Player Health 0
    public static event Action a_PlayerHealth;

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

        //Input to detect Movement
        if (Input.GetKeyDown(KeyCode.A)) { MoveLeft(); }
        else if (Input.GetKeyDown(KeyCode.D)) { MoveRight(); }

        //keep player in bounds
        if (transform.position.x < -3) { transform.position = new Vector2(-3, transform.position.y); }
        if (transform.position.x> 3) { transform.position = new Vector2(3, transform.position.y); }

    }



    //Damage Taken
    public void Damage(float dam)
    {
        if (_hullStrength > 0) { _hullStrength -= dam; }
        else if (_hullStrength<=0) { _health -= dam; }

        //Player health zero
        if (_health<=0) { a_PlayerHealth.Invoke(); }
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

        if (collision.CompareTag("ObstacleBarrier"))
        {
            Debug.Log("in barrier");

            if (transform.position.x < 0) { rb.AddForce(Vector2.right * _moveSpeed * 2f, ForceMode2D.Impulse); }
            if (transform.position.x > 0) { rb.AddForce(Vector2.left * _moveSpeed *2f, ForceMode2D.Impulse); }
        }
       

    }



}

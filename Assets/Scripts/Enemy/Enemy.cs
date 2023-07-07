
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public float _MoveSpeed;

    [Header("Amount this enemy damages")]
    [SerializeField] float _DamageAmount;


    private void OnEnable()
    {
        GameManager.A_GameState += DespawnEnemy;
    }
    private void OnDisable()
    {
        GameManager.A_GameState -= DespawnEnemy;
    }

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


    //Destroy enemies if game is over
    private void DespawnEnemy(GameState state)
    {
        //destroy if gamestate is GameOver
        if (state == GameState.gameover ) { Destroy(gameObject); }
        
    }

    //Despawn

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Vehicles>(out Vehicles comp))
        {
            comp.Damage(_DamageAmount);
            Destroy(gameObject);
        }
    }

    
}

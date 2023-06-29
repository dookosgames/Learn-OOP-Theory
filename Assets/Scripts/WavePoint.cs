using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class WavePoint : MonoBehaviour
{
    public float velocity=0f;
    public float height=0f;
    public float force=0f;
    public float targetHeight;

    [SerializeField] float resistance;

    public int waveIndex;

    private SpriteShapeController ss_Controller;
    

    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        height = transform.localPosition.y;

        //maximum movement
        var x = height- targetHeight;
        var loss = -dampening * velocity;

        force = -springStiffness * x + loss;

        velocity += force;

        var y = transform.localPosition.y;

        transform.localPosition = new Vector3(transform.localPosition.x, y+velocity, transform.localPosition.z);
    }

    public void Init(SpriteShapeController ssc)
    {
        var index = transform.GetSiblingIndex();
        waveIndex = index + 1;
        ss_Controller = ssc;

        velocity = 0;
        height = transform.localPosition.y;
        targetHeight = transform.localPosition.y;        
    }

    public void WavePointUpdate()
    {
        if (ss_Controller != null)
        {
            Spline waterSpline = ss_Controller.spline;
            Vector3 wavePosition = waterSpline.GetPosition(waveIndex);
            waterSpline.SetPosition(waveIndex, new Vector3(wavePosition.x, transform.localPosition.y, wavePosition.z));
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FallingObject"))
        {
            Rigidbody2D fallingObj = other.gameObject.GetComponent<Rigidbody2D>();

            var speed = fallingObj.velocity;
            velocity += speed.y/resistance;
        }
    }

    
  
}

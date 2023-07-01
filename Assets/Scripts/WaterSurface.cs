using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    [SerializeField] ParticleSystem _Splash;
    [SerializeField] ParticleSystem _Splash2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingObject"))
        {
            _Splash.Play();
            _Splash2.Play();
        }
    }
}

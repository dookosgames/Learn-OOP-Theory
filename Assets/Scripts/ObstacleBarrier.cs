using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//INHERITANCE
public class ObstacleBarrier : MonoBehaviour
{

    [SerializeField] CameraFollow camFollow;

    [SerializeField] float yOffset;


    // Update is called once per frame
    void Update()
    {
        ReadjustBarrier();
    }

    //Readjust position to match player/camera movement
    private void ReadjustBarrier()
    {
        transform.position=new Vector2(transform.position.x,camFollow.cam.ViewportToWorldPoint(new Vector3(1, 1, 0)).y+yOffset);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy obj))
        {
            Destroy(obj.gameObject);
        }
    }
}

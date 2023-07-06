using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject _player { get; private set; }
    public Camera cam { get; private set; }

    
    public float cameraWidthL { get; private set; }
    public float cameraWidthR { get; private set; }


    private void OnEnable()
    {
        SubSelectManager.a_ActiveSub += SetSubToFollow;
    }
    private void OnDisable()
    {
        SubSelectManager.a_ActiveSub -= SetSubToFollow;
    }


    private void Start()
    {
        _player = null;
        cam = gameObject.GetComponent<Camera>();
        FindViewBoundry();
    }

    private void Update()
    {

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, _player.transform.position.y, gameObject.transform.position.z);
    }


    //Sets Which sub camer needs to follow
    private void SetSubToFollow(GameObject sub)
    {
        _player = sub;
    }


    //Finds the boundries of the camera to be used to determine where to spawn enemies
    void FindViewBoundry()
    {
        cameraWidthL = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        cameraWidthR= cam.ViewportToWorldPoint(new Vector3(1,0, 0)).x;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _player=null;
    [SerializeField] float zOffset;



    private void OnEnable()
    {
        SubSelectManager.a_ActiveSub += SetSubToFollow;
    }
    private void OnDisable()
    {
        SubSelectManager.a_ActiveSub -= SetSubToFollow;
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



}

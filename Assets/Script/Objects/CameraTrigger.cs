using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] CameraManager.CamName camName;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
            CameraManager.I.MoveCamera(camName);
    }
}

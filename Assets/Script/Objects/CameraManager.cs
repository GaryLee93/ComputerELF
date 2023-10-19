using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Serializable] public enum CamName{MainCam,BinRoomCam,DecRoomCam}
    [Serializable] public struct Cam
    {
        [SerializeField] public CamName name;
        [SerializeField] public Vector3 position;
    }
    [SerializeField] List<Cam> cameras;
    [SerializeField] GameObject mainCam;
    [SerializeField] float moveSpeed;
    private Vector3 destination,original;
    private bool move = false;
    private bool reset = false;
    private float lerpPct;
    public void MoveCamera(CamName name)
    {
        if(name==CamName.MainCam && !mainCam.GetComponent<MainCamera>().follow)
        {
            reset = true;
            original = mainCam.transform.position;
            lerpPct = 0;
            return;
        }
        foreach(Cam c in cameras)
        {
            if(c.name == name)
            {
                mainCam.GetComponent<MainCamera>().follow = false;
                move = true;
                destination = c.position;
                original = mainCam.transform.position;
                lerpPct = 0;
                break;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if(move)
        {
            mainCam.transform.position = Vector3.Lerp(original,destination,lerpPct);
            lerpPct += Time.fixedDeltaTime*moveSpeed*Time.fixedDeltaTime;
            if(lerpPct>=1f)
            {
                mainCam.transform.position = destination;
                move = false;
            }
        }
        else if(reset)
        {
            Vector3 dest = new(Extensions.GetPlayer().x,Extensions.GetPlayer().y,-10);
            mainCam.transform.position = Vector3.Lerp(original,dest,lerpPct);
            lerpPct += Time.fixedDeltaTime*moveSpeed*Time.fixedDeltaTime;
            if(lerpPct>=1f)
            {
                mainCam.GetComponent<MainCamera>().follow = true;
                reset = false;
            }
        }
    }
}

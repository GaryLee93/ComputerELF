using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public bool follow;
    void Update()
    {
        if(follow)
        {
            float x = Extensions.GetPlayer().x;
            float y = Extensions.GetPlayer().y;
            float z = -10;
            transform.position = new(x,y,z);
        }
    }
}

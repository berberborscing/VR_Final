using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject dayNightHandler;
    public GameObject lampObject;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //0 = Noon, 180 = midnight
        time = dayNightHandler.transform.rotation.z;
        bool flag = false;
        if(time > 0.75 || time < -0.75)
            flag = true;
        
        lampObject.SetActive(flag);
    }
}

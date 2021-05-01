using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public Transform Target;
    public float followspeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.rotation = Quaternion.Lerp(transform.rotation , Target.rotation , followspeed);
        gameObject.transform.position = Vector3.Lerp(transform.position , Target.position , followspeed);
        //gameObject.transform.LookAt(Target);
    }
}

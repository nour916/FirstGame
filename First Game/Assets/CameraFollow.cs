using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform astroTransform;
    Vector3 range;
    
    void Awake()
    {
        range = transform.position - astroTransform.position; 
    }

    
    void Update()
    {
        transform.position = new Vector3(range.x + astroTransform.position.x, transform.position.y);
    }
}

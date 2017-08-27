using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public float angle;
    public float move;
    
	void Update ()
    {
        transform.Rotate(Vector3.forward, angle);
        transform.Translate(Vector3.right * move, Space.World);
	}
}

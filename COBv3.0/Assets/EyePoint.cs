using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePoint : MonoBehaviour {

    public bool User=false;
    public GameObject idleForm;
    private Vector3 initialPosition;
	void Awake () {
        initialPosition = transform.position;
    }
	void Update () {
        if (transform.position != initialPosition)
        {
            User = true;
        }
        else
        {
            User = false;
        }
        idleForm.GetComponent<IdleForm>().activate = User;

    }
}

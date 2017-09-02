using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HandElastic : MonoBehaviour {
    public Transform target;

	void Update () {
        AjustRotation();
    }
    [ContextMenu("AjustRotation()")]
    public void AjustRotation()
    {
        transform.LookAt(target);
    }
}

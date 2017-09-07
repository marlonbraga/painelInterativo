using UnityEngine;

public class HandElastic : MonoBehaviour {
    public Transform target;

	void Update () {
        AjustRotation();
    }
    
    public void AjustRotation()
    {
        transform.LookAt(target);
    }
}

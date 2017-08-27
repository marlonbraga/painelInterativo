using UnityEngine;
using System.Collections;

public class skyBoxCam : MonoBehaviour
{
    public GameObject PlayerCamera; public float FOV = 60.0f;
    public float scaleFloat = 0.02f;
    // LateUpdate is for camera functions - REMEMBER THIS!! 
    void LateUpdate()
    {
        transform.rotation = PlayerCamera.transform.rotation;
        transform.position = new Vector3(PlayerCamera.transform.position.x * scaleFloat -100, PlayerCamera.transform.position.y * scaleFloat -100, PlayerCamera.transform.position.z * scaleFloat -100);
    }

}

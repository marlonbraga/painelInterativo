using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderEffect : MonoBehaviour
{
    public GameObject[] TentacleRig;
    private Vector3 lastPosition;
    private float lastVelocity;

    public float x;
    public float velocity;
    public float RescaleFactor = 0.01f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < TentacleRig.Length; i++)
        {
            GetComponent<LineRenderer>().SetPosition(i, TentacleRig[i].transform.position);

        }

        //ALONGAR BRAÇOS
        velocity = (transform.parent.position - lastPosition).magnitude / Time.deltaTime;//Faz a velocidade.
        lastPosition = transform.parent.position;//Armazena a última posição


        if (velocity > x)
        {
            Debug.Log("High Speed");
            Vector3 V = transform.localScale;

            if (transform.localScale.y > 0.1f)
            {
                Debug.Log("HIGH transform.localScale.y: " + transform.localScale.y);
                //transform.localScale = new Vector3(V.x, 0.09f, V.z);
            }
            else
            {
                transform.localScale = new Vector3(V.x, V.z + RescaleFactor, V.z);
            }
        }
        else
        {
            Debug.Log("Low Speed");
            Vector3 V = transform.localScale;
            
            if (transform.localScale.y < 0.01f)
            {
                Debug.Log("LOW transform.localScale.y: " + transform.localScale.y);
                transform.localScale = new Vector3(V.x, 0.01f, V.z);
            }
            else
            {
                transform.localScale = new Vector3(V.x, V.z - RescaleFactor, V.z);
            }
        }
        

        //if (velocity > lastVelocity)//Checa se ele está se movendo.
 

        lastVelocity = velocity;//Salva a última velocidade.
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineRenderEffect : MonoBehaviour
{
    [SerializeField]
    private int numOfVertices;
    [SerializeField]
    private GameObject pointTarget;
    [SerializeField]
    private GameObject tentacleJoint;

    private List<GameObject> tentacleRig = new List<GameObject>();
    private List<GameObject> pointsTarget = new List<GameObject>();
    private Vector3 lastPosition;
    private float lastVelocity;
    private GameObject lastJoint;
    private Vector3 firstJoint;

    public float x;
    public float velocity;
    public float scaleObject = 0.01f;

    private void Start()
    {
        for (int i = 0; i < numOfVertices; i++)
        {
            var pt = Instantiate(pointTarget);
            pt.name = "PointTarget" + i;
            pt.transform.parent = transform;
            pt.transform.localPosition = new Vector3(0, i * 0.1f, 0);
            pointsTarget.Add(pt);

            var tj = Instantiate(tentacleJoint);
            tj.name = "TentacleJoint" + i;
            tj.GetComponent<TentacleAvatar>().TentacleTarget = pt;
            tj.GetComponent<TentacleAvatar>().Speed = i / 10;
            tentacleRig.Add(tj);

            if (i == 0)
            {
                firstJoint = pt.transform.localPosition;
            }

            if (i == numOfVertices - 1)
            {
                lastJoint = pt;
            }
        }
        GetComponent<LineRenderer>().positionCount = numOfVertices;
    }

    void Update()
    {
        for (int i = 0; i < tentacleRig.Count; i++)
        {
            GetComponent<LineRenderer>().SetPosition(i, tentacleRig[i].transform.position);
        }

        //ALONGAR BRAÇOS
        velocity = (transform.parent.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.parent.position;

        if (velocity > x)
        {
            for (int i = 0; i < tentacleRig.Count; i++)
            {
                iTween.MoveUpdate(pointsTarget[i], new Vector3(0, 10 * (i / 100), 0), 0.2f);
            }
        }
        else
        {
            for (int i = 0; i < tentacleRig.Count; i++)
            {
                iTween.MoveUpdate(pointsTarget[i], Vector3.zero, 0.2f);
            }
        }

        //if (velocity > lastVelocity)//Checa se ele está se movendo.

        lastVelocity = velocity;//Salva a última velocidade.
    }
}

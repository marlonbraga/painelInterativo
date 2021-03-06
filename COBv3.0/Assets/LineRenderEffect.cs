﻿using UnityEngine;
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
    public int indexUser = 0;
    public int indexLineRednerer = 0;
    private List<GameObject> tentacleRig = new List<GameObject>();
    private List<GameObject> pointsTarget = new List<GameObject>();
    private Vector3 lastPosition;
    private Queue<float> medVelocity = new Queue<float>();
    public float x;
    public float velocity;
    public float scaleObject;
    [Range(0, 0.5f)]
    public float tentacleSlowness = 0.2f;
    public GameObject hand;
    public GameObject particle;
    public int count = 0;

    private void Start()
    {
        for (int i = 0; i < numOfVertices; i++)
        {
            var pt = Instantiate(pointTarget);
            pt.name = "PointTargetHand" + i;
            pt.transform.parent = transform;
            pt.transform.localPosition = new Vector3(0, 0, 0);
            pointsTarget.Add(pt);

            var tj = Instantiate(tentacleJoint);
            tj.name = "TentacleJointHand" + i;
            tj.GetComponent<TentacleAvatar>().TentacleTarget = pt;
            tj.GetComponent<TentacleAvatar>().Speed = (i * tentacleSlowness);

            if (i == numOfVertices - 1)
            {
                hand = Instantiate(hand, tj.transform);
                hand.GetComponent<HandElastic>().target = tentacleRig[i - 1].transform;
                hand.transform.localPosition = Vector3.zero;
                hand.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                hand.transform.localRotation = Quaternion.Euler(180, 0, 0);
            }
            tentacleRig.Add(tj);
        }
        GetComponent<LineRenderer>().positionCount = numOfVertices;
        //Game_Manager.Avatar[indexUser][indexLineRednerer] = hand;
    }

    void Update()
    {
        Hashtable hashtable = new Hashtable();
        hashtable.Add("time", 1f);
        hashtable.Add("islocal", true);

        velocity = (transform.parent.parent.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.parent.parent.position;

        //Update window of velocity
        if (medVelocity.Count < 10)
        {
            medVelocity.Enqueue(velocity);
        }
        else
        {
            medVelocity.Dequeue();
            medVelocity.Enqueue(velocity);
        }

        //
        for (int i = 0; i < medVelocity.Count; i++)
        {
            velocity = (velocity + medVelocity.ToArray()[i]);
        }
        velocity = velocity / medVelocity.Count;

        if (velocity > x)
        {
            for (int i = 0; i < pointsTarget.Count; i++)
            {
                hashtable.Add("position", new Vector3(0, i + 1, 0));
                iTween.MoveUpdate(pointsTarget[i], hashtable);
                hashtable.Remove("position");
            }
            //ocludeHand.SetActive(true);
            hand.SetActive(true);
        }
        else
        {
            for (int i = 0; i < pointsTarget.Count; i++)
            {
                hashtable.Add("position", Vector3.zero);
                iTween.MoveUpdate(pointsTarget[i], hashtable);
                hashtable.Remove("position");
            }
            //ocludeHand.SetActive(false);
            hand.SetActive(false);
        }
    }
    void LateUpdate()
    {
        //Desenhar braço (LINE RENDERER)
        for (int i = 0; i < tentacleRig.Count; i++)
        {
            GetComponent<LineRenderer>().SetPosition(i, tentacleRig[i].transform.position);
            if (/*i % 1 == 0  && */ velocity > x && count>=10)
            {
                var p = Instantiate(particle);
                p.transform.parent = tentacleRig[i].transform;
                p.transform.localPosition = Vector3.zero;
                p.transform.localRotation = Quaternion.Euler(Vector3.zero);
                StartCoroutine(DestroyParticles(3, p));
                if (i >= tentacleRig.Count-1)
                {
                    count = 0;
                    Debug.Log("+count :" + count);
                }
            }
        }
        count++;
    }
    IEnumerator DestroyParticles(float delay, GameObject particle)
    {
        yield return new WaitForSeconds(delay);
        Destroy(particle);
    }
}

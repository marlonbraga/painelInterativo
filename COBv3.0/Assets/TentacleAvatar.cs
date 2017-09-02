using UnityEngine;
using System.Collections.Generic;

public class TentacleAvatar : MonoBehaviour
{
    private GameObject tentacleTarget;
    [SerializeField, Range(0, 10)]
    private float speed;

    public GameObject TentacleTarget
    {
        get
        {
            return tentacleTarget;
        }

        set
        {
            tentacleTarget = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    void FixedUpdate()
    {
        iTween.MoveUpdate(gameObject, TentacleTarget.transform.position, Speed);
    }

    //public static List<Transform> jointTentacle = new List<Transform>();

    //// Use this for initialization
    //void Start()
    //{
    //    SetList(jointTentacle, transform);
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public void SetList(List<Transform> list, Transform go)
    //{
    //    list.Add(go);

    //    if (go.childCount > 0)
    //    {
    //        SetList(list, go.GetChild(0));
    //    }
    //}
}

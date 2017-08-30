using UnityEngine;
using System.Collections.Generic;

public class TentacleAvatar : MonoBehaviour
{

    public GameObject TentacleTarget;
    [Range(0,10)]
    public float speed;

    void FixedUpdate()
    {
            iTween.MoveUpdate(gameObject, TentacleTarget.transform.position, 0.2f + speed * 0.2f);
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

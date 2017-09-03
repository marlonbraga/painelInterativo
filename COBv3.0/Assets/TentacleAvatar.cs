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

    void Update()
    {
        iTween.MoveUpdate(gameObject, TentacleTarget.transform.position, Speed);
    }
}

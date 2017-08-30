using UnityEngine;

public class ParticleSystemHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject particles;

    void Start()
    {
        GetKeyJoint(transform);
    }

    void GetKeyJoint(Transform currentGameObject)
    {
        if (currentGameObject.childCount != 0)
        {
            for (int i = 0; i < currentGameObject.childCount; i++)
            {
                GetKeyJoint(currentGameObject.GetChild(i));
            }
        }
        SetParticle(currentGameObject);
    }

    void SetParticle(Transform jointKey)
    {
        if (jointKey.GetComponent<JointKey>())
        {
            var currentParticle = Instantiate(particles);
            currentParticle.transform.parent = jointKey;
            currentParticle.transform.localPosition = Vector3.zero;
        }
    }
}

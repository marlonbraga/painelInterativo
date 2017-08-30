using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hadouken : MonoBehaviour {

    public GameObject HadoukenParticle;
    private GameObject p;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="mão") {
            Debug.Log(collision.gameObject.name);
            p = Instantiate(HadoukenParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            p.transform.parent = collision.gameObject.transform;
            StartCoroutine(DestroyParticles(4f, p));
        }
    }
    IEnumerator DestroyParticles(float delay, GameObject particle)
    {
        yield return new WaitForSeconds(delay);
        Destroy(particle);
    }
}

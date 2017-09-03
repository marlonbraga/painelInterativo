using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleForm : MonoBehaviour
{
    public bool activate = false;
    private bool reducing = false;
    private bool augmenting = false;
    private bool user = false;
    private int idlePulse = 1;

    public float speedRotation = 10;
    public float speedReducing = 3;
    public GameObject particles;
    public GameObject[] avatar;
    public FollowingSpheres followingSpheres;

    private float InitialScaleForm;
    public float pulseIntensity = 0.2f;
    public float pulseSpeed = 0.2f;
    void Start()
    {
        InitialScaleForm = transform.localScale.x;
    }

    void Update()
    {
        /*//IDLE rotation
        transform.Rotate(0, 0f, speedRotation * Time.deltaTime);

        //IDLE pulsing
        if (activate == false)
        {
            if (transform.localScale.x > InitialScaleForm + pulseIntensity)
            {
                idlePulse = idlePulse * -1;
            }
            if (transform.localScale.x < InitialScaleForm - pulseIntensity)
            {
                idlePulse = idlePulse * -1;
            }
            float PulsingFactor = pulseSpeed * idlePulse * Time.deltaTime;
            Vector3 ReducingVector = new Vector3(PulsingFactor, PulsingFactor, PulsingFactor);
            transform.localScale = transform.localScale + ReducingVector;
        }*/
        //ACTIVATE
        if (activate == true && reducing == false && augmenting == false && user == false)
        {
            reducing = true;
            particles.SetActive(true);
            StartCoroutine(ReduceCube());
            StartCoroutine(ShowAvatar(true, 20));
            user = true;
            followingSpheres.movement = true;
            followingSpheres.follow = true;
        }
        //DESACTIVE
        else
        if (activate == false && reducing == false && augmenting == false && user == true)
        {
            augmenting = true;
            particles.SetActive(true);
            StartCoroutine(AugmenteCube());
            StartCoroutine(ShowAvatar(false, 0));
            user = false;
            followingSpheres.follow = false;
        }
    }
    IEnumerator ReduceCube()
    {
        float reducingFactor = speedReducing * Time.deltaTime;
        Vector3 ReducingVector = new Vector3(reducingFactor, reducingFactor, reducingFactor);
        while (transform.localScale.x > 0.2f)
        {
            transform.localScale = transform.localScale - ReducingVector;
            yield return new WaitForSeconds(0.1f * Time.deltaTime);
        }
        reducing = false;
        GetComponent<MeshRenderer>().enabled = false;
        //transform.localScale = new Vector3(InitialScaleForm, InitialScaleForm, InitialScaleForm);
    }
    IEnumerator AugmenteCube()
    {
        GetComponent<MeshRenderer>().enabled = true;

        float augmentingFactor = speedReducing * Time.deltaTime;
        Vector3 AugmentingVector = new Vector3(augmentingFactor, augmentingFactor, augmentingFactor);
        yield return new WaitForSeconds(0.4f * Time.deltaTime);
        while (transform.localScale.x < InitialScaleForm)
        {
            transform.localScale = transform.localScale + AugmentingVector;
            yield return new WaitForSeconds(0.1f * Time.deltaTime);
        }
        augmenting = false;
    }
    IEnumerator ShowAvatar(bool b, float delay)
    {
        yield return new WaitForSeconds(delay * Time.deltaTime);
        if (avatar[0])
        {
            for (int i = 0; i < avatar.Length-1; i++)
            {
                avatar[i].SetActive(b);
            }
        }
    }
}
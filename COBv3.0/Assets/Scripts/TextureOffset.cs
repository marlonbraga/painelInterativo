using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffset : MonoBehaviour {
    public float scrollSpeedHorizontal;
    public float scrollSpeedVertical;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offsetH = Time.time * scrollSpeedHorizontal;
        float offsetV = Time.time * scrollSpeedVertical;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offsetH, offsetV));
    }
}

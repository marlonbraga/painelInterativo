using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
	[HideInInspector]
	public Transform t;
	void Awake() {
		t = transform;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSpheres:MonoBehaviour {

	public GameObject ParticlePreFab;
	public GameObject[] BodyPiece;
	private Sphere[] Spheres;
	[HideInInspector]
	public bool movement = false;
	[HideInInspector]
	public bool follow = false;
	private bool lastFollow = false;
	void Start() {
		Spheres = gameObject.gameObject.GetComponentsInChildren<Sphere>();
	}

	void FixedUpdate() {
		if(movement == true) {
			if(follow) {
				for(int i = 0; i < Mathf.Min(Spheres.Length, BodyPiece.Length); i++) {
					StartCoroutine(reduceSpheres(Spheres[i].gameObject.transform));
					Vector3 pos;
					pos = BodyPiece[i].transform.position;
					iTween.MoveUpdate(Spheres[i].gameObject, pos, 1.2f);
					if(lastFollow != follow) {
						if(i == 0) {
							GameObject P;
							P = Instantiate(ParticlePreFab, transform.position, transform.rotation);
							P.transform.parent = transform;
							StartCoroutine(DestroyParticles(4f, P));
						}
						GameObject p;
						p = Instantiate(ParticlePreFab, Spheres[i].gameObject.transform.position, Spheres[i].gameObject.transform.rotation);
						p.transform.parent = Spheres[i].gameObject.transform;
						StartCoroutine(DestroyParticles(4f, p));
					}
				}
			} else {
				for(int i = 0; i < Mathf.Min(Spheres.Length, BodyPiece.Length); i++) {
					Vector3 pos;
					pos = gameObject.transform.position;
					StartCoroutine(augmenteSpheres(Spheres[i].gameObject.transform, pos));
					if(lastFollow != follow) {
						if(i == 0) {
							GameObject P;
							P = Instantiate(ParticlePreFab, BodyPiece[0].transform.position, BodyPiece[0].transform.rotation);
							//P.transform.parent = BodyPiece[0].transform;
							StartCoroutine(DestroyParticles(4f, P));
						}
						GameObject p;
						p = Instantiate(ParticlePreFab, Spheres[i].gameObject.transform.position, Spheres[i].gameObject.transform.rotation);
						p.transform.parent = Spheres[i].gameObject.transform;
						StartCoroutine(DestroyParticles(4f, p));
					}
				}
			}
			lastFollow = follow;
		}
	}
	IEnumerator reduceSpheres(Transform sphere) {
		float speedReducing = 0.1f;

		float reducingFactor = speedReducing * Time.deltaTime;
		Vector3 ReducingVector = new Vector3(reducingFactor, reducingFactor, reducingFactor);

		yield return new WaitForSeconds(0.3f);

		while(sphere.localScale.x > 0.2f) {
			sphere.localScale = sphere.localScale - ReducingVector;
			yield return new WaitForSeconds(0.1f * Time.deltaTime);
		}

	}
	IEnumerator augmenteSpheres(Transform sphere, Vector3 pos) {
		float speedReducing = 1f;

		float reducingFactor = speedReducing * Time.deltaTime;
		Vector3 ReducingVector = new Vector3(reducingFactor, reducingFactor, reducingFactor);


		while(sphere.localScale.x < 0.3f) {
			sphere.localScale = sphere.localScale + ReducingVector;
			yield return new WaitForSeconds(0.1f * Time.deltaTime);
		}
		iTween.MoveUpdate(sphere.gameObject, pos, 1.2f);
		while(sphere.localScale.x < 1.0f) {
			sphere.localScale = sphere.localScale + ReducingVector;
			yield return new WaitForSeconds(0.1f * Time.deltaTime);
		}
	}
	IEnumerator DestroyParticles(float delay, GameObject particle) {
		yield return new WaitForSeconds(delay);
		Destroy(particle);
	}
}

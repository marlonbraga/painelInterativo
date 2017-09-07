using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FootLineRenderer : MonoBehaviour{

	[SerializeField]
	private GameObject LineRendererTarget;
	[SerializeField]
	[Range(0, 1)]
	private float time;
	[SerializeField]
	[Range(0, 2)]
	private float y;
	[SerializeField]
	private GameObject avatar;
	private Hashtable ht1 = new Hashtable();
	private Hashtable ht2 = new Hashtable();
	
	void Awake(){
		iTween.Init(avatar);
		//void Update(){
		ht1.Add("y", y);
		ht1.Add("time", time);
		ht1.Add("delay", 0.1f);
		ht1.Add("easetype", iTween.EaseType.easeOutBack);

		ht2.Add("y", avatar.transform.position.y);
		ht2.Add("time", time);
		ht2.Add("delay", time);
		ht2.Add("easetype", iTween.EaseType.easeOutBack);

		GetComponent<LineRenderer>().SetPosition(0, LineRendererTarget.transform.position);
	}
	void LateUpdate(){
		GetComponent<LineRenderer>().SetPosition(1, transform.position);
	}
	[ContextMenu("Jump()")]
	private void _Jump(){
		StartCoroutine(Jump());
	}
	public IEnumerator Jump(){
		iTween.MoveTo(avatar, ht1);
		yield return new WaitForSeconds(0);
		iTween.MoveTo(avatar, ht2);
	}
}

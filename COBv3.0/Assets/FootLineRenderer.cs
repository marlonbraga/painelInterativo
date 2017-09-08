using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FootLineRenderer:MonoBehaviour {
	[SerializeField]
	private GameObject rightFootLineRendererTarget;
	[SerializeField]
	private GameObject leftFootLineRendererTarget;
	[SerializeField, Range(0, 2)]
	private float heightToJump;
	[SerializeField, Range(0, 30)]
	private int numOfVertices;
	[SerializeField]
	private GameObject pointTarget;
	[SerializeField]
	private GameObject tentacleJoint;
	[SerializeField, Range(0, 0.5f)]
	private float tentacleSlowness = 0.2f;
	[SerializeField]
	private GameObject footPrefab;

	private List<GameObject> pointsTarget_L = new List<GameObject>();
	private List<GameObject> pointsTarget_R = new List<GameObject>();
	private List<GameObject> tentacleRig_L = new List<GameObject>();
	private List<GameObject> tentacleRig_R = new List<GameObject>();
	private Vector3 initialPosition;
	public GameObject footRight;
	public GameObject footLeft;
	private Hashtable ht1 = new Hashtable();
	private Hashtable ht2 = new Hashtable();

	private void Start() {
		initialPosition = transform.position;
		GetKeyJoint(transform);
	}

	void GetKeyJoint(Transform currentGameObject) {
		if(currentGameObject.childCount != 0) {
			for(int i = 0; i < currentGameObject.childCount; i++) {
				GetKeyJoint(currentGameObject.GetChild(i));
			}
		}
		SetFoots(currentGameObject);
	}

	private void SetFoots(Transform foot) {
		if(foot.GetComponent<Foot>()) {
			switch(foot.GetComponent<Foot>().Direction) {
				case EnumTypes.Direction.Right:
					footRight = foot.gameObject;
					GenerateTentacle(footRight.transform);
					break;
				case EnumTypes.Direction.Left:
					footLeft = foot.gameObject;
					GenerateTentacle(footLeft.transform);
					break;
				default:
					break;
			}
		}
	}

	private void SetPosition(Transform foot) {
		if(foot.GetComponent<Foot>().Direction == EnumTypes.Direction.Right) {
			rightFootLineRendererTarget.GetComponent<Transform>().position = new Vector3(foot.position.x, -0.64f, 0);
		} else {
			leftFootLineRendererTarget.GetComponent<Transform>().position = new Vector3(foot.position.x, -0.64f, 0);
		}
	}

	private void GenerateTentacle(Transform foot) {
		for(int i = 0; i < numOfVertices; i++) {
			var pt = Instantiate(pointTarget);
			pt.name = "PointTargetFoot" + i;
			pt.transform.parent = foot;
			pt.transform.localPosition = new Vector3(0, 0, 0);
			if(foot.GetComponent<Foot>().Direction == EnumTypes.Direction.Left)
				pointsTarget_L.Add(pt);
			else {
				pointsTarget_R.Add(pt);
			}
			var tj = Instantiate(tentacleJoint);
			tj.name = "TentacleJointFoot" + i;
			tj.GetComponent<TentacleAvatar>().TentacleTarget = pt;
			tj.GetComponent<TentacleAvatar>().Speed = (/*i * tentacleSlowness*/0);

			if(i == numOfVertices - 1) {
				foot = Instantiate(foot, tj.transform);
				foot.transform.localPosition = Vector3.zero;
				foot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				foot.transform.localRotation = Quaternion.Euler(180, 0, 0);
			}
			if(foot.GetComponent<Foot>().Direction == EnumTypes.Direction.Left) {
				tentacleRig_L.Add(tj);
			} else {
				tentacleRig_R.Add(tj);
			}
			if(i == numOfVertices - 1) {
				var footTemp = Instantiate(footPrefab, tj.transform);
				if(foot.GetComponent<Foot>().Direction == EnumTypes.Direction.Left) {
					footTemp.GetComponent<HandElastic>().target = tentacleRig_L[i - 1].transform;
				} else {
					footTemp.GetComponent<HandElastic>().target = tentacleRig_R[i - 1].transform;
				}
				footTemp.transform.localPosition = Vector3.zero;
				//footTemp.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				//footTemp.transform.localRotation = Quaternion.Euler(180, 0, 0);
			}
		}
		//foot.GetComponent<LineRenderer>().Posi = numOfVertices;
	}

	void Update() {
		Hashtable hashtable = new Hashtable();
		hashtable.Add("time", 1f);
		hashtable.Add("islocal", true);

		SetPosition(footRight.transform);
		SetPosition(footLeft.transform);
		if(transform.position.y > (initialPosition.y + heightToJump)) {
			for(int i = 0; i < tentacleRig_R.Count; i++) {

				hashtable.Add("position", /*pointsTarget_R[i].transform.position - */new Vector3(0, i - 0.1f, 0));
				iTween.MoveUpdate(pointsTarget_R[i], hashtable);
				hashtable.Remove("position");

				hashtable.Add("position", new Vector3(0, i - 0.1f, 0));
				iTween.MoveUpdate(pointsTarget_L[i], hashtable);
				hashtable.Remove("position");
			}

			//footPrefab.SetActive(true);
			_Jump();
		} else {
			for(int i = 0; i < pointsTarget_L.Count; i++) {
				hashtable.Add("position", Vector3.zero);
				iTween.MoveUpdate(pointsTarget_L[i], hashtable);
				hashtable.Remove("position");

				hashtable.Add("position", Vector3.zero);
				iTween.MoveUpdate(pointsTarget_R[i], hashtable);
				hashtable.Remove("position");
			}
			//footPrefab.SetActive(false);
		}
		//transform.GetChild(0).position = new Vector3(LineRendererTarget.transform.position.x, transform.position.y, transform.position.z);
	}

	void LateUpdate() {

		for(int i = 0; i < numOfVertices; i++) {
			footRight.GetComponent<LineRenderer>().SetPosition(i, tentacleRig_R[i].transform.position);
			footLeft.GetComponent<LineRenderer>().SetPosition(i, tentacleRig_L[i].transform.position);
		}
	}

	[ContextMenu("Jump()")]
	private void _Jump() {

		StartCoroutine(Jump());
	}
	public IEnumerator Jump() {
		Debug.Log("Jump;");
		footRight.SetActive(true);
		footLeft.SetActive(true);
		yield return new WaitForSeconds(1f);

		footRight.SetActive(false);
		footLeft.SetActive(false);

	}
}
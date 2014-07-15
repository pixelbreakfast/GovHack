using UnityEngine;
using System.Collections;

public class TargetPicker : MonoBehaviour {

	public GameObject CurrentTarget;

	public Ray TargetRay;
	public RaycastHit TargetRayHit;
	Suburb currentDataPoint;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray TargetRay = camera.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(TargetRay,out TargetRayHit,Mathf.Infinity);
			if (TargetRayHit.collider != null) {

				CurrentTarget = TargetRayHit.collider.gameObject;
				currentDataPoint = (Suburb) CurrentTarget.GetComponent<Suburb>();

			}

			Debug.DrawRay(TargetRay.origin, TargetRay.direction * 100, Color.yellow);
		}
	}
}

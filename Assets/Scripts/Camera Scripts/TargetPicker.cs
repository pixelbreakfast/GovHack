using UnityEngine;
using System.Collections;

public class TargetPicker : MonoBehaviour {

	public GameObject CurrentTarget;

	public Ray TargetRay;
	public RaycastHit TargetRayHit;
	DataPoint currentDataPoint;

	GUIStyle style = new GUIStyle();
	// Use this for initialization
	void Start () {
	
	}

	void OnGUI() {
		if(currentDataPoint != null) {
			style.normal.textColor = Color.black;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = 32;
			style.font = (Font) Resources.Load ("Fonts/PoetsenOne");
			GUI.Label(new Rect(0, 0, Screen.width, 150), currentDataPoint.GetName(), style);

			style.fontSize = 20;
			GUI.Label(new Rect(0, 0, Screen.width, 225), "$" + System.Math.Round (currentDataPoint.GetValue()/1000000,2) + "m", style);

		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Ray TargetRay = camera.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(TargetRay,out TargetRayHit,Mathf.Infinity);
			if (TargetRayHit.collider != null) {
				CurrentTarget = TargetRayHit.collider.gameObject;
				currentDataPoint = CurrentTarget.GetComponent<DataPointNode>().dataPoint;
			}

			Debug.DrawRay(TargetRay.origin, TargetRay.direction * 100, Color.yellow);
		}
	}
}

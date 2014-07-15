using UnityEngine;
using System.Collections;

public class CameraDolley : MonoBehaviour {

	Vector3 targetPoint;

	void Awake() {
		targetPoint = transform.position;
	
	}

	void Update() {
	
		transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime);
	}

	public void MoveToPoint(Vector3 point, string moveType) {
		if(moveType == "INSTANT") {
			transform.position = point;
		}
		targetPoint = point;
	}
}

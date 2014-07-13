using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	public TargetPicker targetPicker;
	public GameObject myTarget;
	public GameObject myLastTarget;

	//public NodeScript oldNodeScript;
	//public NodeScript newNodeScript;

	public bool updatingLocation;

	public float X1; //These ugly, ugly variables are used to trianglate where the camera needs to move to after picking a new target
	public float X2; //Please ignore the ugly
	public float X3;
	public float Z1;
	public float Z2;
	public float Z3;
	public float destOffset;
	public Vector3 myDestination;
	public float heightLimit;

	//public float lastTargetX;
	//public float lastTargetZ;
	//public float newTargetJumpX;
	//public float newTargetJumpZ;
	//public float HaveIArrived;

	public bool clickCheck;
	public bool hitCheck;

	public float cameraRotateSpeed; 
	public float cameraAmbientRotateSpeed;

	public float smoothZoom;
	public float zoomStrength;
	public float smoothZoomCutoff;
	public float zoomBounds;

	void Update () {
		InputCheck ();
		UpdateLocation ();
		DoRotation ();
		DoZoom ();
	}

	void InputCheck() {
		if ( Input.GetMouseButtonDown( 0 ) ) {
			UpdateTarget();
			clickCheck = true;
			if (targetPicker.TargetRayHit.collider != null) {
				hitCheck = true;
				cameraAmbientRotateSpeed = 0.1f;
			}
		}
		
		if ( Input.GetMouseButtonUp( 0 ) ) {
			clickCheck = false;
			hitCheck = false;
			//Invoke("TurnOffUpdateLocation",10f);
		}
	}

	void UpdateTarget() {
		updatingLocation = false;
		if (targetPicker.CurrentTarget != null && targetPicker.CurrentTarget != myTarget) {
			myLastTarget = myTarget;
			//oldNodeScript = myLastTarget.GetComponent<NodeScript>();
			//if (oldNodeScript != null) {
			//	oldNodeScript.AmIActive = false;
			//}
			myTarget = targetPicker.CurrentTarget;
			//newNodeScript = myTarget.GetComponent<NodeScript>();
			//if (newNodeScript != null) {
			//	newNodeScript.AmIActive = true;
			//}
			/*
			lastTargetX = transform.position.x - myLastTarget.transform.position.x;
			if (lastTargetX < 0) {
				lastTargetX = -lastTargetX;
			}
			lastTargetZ = transform.position.z - myLastTarget.transform.position.z;
			if (lastTargetZ < 0) {
				lastTargetZ = -lastTargetZ;
			}
			*/

			//myDestination3 = new Vector3(myTarget.transform.position.x,transform.position.y,myTarget.transform.position.z);

			//newTargetJumpX = myTarget.transform.position.x - myLastTarget.transform.position.x + 5;
			//newTargetJumpZ = myTarget.transform.position.z - myLastTarget.transform.position.z + 5;
			//myDestination1 = new Vector3(transform.position.x + newTargetJumpX,transform.position.y,transform.position.z + newTargetJumpZ);
			updatingLocation = true;
		}
	}

	void UpdateLocation() {
		if(myLastTarget ==null) return;
		if(myTarget == null) return;

		X1 = myLastTarget.transform.position.x;
		Z1 = myLastTarget.transform.position.z;
		X2 = myTarget.transform.position.x;
		Z2 = myTarget.transform.position.z;

		if (X1 > X2) {
			X3 = X2 + destOffset;
		}
		else {
			X3 = X2 - destOffset;
		}
		
		if (Z1 > Z2) {
			Z3 = Z2 + destOffset;
		}
		else {
			Z3 = Z2 - destOffset;
		}

		if (updatingLocation) {
			myDestination = new Vector3(X3,transform.position.y,Z3);

			if (myDestination.y < heightLimit) {
				myDestination = new Vector3(myDestination.x,heightLimit,myDestination.z);
			}
			//There are two Destinations because having only one doesn't allow for a 3D zoom
			//myDestination = new Vector3(myDestination1.x,transform.position.y,myDestination1.z);
			transform.position = Vector3.Lerp (transform.position, myDestination, Time.deltaTime);
			//HaveIArrived = Vector3.Distance(transform.position,myTarget.transform.position);


		}

		if (transform.position.y < heightLimit) {
			myDestination = new Vector3(transform.position.x,heightLimit,transform.position.z);
			transform.position = Vector3.Lerp (transform.position, myDestination, Time.deltaTime);
		}
	}

	void DoRotation() {
		if (hitCheck) {
			transform.RotateAround (myTarget.transform.position, -Vector3.up, Input.GetAxis ("Mouse X") * cameraRotateSpeed);
			//transform.RotateAround (myTarget.transform.position, Vector3.forward, Input.GetAxis ("Mouse Y") * cameraRotateSpeed/2);
			//Repeat the line above and configure it to use Vector3.left, possibly minused. That might fix the issue.
		} //else if (updatingLocation == false) {
			//cameraAmbientRotateSpeed = Mathf.Lerp(cameraAmbientRotateSpeed,0.05f,Time.deltaTime/4);
			//transform.RotateAround (myTarget.transform.position, Vector3.up, cameraAmbientRotateSpeed);
		//}
	}

	void DoZoom() {
		if (smoothZoom > smoothZoomCutoff || smoothZoom < -smoothZoomCutoff) {
			transform.position += transform.forward * smoothZoom;
			smoothZoom = Mathf.Lerp(smoothZoom,0,Time.deltaTime*2);
		}

		if (transform.position.y < heightLimit) {
			zoomStrength = 0.01f;
		}
		
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && smoothZoom < 0.5f) {
			//zoomBounds = Vector3.Distance(transform.position,myTarget.transform.position);
			//if (zoomBounds > 20f) {
				smoothZoom += zoomStrength;
			//}
		}
		
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && smoothZoom > -0.5f) {
			//zoomBounds = Vector3.Distance(transform.position,myTarget.transform.position);
			//if (zoomBounds < 100f) {
				smoothZoom -= zoomStrength;
			//}
		}

		zoomStrength = 0.1f;
	}

	void TurnOffUpdateLocation() {
		updatingLocation = false;
	}
}

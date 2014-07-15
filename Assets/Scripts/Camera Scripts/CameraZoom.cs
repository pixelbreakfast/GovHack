using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
	
	float scrollSpeed = 200;
	Vector3 parentOffset = new Vector3(0,70,-80);
	Vector3 targetPoint;

	// Use this for initialization
	void Awake () {
		targetPoint = transform.position;

	}
	
	// Update is called once per frame
	void Update () {


		transform.LookAt(transform.parent.position);

			if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
			{
				
			targetPoint -= transform.forward * scrollSpeed * Time.deltaTime;
				
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
			{
				
			targetPoint += transform.forward * scrollSpeed * Time.deltaTime;
				
			}

		transform.position = transform.parent.position + Vector3.Lerp(transform.localPosition, targetPoint, Time.deltaTime);
	}
}

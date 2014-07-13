var target : Transform;
var damping = 6.0;
var smooth = true;

@script AddComponentMenu("Camera-Control/Smooth Look At")

function LateUpdate () {
	if(Input.GetMouseButtonDown(0)) {
		var TargetRay : Ray = camera.ScreenPointToRay(Input.mousePosition);
		var TargetRayHit: RaycastHit;
		Physics.Raycast(TargetRay,TargetRayHit,Mathf.Infinity);
		if (TargetRayHit.collider != null) {
			target = TargetRayHit.collider.gameObject.transform;
		}
	}

	if (target) {
		if (smooth)
		{
			// Look at and dampen the rotation
			var rotation = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
		}
		else
		{
			// Just lookat
		    transform.LookAt(target);
		}
	}
}

function Start () {
	// Make the rigid body not change rotation
   	if (rigidbody)
		rigidbody.freezeRotation = true;
}
using UnityEngine;
using System.Collections;

public class Suburb : MonoBehaviour {

	public string postcode;
	public float value;
	public float normalisedValue = 0.01f;


	void Start() {
		transform.localScale = new Vector3(1,1,GetNormalisedValue() * 10);

		gameObject.renderer.material.color = new Color(GetNormalisedValue() * 10, 1- GetValue() * 10, 0, 0);

	}

	void Update() {
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(1,1,GetNormalisedValue() * 10), Time.deltaTime);
		gameObject.renderer.material.color = Color.Lerp(gameObject.renderer.material.color, new Color(GetNormalisedValue() * 10, 1 - GetNormalisedValue()  * 10, 0, 1), Time.deltaTime);

	}

	void OnMouseUp() {
		SpatialDataRender.instance.selectedSuburb = this;
		Camera.main.transform.parent.gameObject.GetComponent<CameraDolley>().MoveToPoint(transform.position, "SMOOTH");
	}

	
	public void SetValue(float value, float maxValue) {

		this.value = value;
		this.normalisedValue = value/maxValue;
	}
	
	public float GetValue() {
		return value;
	}
	
	public float GetNormalisedValue() {
		return normalisedValue;
	}
	
	public string GetName() {
		return name;
	}

}

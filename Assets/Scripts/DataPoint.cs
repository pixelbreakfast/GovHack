using UnityEngine;
using System.Collections;

public class DataPoint  {

	Vector2 position;
	string name;
	string postcode;
	float value = 0;
	float normalisedValue = 0.01f;
	public GameObject gameObject;


	public DataPoint (Vector2 position, string postcode, string name) {
		this.position = position;
		this.name = name;
		this.postcode = postcode;
	}

	public GameObject CreateGameObject() {


		gameObject = GameObject.Instantiate(Resources.Load ("Meshes/cube")) as GameObject;
		gameObject.transform.position = new Vector3(position.x, 0, position.y);
		gameObject.name = name;
		gameObject.GetComponent<DataPointNode>().dataPoint = this;


		return gameObject;
		//gameObject.transform.localScale = new Vector3(0.4f, 1, 0.4f);

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

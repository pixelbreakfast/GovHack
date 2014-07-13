using UnityEngine;
using System.Collections;

public class DataPointNode : MonoBehaviour {

	public DataPoint dataPoint;

	void Start() {
		transform.localScale = new Vector3(1,1,dataPoint.GetNormalisedValue() * 10);

		gameObject.renderer.material.color = new Color(dataPoint.GetNormalisedValue() * 10, 1- dataPoint.GetValue() * 10, 0, 0);

	}

	void Update() {
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(1,1,dataPoint.GetNormalisedValue() * 10), Time.deltaTime);
		gameObject.renderer.material.color = Color.Lerp(gameObject.renderer.material.color, new Color(dataPoint.GetNormalisedValue() * 10, 1 - dataPoint.GetNormalisedValue()  * 10, 0, 1), Time.deltaTime);

	}

}

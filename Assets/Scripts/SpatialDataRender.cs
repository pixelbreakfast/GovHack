using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SpatialDataRender : MonoBehaviour
{
	public static SpatialDataRender instance;

	public float mapScale = 50;
	string line;
	GameObject map;
	Dictionary<string,DataPoint> dataPoints = new Dictionary<string,DataPoint>();
	List<string> dataNames = new List<string>();

	Vector2 offset = new Vector2(-2900,769.2f);

	float maxValue = 0;

	float maxCubeHeight = 10;

	float minXExtent = 0;
	float maxXExtent = 0;
	
	float minYExtent = 0;
	float maxYExtent = 0;

	int currentNode = 0;
	int nodesPerFrame = 15;

	void Awake() {
		if(SpatialDataRender.instance != null) {
			Debug.LogError("Caan't have more than once instance of SpatialDataRender");
		}
		SpatialDataRender.instance = this;
	}

	void Start () {

		map = new GameObject();
		map.name = "Map";

		StreamReader file = new StreamReader("Assets\\Melbourne.csv");

		while((line = file.ReadLine()) != null)
		{
			string [] suburb = line.Split(new char [] {','});
			
			Vector2 coord =  new Vector2(float.Parse(suburb[10]) * mapScale, float.Parse(suburb[9]) * mapScale);
			string postcode = suburb[1];
			string suburbName = suburb[2];

			if(!dataPoints.ContainsKey(postcode)) {
				dataNames.Add(postcode);
			}


			DataPoint dataPoint = new DataPoint(coord+offset,postcode,suburbName);
			dataPoints[postcode] = dataPoint;
			dataPoint.CreateGameObject().transform.parent = map.transform;

			SetExtents(coord.x, coord.y);
		}

		StartCoroutine("ShowNodes");

	}


	IEnumerator ShowNodes() {


		//dataPoints[dataNames[currentNode]].CreateGameObject().transform.parent = map.transform;

		for(int i = 0; i < nodesPerFrame; i++) {
			if(currentNode >= dataNames.Count){
				GameObject.FindGameObjectWithTag("DataOptions").GetComponent<DataOptionsGUI>().ShowGUI();
				return false;
			}
			dataPoints[dataNames[currentNode]].gameObject.renderer.enabled = true;
		
			currentNode++;
		}
		yield return new WaitForSeconds(0.001f);

		StartCoroutine("ShowNodes");
	}

	public void SetMaxValue(float value) {

		maxValue = value;
	}

	public void SetData(string postcode, float value) {

		if(dataPoints.ContainsKey(postcode)) {
			dataPoints[postcode].SetValue(value, maxValue);
		} else {
			Debug.LogWarning("Trying to set data to datapoint that does not exist");
		}

	}


	
	void SetExtents(float x, float y) {
		if(x < minXExtent) minXExtent = x;
		if(x > maxXExtent) maxXExtent = x;
		if(y < minYExtent) minYExtent = y;
		if(y > maxYExtent) maxYExtent = y;
	}
}

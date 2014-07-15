using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LumenWorks.Framework.IO.Csv;

public class SpatialDataRender : MonoBehaviour
{
	public static SpatialDataRender instance;

	public float mapScale = 50;
	string line;
	GameObject map;
	Dictionary<string,Suburb> suburbs = new Dictionary<string,Suburb>();
	List<string> dataNames = new List<string>();
	public Suburb selectedSuburb;
	
	GUIStyle style = new GUIStyle();

	float maxValue = 0;

	float maxCubeHeight = 10;

	float minXExtent = float.MaxValue;
	float maxXExtent = float.MinValue;
	float minYExtent = float.MaxValue;
	float maxYExtent = float.MinValue;

	int currentNode = 0;
	int nodesPerFrame = 15;

	public bool showData = false;

	void Awake() {
		if(SpatialDataRender.instance != null) {
			Debug.LogError("Caan't have more than once instance of SpatialDataRender");
		}
		SpatialDataRender.instance = this;
	}
	
	void OnGUI() {
		if(selectedSuburb != null) {
			style.normal.textColor = Color.black;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = 32;
			style.font = (Font) Resources.Load ("Fonts/PoetsenOne");
			GUI.Label(new Rect(0, 0, Screen.width, 150),selectedSuburb.GetName(), style);
			if(showData) {
				style.fontSize = 20;
				
				GUI.Label(new Rect(0, 0, Screen.width, 225),System.Math.Round(selectedSuburb.GetValue(),2).ToString(), style);
			}
		}
	}
	void Start () {

		map = new GameObject();
		map.name = "Map";

		
		using (CsvReader csv =
		       new CsvReader(new StreamReader("Assets/Melbourne.csv"), true))
		{
			
			int fieldCount = csv.FieldCount;
			
			string[] headers = csv.GetFieldHeaders();
			while (csv.ReadNextRecord())
			{
				
				Vector2 coord =  new Vector2(float.Parse(csv[10]) * mapScale, float.Parse(csv[9]) * mapScale);
				string postcode = csv[1];
				string suburbName = csv[2];


				if(!suburbs.ContainsKey(postcode)) {
					dataNames.Add(postcode);
					GameObject suburbGameObject = (GameObject) GameObject.Instantiate(Resources.Load("Meshes/Suburb"));
					suburbGameObject.transform.position = new Vector3(coord.x,0,coord.y);

					Suburb suburb = suburbGameObject.GetComponent<Suburb>();
					suburbs[postcode] = suburb;
					suburb.transform.parent = map.transform;
					suburb.name = suburbName;
					SetExtents(coord.x, coord.y);
				}


			}
		}


		Camera.main.transform.parent.gameObject.GetComponent<CameraDolley>().MoveToPoint(new Vector3(minXExtent + (maxXExtent - minXExtent) /2 ,0, minYExtent + (maxYExtent - minYExtent) /2), "INSTANT");

		StartCoroutine("ShowNodes");

	}


	IEnumerator ShowNodes() {

		//dataPoints[dataNames[currentNode]].CreateGameObject().transform.parent = map.transform;

		for(int i = 0; i < nodesPerFrame; i++) {
			if(currentNode >= suburbs.Count){
				GameObject.FindGameObjectWithTag("DataOptions").GetComponent<DataOptionsGUI>().ShowGUI();
				return false;
			}
			suburbs[dataNames[currentNode]].gameObject.renderer.enabled = true;
		
			currentNode++;
		}
		yield return new WaitForSeconds(0.001f);

		StartCoroutine("ShowNodes");
	}

	public void SetMaxValue(float value) {

		maxValue = value;
	}

	public void SetData(string postcode, float value) {

		if(suburbs.ContainsKey(postcode)) {

			suburbs[postcode].SetValue(value, maxValue);
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

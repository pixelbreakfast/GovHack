using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class DataOptionsGUI : MonoBehaviour {

	public GUIStyle style;
	bool showGUI = false;
	string selectedCategory;
	List<string> datasets = new List<string>();
	DataSource dataSource = new DataSource();
	string selectedFile;

	void OnGUI() {

		int childIndex = 0;
		if(showGUI){
			foreach(string file in datasets) {

				if(GUI.Button(new Rect( 50, (60 * childIndex) + 20, Screen.width - 100, 50), file,style)){
					showGUI = false;
					selectedCategory = file;
					selectedFile = file;
				}
				childIndex++;
			}

		} 
		if(selectedFile != null) {
			
			if(GUI.Button(new Rect(Screen.width / 2  - 100, Screen.height / 4, 200, 50), "Load",style)){
				dataSource.SelectIndices(12, 6, selectedFile);
				selectedFile = null;
			}
		}

		/*else {
			GUIStyle labelStyle = new GUIStyle();
			labelStyle.normal.textColor = Color.black;
			GUI.Label(new Rect( 20, (50 * childIndex) + 20, 400, 50), selectedCategory, labelStyle);

		}*/
	}

	void Start () {
		try 
		{
			// Only get files that begin with the letter "c." 
			string[] files = Directory.GetFiles(@"Assets/Datasets", "*.csv");

			foreach (string filename in files) 
			{
				Debug.Log (filename);
				datasets.Add(filename);

			}
		} 
		catch (Exception e) 
		{
			Debug.LogException(e);
		}
	}

	public void ShowGUI() {
		showGUI = true;
	}

}

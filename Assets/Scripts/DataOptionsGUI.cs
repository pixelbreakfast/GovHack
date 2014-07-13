using UnityEngine;
using System.Collections;

public class DataOptionsGUI : MonoBehaviour {

	public GUIStyle style;
	bool showGUI = false;
	string selectedCategory;

	void OnGUI() {

		int childIndex = 0;
		if(showGUI){
			foreach(Transform child in transform) {
				DataSource dataSource = child.gameObject.GetComponent<DataSource>();

				if(GUI.Button(new Rect( 20, (50 * childIndex) + 20, 400, 50), dataSource.GetSourceName(),style)){
					showGUI = false;
					selectedCategory = dataSource.GetSourceName();
					dataSource.LoadData();
				}
				childIndex++;
			}

		} else {
			GUIStyle labelStyle = new GUIStyle();
			labelStyle.normal.textColor = Color.black;
			GUI.Label(new Rect( 20, (50 * childIndex) + 20, 400, 50), selectedCategory, labelStyle);

		}
	}

	public void ShowGUI() {
		showGUI = true;
	}

}

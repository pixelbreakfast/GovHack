using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class DataSource : MonoBehaviour {

	Dictionary<string, float> valuePerPostcode = new Dictionary<string, float>();

	string line;

	public int postcodeIndex;
	public int valueIndex;
	public string sourceName;


	// Use this for initialization
	public void LoadData () {

		StreamReader file = new StreamReader("Assets\\Permits2014.csv");

		while((line = file.ReadLine()) != null)
		{ 

			string [] suburb = line.Split(new char [] {','});
			string value =  suburb[valueIndex];
			string postcode = suburb[postcodeIndex];

			Regex rgx = new Regex("[^a-zA-Z0-9.]");
			value = rgx.Replace(value, "");

			if(valuePerPostcode.ContainsKey(postcode)) {
				valuePerPostcode[postcode] += float.Parse(value);

			} else {
				valuePerPostcode[postcode] = float.Parse(value);
					
			}


		}

		float maxValue = 0;
		foreach(KeyValuePair<string, float> suburb in valuePerPostcode) {
			Debug.Log (suburb.Key + ", " + suburb.Value);
			if(suburb.Value > maxValue) maxValue = suburb.Value;

		}
		//hack: this needs to be set first
		SpatialDataRender.instance.SetMaxValue(maxValue);

		foreach(KeyValuePair<string, float> suburb in valuePerPostcode) {

			SpatialDataRender.instance.SetData(suburb.Key, suburb.Value);
		}

	}

	public string GetSourceName() {
		return sourceName;
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using LumenWorks.Framework.IO.Csv;

public class DataSource {

	Dictionary<string, float> valuePerPostcode = new Dictionary<string, float>();

	string line;

	public int postcodeIndex = 12;
	public int valueIndex = 6;

	string source;
	GUIContent[] valueSource;
	GUIContent[] postcodeSource;

	public void SelectIndices(int postcodeIndex, int valueIndex, string source) {

		this.source = source;
		this.postcodeIndex = postcodeIndex;
		this.valueIndex = valueIndex;
		LoadData();

	}


	// Use this for initialization
	public void LoadData () {
	
		float maxValue = 0;
		using (CsvReader csv = new CsvReader(new StreamReader(source), true))
		{
		
			int fieldCount = csv.FieldCount;

			string[] headers = csv.GetFieldHeaders();
			while (csv.ReadNextRecord())
			{
				string valueString = csv[valueIndex];
				Regex rgx = new Regex("[^a-zA-Z0-9.]");
				valueString = rgx.Replace(valueString, "");


				if(valuePerPostcode.ContainsKey(csv[postcodeIndex])) {
					valuePerPostcode[csv[postcodeIndex]] += float.Parse(valueString);
					
				} else {
					valuePerPostcode[csv[postcodeIndex]] = float.Parse(valueString);
					
				}
				if(valuePerPostcode[csv[postcodeIndex]] > maxValue) maxValue = valuePerPostcode[csv[postcodeIndex]];
			}
		}
	
	

	//hack: this needs to be set first
	SpatialDataRender.instance.SetMaxValue(maxValue);
	
	foreach(KeyValuePair<string, float> suburb in valuePerPostcode) {

			SpatialDataRender.instance.SetData(suburb.Key, suburb.Value);
		}
		
		SpatialDataRender.instance.showData = true;
	
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // Get and set current palette from saved palette
        float palette = PlayerPrefs.GetFloat("palette");
        GetComponent<Renderer>().material.SetFloat("_PalettePicker", palette);
	}
}

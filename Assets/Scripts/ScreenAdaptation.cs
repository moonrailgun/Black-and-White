using UnityEngine;
using System.Collections;

public class ScreenAdaptation : MonoBehaviour {
	
	float screenWidth;
	float screenHeight;
	GameObject Informations;
	
	void Awake()
	{
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		Informations = GameObject.Find("Informations");
	}
	
	// Use this for initialization
	void Start () {
		Informations.transform.localPosition = new Vector3( - screenWidth / 2 + 20 , screenHeight / 2 - 50, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if(screenWidth != Screen.width||screenHeight != Screen.height)
		{
			screenWidth = Screen.width;
			screenHeight = Screen.height;
			Informations.transform.localPosition = new Vector3( - screenWidth / 2 + 20 , screenHeight / 2 - 50, 1);
		}
	}
}

using UnityEngine;
using System.Collections;

public class ChessmanSettings : MonoBehaviour {
	
	private float AnimationSpeed = 20f;
	
	// Use this for initialization
	void Start () {
		animation["whitetoblack"].speed = AnimationSpeed;
		animation["blacktowhite"].speed = AnimationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

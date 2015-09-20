using UnityEngine;
using System.Collections;

public class GlobalVarContainer : MonoBehaviour {

	public string _playerName = "";

	// Use this for initialization
	void Start () {
	
		if (this.name == "GlobalVarContainer") DontDestroyOnLoad (this.gameObject);
	}
}

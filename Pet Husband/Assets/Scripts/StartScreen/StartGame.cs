using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour {

	public Canvas _NamingWindow;
	public Button _start;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FillInName()
	{
		_NamingWindow.enabled = true;
		_start.enabled = false;
	}
	
}

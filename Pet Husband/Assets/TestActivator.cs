using UnityEngine;
using System.Collections;

public class TestActivator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonUp (0)) 
		{
			Canvas task = GameObject.Find("Task").GetComponent<Canvas>();
			task.enabled = true;
		}
	}
}

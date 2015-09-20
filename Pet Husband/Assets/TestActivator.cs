using UnityEngine;
using System.Collections;

public class TestActivator : MonoBehaviour {

	private Canvas _task;
	// Use this for initialization
	void Start () {
		_task = GameObject.Find("Task").GetComponent<Canvas>();
		_task.enabled = false;	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonUp (0)) 
		{ 
			_task.enabled = true;
		}
	}
}

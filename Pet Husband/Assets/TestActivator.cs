using UnityEngine;
using System.Collections;

public class TestActivator : MonoBehaviour {

	private TaskBehavior _taskBehavior;
	// Use this for initialization
	void Start () {
		GameObject task = GameObject.Find ("Task");		
		_taskBehavior = task.GetComponent<TaskBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonUp (0)) 
		{ 
			_taskBehavior.ShowFirstTime();
		}
	}
}

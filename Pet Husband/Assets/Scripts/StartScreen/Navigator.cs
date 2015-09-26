using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Navigator : MonoBehaviour {

	private GlobalVarContainer _globalVars;
	public Text _input;
	// Use this for initialization
	void Start () {
	
		if (this.name == "Navigator") DontDestroyOnLoad (this.gameObject);

		if(GameObject.Find ("GlobalVarContainer") != null)
			_globalVars = GameObject.Find ("GlobalVarContainer").GetComponent<GlobalVarContainer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyUp (KeyCode.Escape))
			Quit ();
	}

	public void GoToSelectionScreen()
	{
		_globalVars._playerName = _input.text;
		Application.LoadLevel ("Husband_selection_screen");
	}

	public void GoToFirstCutscene()
	{
		Application.LoadLevel ("First_Cutscene");
	}

	public void Quit()
	{
		//Debug.Log ("Quit");
		Application.Quit ();
	}

}

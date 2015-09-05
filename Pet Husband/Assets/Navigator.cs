using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Navigator : MonoBehaviour {

	public string _playerName = "";
	public Text _input;
	// Use this for initialization
	void Start () {
	
		if (this.name == "Navigator") DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoToSelectionScreen()
	{
		_playerName = _input.text;
		Application.LoadLevel ("Husband_selection_screen");
	}

	public void GoToFirstCutscene()
	{
		Application.LoadLevel ("First_Cutscene");
	}

}

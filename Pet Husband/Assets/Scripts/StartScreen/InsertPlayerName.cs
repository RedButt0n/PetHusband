using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InsertPlayerName : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		GameObject _navigator = GameObject.Find ("Navigator");
		Navigator _nav = _navigator.GetComponent<Navigator> ();

		Text welcome = this.gameObject.GetComponent<Text> ();
		
		welcome.text = welcome.text.Replace ("%Player%", _nav._playerName);
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectedHusbandController : MonoBehaviour {

	//These are needed to connect to the Image & Text objects in the scene
	public Image _husbandImage;
	public Text _husbandDescription;

	public GameObject[] _husbandlist;				//List of all available husbands
	private int _selectedNumber = 0;				//Number to iterate through husbandList
	private HusbandProperties _selectedHusband;		//Variable to access properties of the selected husband
	private int _amountOfHusbands;
	// Use this for initialization
	void Start () {
	
		_amountOfHusbands = _husbandlist.Length-1;
		UpdateHusband();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonUp (0)) {
			AdvanceInList();
			UpdateHusband();
		}
	}

	private void AdvanceInList() {
		if (_selectedNumber < _amountOfHusbands)
			++_selectedNumber;
		else
			_selectedNumber = 0;
	}

	public void UpdateHusband() {
		_selectedHusband = _husbandlist [_selectedNumber].GetComponent<HusbandProperties> ();
		_husbandDescription.text = _selectedHusband._description;
		_husbandImage.sprite = _selectedHusband._image;
	}

}

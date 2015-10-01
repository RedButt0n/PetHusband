using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectedHusbandController : MonoBehaviour {

	//These are needed to connect to the Image & Text objects in the scene
	public Image _husbandImage;
	public Text _husbandName;
	public Text _husbandDescription;

	public Image _husbandInfoImage;
	public Text _husbandLongDescription;

	public Button _selectButton;
	public Button _infoButton;

	public GameObject[] _husbandlist;				//List of all available husbands
	private int _selectedNumber = 0;				//Number to iterate through husbandList
	private HusbandProperties _selectedHusband;		//Variable to access properties of the selected husband
	private int _amountOfHusbands;					//Variable to store amount of available husbands

	// Use this for initialization
	void Start () {
	
		_amountOfHusbands = _husbandlist.Length-1;
		UpdateHusband();

	}

	public void PressNext()
	{
		AdvanceInList ();
		UpdateHusband ();
	}

	public void PressPrevious()
	{
		GoBackInList ();
		UpdateHusband ();
	}

	private void AdvanceInList() {
		if (_selectedNumber < _amountOfHusbands)
			++_selectedNumber;
		else
			_selectedNumber = 0;
	}

	private void GoBackInList() {
		if (_selectedNumber != 0)
			--_selectedNumber;
		else
			_selectedNumber = _amountOfHusbands;
	}

	public void UpdateHusband() {
		_selectedHusband = _husbandlist [_selectedNumber].GetComponent<HusbandProperties> ();

		_husbandName.text = _selectedHusband._name;
		_husbandDescription.text = _selectedHusband._description;
		_husbandLongDescription.text = _selectedHusband._longDescription;

		_husbandImage.sprite = _selectedHusband._image;
		_husbandInfoImage.sprite = _selectedHusband._infoImage;
		
		_selectButton.interactable = _selectedHusband._unlocked;
		_infoButton.interactable = _selectedHusband._unlocked;
	}

}

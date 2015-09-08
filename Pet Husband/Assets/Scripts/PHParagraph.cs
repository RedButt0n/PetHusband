using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;
using Inklewriter.Unity;

public class PHParagraph : MonoBehaviour {
	/*
	public TextBlock text;
	public PHOptionButton option;
	public Text chosenOptionText;
	public NextButton nextButton;

	List<PHOptionButton> options = new List<PHOptionButton> ();
*/
	public Text text;
	public Text chosenOptionText;
	public Button optionButton;
	public Button nextButton;

	void Start()
	{
		//nextButton.gameObject.SetActive(true);
		//option.gameObject.SetActive (false);
	}

	public void SetText (Paragraph paragraph, Option chosenOption)
	{
		Debug.Log ("SetText PHParagraph");
		text.gameObject.SetActive (false);

		var obj = Instantiate (text.gameObject) as GameObject;
		obj.SetActive (true);
		obj.transform.SetParent (text.transform.parent,false);
		//obj.GetComponent<TextBlock> ().Set (paragraph);
		obj.GetComponent<Text>().text = paragraph.Text;
		//text.text = paragraph.Text;
			
		if (chosenOption != null) {
			chosenOptionText.gameObject.SetActive (true);
			chosenOptionText.text = chosenOption.Text;
		} else {
			chosenOptionText.gameObject.SetActive (false);
		}
	}

	public void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player)
	{
/*		option.gameObject.SetActive (false);
		nextButton.gameObject.SetActive(false);
		if(options.Count > 0)
		{
			foreach (var o in optionList) {
			if (!o.isVisible) {
				continue;
			}
			var obj = Instantiate (option.gameObject) as GameObject;
			obj.SetActive (true);
			obj.transform.SetParent (option.transform.parent);
			var optionButton = obj.GetComponent<PHOptionButton> ();
			optionButton.Set (o.content, player);
			options.Add (optionButton);
			}
			option.transform.parent.SetAsLastSibling ();
		} else
		{
			nextButton.gameObject.SetActive(true);
		}*/
	}
	
	public void Enable ()
	{
		/*option.transform.parent.gameObject.SetActive (true);
		foreach (var o in options) {
			o.Enable ();
		}
		chosenOptionText.gameObject.SetActive (false);*/
	}
	
	public void Disable ()
	{
		/*option.transform.parent.gameObject.SetActive (false);
		foreach (var o in options) {
			o.Disable ();
		}*/
	}
}

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
*/
	//List<PHOptionButton> options = new List<PHOptionButton> ();

	public Text text;
	public Text chosenOptionText;
	public Button optionButton;
	public Button nextButton;
	List<Button> options = new List<Button> ();

	void Start()
	{
		nextButton.gameObject.SetActive(true);
		optionButton.gameObject.SetActive (false);
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
		//option.gameObject.SetActive (false);
		nextButton.gameObject.SetActive(false);

		foreach (var o in optionList) {
			if (!o.isVisible) {
				continue;
			}
			var obj = Instantiate (optionButton.gameObject) as GameObject;
			obj.SetActive (true);
			obj.transform.SetParent (optionButton.transform.parent, false);

			//Add the onClick action
			Button newOptionButton = obj.GetComponent<Button> ();
			newOptionButton.onClick.AddListener(delegate {
				player.SelectOption (o.content);
			});

			//Text buttonText = obj.GetComponentInChildren<Text>();
			//buttonText.text = o.content.Text;
			//Set the text
			//obj.GetComponentsInChildren().text = o.content.Text;
			/*Text objText = obj.GetComponentInChildren<Text>();
			if(objText != null)
			{
				objText.text = o.content.Text;
			}*/

			//newOptionButton.GetComponentInChildren<Text>().text = o.content.Text;
			//newOptionButton.GetComponentInChildren<Text>().text = "bla";

			//optionButton.Set (o.content, player);
			options.Add (newOptionButton);
		}
		//option.transform.parent.SetAsLastSibling ();

		if(options.Count <= 0)
		{
			nextButton.gameObject.SetActive(true);
		}
	}
	
	public void Enable ()
	{
		//option.transform.parent.gameObject.SetActive (true);
		foreach (var o in options) {
			o.interactable = true;
		}
		chosenOptionText.gameObject.SetActive (false);
	}
	
	public void Disable ()
	{
		//option.transform.parent.gameObject.SetActive (false);
		foreach (var o in options) {
			o.interactable = false;
		}
	}
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;
using Inklewriter.Unity;
using System.Text.RegularExpressions;

public class PHParagraph : MonoBehaviour {
	/*
	public TextBlock text;
	public PHOptionButton option;
	public Text chosenOptionText;
	public NextButton nextButton;
*/
	//List<PHOptionButton> options = new List<PHOptionButton> ();

	public Text text;
	//public Text chosenOptionText;
	public Button optionButton;
	public Button nextButton;
	List<Button> options = new List<Button> ();

	public Image image;

	private bool enableNextButton = true;

	private GlobalVarContainer globalVarContainer;

	void Start()
	{
		//Debug.Log("Start");
		nextButton.gameObject.SetActive(enableNextButton);
		optionButton.gameObject.SetActive (false);
	}

	public void SetText (Paragraph paragraph, Option chosenOption, string prevImage)
	{
		//Debug.Log ("SetText PHParagraph");
		globalVarContainer = GameObject.Find("GlobalVarContainer").GetComponent<GlobalVarContainer>();
		text.gameObject.SetActive (false);

		var obj = Instantiate (text.gameObject) as GameObject;
		obj.SetActive (true);
		obj.transform.SetParent (text.transform.parent,false);
		//obj.GetComponent<TextBlock> ().Set (paragraph);
		obj.GetComponent<Text>().text = ProcessText(paragraph.Text);
		//text.text = paragraph.Text;
			
		/*if (chosenOption != null) {
			chosenOptionText.gameObject.SetActive (true);
			chosenOptionText.text = chosenOption.Text;
		} else {
			chosenOptionText.gameObject.SetActive (false);
		}*/

		//Set Image
		if (!string.IsNullOrEmpty (paragraph.Image)) {
			// Get file name without extension
			ConstructImage (paragraph.Image);
		} else
		{
			//If there is no image for this paragraph, use the image of the previous paragraph
			if(!string.IsNullOrEmpty (prevImage))
			{
				ConstructImage (prevImage);
			} else
			{
				image.gameObject.SetActive(false);
				//Debug.Log("No image name provided!");
			}
		}
	}

	public void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player)
	{
		//option.gameObject.SetActive (false);
		nextButton.gameObject.SetActive(false);

		foreach (var o in optionList) {
			if (!o.IsVisible) {
				continue;
			}
			var obj = Instantiate (optionButton.gameObject) as GameObject;
			obj.SetActive (true);
			obj.transform.SetParent (optionButton.transform.parent, false);

			//Add the onClick action
			Button newOptionButton = obj.GetComponent<Button> ();
			var optionContent = o.Content;
			newOptionButton.onClick.AddListener(delegate {
				player.SelectOption (optionContent);
			});

			//Set the text
			newOptionButton.gameObject.transform.FindChild("Text").GetComponent<Text>().text = o.Content.Text;

			//optionButton.Set (o.content, player);
			options.Add (newOptionButton);
		}
		//option.transform.parent.SetAsLastSibling ();

		if(options.Count <= 0)
		{
			nextButton.gameObject.SetActive(true);
			enableNextButton = true;
		} else
		{
			enableNextButton = false;
		}
	}
	
	public void Enable ()
	{
		//option.transform.parent.gameObject.SetActive (true);
		foreach (var o in options) {
			o.interactable = true;
		}
		//chosenOptionText.gameObject.SetActive (false);

		if(options.Count > 0)
		{
			enableNextButton = false;
		}
	}
	
	public void Disable ()
	{
		//option.transform.parent.gameObject.SetActive (false);
		foreach (var o in options) {
			o.interactable = false;
		}

		nextButton.enabled = false;
		enableNextButton = false;
		//nextButton.gameObject.SetActive(false);
	}

	private string FileNameWithoutExtension (string path)
	{
		var pattern = @"[^/]*(?=\.[^.]+($|\?))";
		var match = Regex.Match (path, pattern);
		if(match.Success)
		{
			return match.Groups[0].Value;
		} else
		{
			return path;
		}
	}

	public string GetImageName()
	{
		string imageName = "";
		if(image.sprite != null)
		{
			imageName = image.sprite.name;
		}

		return imageName;
	}

	void ConstructImage (string rawImageName)
	{
		var imageName = FileNameWithoutExtension (rawImageName);
		var sprite = Resources.Load<Sprite> (imageName);
		if (sprite) {
			//Debug.Log("image name: " + imageName);
			image.sprite = sprite;
		} else
		{
			//Debug.Log("Failed to construct image! raw ImageName: " + rawImageName + " imageName: " + imageName);
		}
	}

	string ProcessText(string text)
	{
		string processText = text.Replace("%naam%",globalVarContainer._playerName);
		return processText;
	}
}

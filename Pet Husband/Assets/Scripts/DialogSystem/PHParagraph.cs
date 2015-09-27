﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;
using Inklewriter.Unity;
using System.Text.RegularExpressions;

public class PHParagraph : APHParagraph
{
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

    public override void Initialize(ParagraphParser parser, Option chosenOption, string prevImage)
    {
        text.gameObject.SetActive(false);

        //Retrieve message Text
        var obj = Instantiate(text.gameObject) as GameObject;
        obj.SetActive(true);
        obj.transform.SetParent(text.transform.parent, false);
        obj.GetComponent<Text>().text = parser.ExtractMessage();

        //Retrieve task text
        string taskText = parser.ExtractTask();
        SetAndDisplayTaskText(taskText);

        //Retrieve image Path
        SetAndDisplayImage(ProcessImageFilename(parser,prevImage));
    }

    public override void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player)
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

    public override void Enable()
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

    public override void Disable()
	{
		//option.transform.parent.gameObject.SetActive (false);
		foreach (var o in options) {
			o.interactable = false;
		}

		nextButton.enabled = false;
		enableNextButton = false;
		//nextButton.gameObject.SetActive(false);
	}

    public override string GetImageName()
    {
        string imageName = "";
        if (image.sprite != null)
        {
            imageName = image.sprite.name;
        }

        return imageName;
    }

    private void SetAndDisplayImage(string imageFileName)
    {
        if (!string.IsNullOrEmpty(imageFileName))
        {
            var sprite = Resources.Load<Sprite>(imageFileName);
            if (sprite != null)
            {
                image.sprite = sprite;
            } else
            {
                Debug.Log("Failed to construct image! imageFileName: " + imageFileName);
            }
        }
        else
        {
            //disable the image
            Debug.Log("An empty filename has been provided as image file path, no image could be shown");
            image.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
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

	public Image characterImage;
    public Image backgroundImage;

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

        //Retrieve image Path of Character
        SetAndDisplayCharacterImage(ProcessImageFilename(parser,prevImage));

        ChangeBackground(parser);
    }

    private void ChangeBackground(ParagraphParser parser)
    {
        string backgroundImage = parser.ExtractBackgroundImage();
        if (!string.IsNullOrEmpty(backgroundImage))
        {
            SetAndDisplayBackgroundImage(backgroundImage);
            GameObject.Find("GlobalStoryVarContainer").GetComponent<GlobalStoryVarContainer>().PreviousBackgroundImage = backgroundImage;
        }
        else
        {
            backgroundImage = GameObject.Find("GlobalStoryVarContainer").GetComponent<GlobalStoryVarContainer>().PreviousBackgroundImage;
            if (!string.IsNullOrEmpty(backgroundImage))
            {
                SetAndDisplayBackgroundImage(backgroundImage);
            }
        }
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
        if (characterImage.sprite != null)
        {
            imageName = characterImage.sprite.name;
        }

        return imageName;
    }

    private void SetAndDisplayCharacterImage(string imageFileName)
    {
        SetAndDisplayImage(characterImage, imageFileName);
    }

    private void SetAndDisplayBackgroundImage(string imageFileName)
    {
        SetAndDisplayImage(backgroundImage, imageFileName);
    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;
using Inklewriter.Unity;
using System.Text.RegularExpressions;

struct OptionData
{
	public Sprite optionSprite;
	public UnityEngine.Events.UnityAction optionAction;
}

public class PHParagraphTV : APHParagraph
{
	public Text text;
	//public Text chosenOptionText;
	//public Button optionButton;
	public Button nextButton;
	//List<Button> options = new List<Button> ();
	
	public Image optionImage;
	
	private bool enableNextButton = true;
	
	//private GlobalVarContainer globalVarContainer;

	private LinkedList<OptionData> options = new LinkedList<OptionData> ();
	private LinkedListNode<OptionData> activeOption;
	
	public Button playButton;

    private TextParser _TextParser = new TextParser();

    private GlobalStoryVarContainer m_GlobalStoryVarContainer;
	
	void Start()
	{
		//Debug.Log("Start");
		nextButton.gameObject.SetActive(enableNextButton);
		//optionButton.gameObject.SetActive (false);
        m_GlobalStoryVarContainer = GameObject.Find("GlobalStoryVarContainer").GetComponent<GlobalStoryVarContainer>();
	}

    public override void Initialize(ParagraphParser parser, Option chosenOption, string prevImage)
    {
        text.gameObject.SetActive(false);

        //Retrieve message Text
        var obj = Instantiate(text.gameObject) as GameObject;
        obj.SetActive(true);
        obj.transform.SetParent(text.transform.parent, false);
		SetAndDisplayMessageText(parser, obj.GetComponent<Text>());

        //Retrieve task text
        string taskText = parser.ExtractTask();
        SetAndDisplayTaskText(taskText);

        //Retrieve image Path
        //SetAndDisplayImage(ProcessImageFilename(parser, prevImage));
    }
	
	public override void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player)
	{
		//option.gameObject.SetActive (false);
		nextButton.gameObject.SetActive(false);
		
		foreach (var o in optionList) {
			if (!o.IsVisible) {
				continue;
			}
			/*var obj = Instantiate (optionImage.gameObject) as GameObject;
			obj.SetActive (true);
			obj.transform.SetParent (optionImage.transform.parent, false);*/

			OptionData optionData;
			//Add Image to option
            string imagePath = ParseImageFileName(o.Content.Text);
            optionData.optionSprite = ConstructSprite(imagePath);
			
            //Add the onClick action
            var optionContent = o.Content;
            optionData.optionAction = delegate
            {
                player.SelectOption(optionContent);
                m_GlobalStoryVarContainer.SelectedMovie = imagePath;
            };
            
			options.AddLast(optionData);
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
		/*foreach (var o in options) {
			o.interactable = true;
		}*/
		//chosenOptionText.gameObject.SetActive (false);

		activeOption = options.First;
        ShowActiveOption();
		
		if(options.Count > 0)
		{
			enableNextButton = false;
		}
	}

	private void ShowActiveOption()
	{
		optionImage.sprite = activeOption.Value.optionSprite;
		playButton.onClick.RemoveAllListeners();
		playButton.onClick.AddListener(activeOption.Value.optionAction);
	}

	public void GoToNextOption()
	{
        if (activeOption.Next != null)
        {
            activeOption = activeOption.Next;
        }
        else
        {
            activeOption = options.First;
        }
		ShowActiveOption();
	}

	public void GoToPreviousOption()
	{
        if (activeOption.Previous != null)
        {
            activeOption = activeOption.Previous;
        }
        else
        {
            activeOption = options.Last;
        }
		ShowActiveOption();
	}

    public override void Disable()
	{
		//option.transform.parent.gameObject.SetActive (false);
		/*foreach (var o in options) {
			o.interactable = false;
		}*/
		
		nextButton.enabled = false;
		enableNextButton = false;
		//nextButton.gameObject.SetActive(false);
	}
	
	public override string GetImageName()
	{
        //string imageName = "";
        //if(optionImage.sprite != null)
        //{
        //    imageName = optionImage.sprite.name;
        //}
		
        //return imageName;

        return string.Empty;
	}

    string ParseImageFileName(string text)
    {
        string imagePath = _TextParser.ExtractDataBetweenTags(ref text, "< Image >", "< /Image >");
        return _TextParser.FileNameWithoutExtension(imagePath);
    }
}

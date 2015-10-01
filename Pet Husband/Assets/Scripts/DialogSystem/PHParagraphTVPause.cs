using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;
using Inklewriter.Unity;
using System.Text.RegularExpressions;
using UnityEngine.Events;

public class PHParagraphTVPause : APHParagraph
{
    public Text text;
    //public Text chosenOptionText;
    //public Button optionButton;
    public Button nextButton;

    public Image screenImage;

    private bool enableNextButton = false;

    //private GlobalVarContainer globalVarContainer;

    public Button pauseButton;

    private UnityEvent m_EndTimerEvent = new UnityEvent();
    //private UnityEngine.Events.UnityAction endTimerAction;

    public float _delay;
    private float _timer = 0.0f;
    private bool _startTimer = false;

    void Start()
    {
        //Debug.Log("Start");
        nextButton.gameObject.SetActive(enableNextButton);
        //optionButton.gameObject.SetActive (false);
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
        GlobalStoryVarContainer globalStoryVarContainer = GameObject.Find("GlobalStoryVarContainer").GetComponent<GlobalStoryVarContainer>();
        SetAndDisplayImage(screenImage,globalStoryVarContainer.GetImageOfMovieCurrentlyPlaying());
    }

    public override void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player)
    {
        //option.gameObject.SetActive (false);
        nextButton.gameObject.SetActive(false);

        foreach (var o in optionList)
        {
            if (!o.IsVisible)
            {
                continue;
            }

            var optionContent = o.Content;

            if (string.Equals(optionContent.Text,"(Pauzeer film)"))
            {
               
                pauseButton.onClick.AddListener(delegate
                {
                    player.SelectOption(optionContent);
                    _startTimer = false;
                });
            }
            else if (string.Equals(optionContent.Text,"(Pauzeer film niet)"))
            {
                m_EndTimerEvent.AddListener( delegate
                {
                    player.SelectOption(optionContent);
                });
            }
        }
    }

    public override void Enable()
    {
        //Start timer
        _startTimer = true;

    }

    public override void Disable()
    {
        nextButton.enabled = false;
        enableNextButton = false;
    }

    public override string GetImageName()
    {
        return string.Empty;
    }

    void Update()
    {
        if (_startTimer)
        {
            _timer += Time.deltaTime;
            if (_timer >= _delay)
            {
                m_EndTimerEvent.Invoke();
            }
        }
    }
}

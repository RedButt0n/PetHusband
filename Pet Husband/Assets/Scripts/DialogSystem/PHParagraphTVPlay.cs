using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;
using Inklewriter.Unity;
using System.Text.RegularExpressions;
using UnityEngine.Events;

public class PHParagraphTVPlay : APHParagraph
{
    public Text text;
    //public Text chosenOptionText;
    //public Button optionButton;
    public Button nextButton;

    public Image screenImage;

    private bool enableNextButton = false;

    //private GlobalVarContainer globalVarContainer;

    public Button playButton;

    private UnityEvent m_EndTimerEvent = new UnityEvent();
    //private UnityEngine.Events.UnityAction endTimerAction;

    public float _delay;
    private float _timer = 0.0f;
    private bool _startTimer = false;

    private GlobalStoryVarContainer _globalStoryVarContainer;

    public PetHusbandPlayer _player;

    private Color _blackImgColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    private Color _originalImgColor;
    private bool _fadeInComplete = false;

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
        obj.GetComponent<Text>().text = parser.ExtractMessage();

        //Retrieve task text
        string taskText = parser.ExtractTask();
        SetAndDisplayTaskText(taskText);

        //Retrieve image Path
        //SetAndDisplayImage(ProcessImageFilename(parser, prevImage));
        _globalStoryVarContainer = GameObject.Find("GlobalStoryVarContainer").GetComponent<GlobalStoryVarContainer>();
        SetAndDisplayImage(screenImage, _globalStoryVarContainer.GetMenuImageOfMovieCurrentlyPlaying());
        _originalImgColor = screenImage.color;

        m_EndTimerEvent.AddListener(delegate
        {
            _player.GoToNextParagraph();
        });
    }

    public override void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player)
    {
        //option.gameObject.SetActive (false);
        nextButton.gameObject.SetActive(false);      
    }

    public override void Enable()
    {
        //Start timer
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
                _startTimer = false;
                m_EndTimerEvent.Invoke();
            }
            else
            {
                if (!_fadeInComplete)
                {
                    FadeIn();
                }
            }
        }
    }

    public void ShowFilm()
    {
        playButton.onClick.RemoveAllListeners();

        SetAndDisplayImage(screenImage, _globalStoryVarContainer.GetImageOfMovieCurrentlyPlaying());
        _startTimer = true;
    }

    private void FadeIn()
    {
        if (_timer < 1)
        {
            screenImage.color = Color.Lerp(_blackImgColor, _originalImgColor, _timer * 2f);
        }

        else
        {
            _fadeInComplete = true;
        }
    }
}

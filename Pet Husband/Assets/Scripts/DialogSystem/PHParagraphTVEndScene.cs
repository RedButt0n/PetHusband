using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;
using Inklewriter.Unity;
using System.Text.RegularExpressions;
using UnityEngine.Events;

public class PHParagraphTVEndScene : APHParagraph
{

    public float _delay;

    private UnityEvent  m_EndTimerEvent = new UnityEvent();
    private float       _timer          = 0.0f;
    private bool        _startTimer     = false;

    public PetHusbandPlayer _player;

    void Start()
    {
    }

    public override void Initialize(ParagraphParser parser, Option chosenOption, string prevImage)
    {
        //Retrieve task text
        string taskText = parser.ExtractTask();
        SetAndDisplayTaskText(taskText);

        m_EndTimerEvent.AddListener(delegate
        {
            _player.GoToNextParagraph();
        });
    }

    public override void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player)
    {
    }

    public override void Enable()
    {
        //Start timer
        _startTimer = true;
    }

    public override void Disable()
    {
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
        }
    }
}

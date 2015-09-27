using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;
using Inklewriter.Unity;

public class PHParagraphEndScene : APHParagraph
{
    private string _sceneToLoad;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Initialize(ParagraphParser parser, Option chosenOption, string prevImage)
    {
        _sceneToLoad = parser.ExtractScene();
    }

    public override void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player)
    {
    }

    public override void Enable()
    {
        if(!string.IsNullOrEmpty(_sceneToLoad))
        {
            Application.LoadLevel(_sceneToLoad);
        }
    }

    public override void Disable()
    {
    }

    public override string GetImageName()
    {
        return string.Empty;
    }
}

using UnityEngine;
using System.Collections.Generic;
using Inklewriter;
using Inklewriter.Player;
using Inklewriter.Unity;

public interface IPHParagraph 
{
    void Initialize(ParagraphParser parser, Option chosenOption, string prevImage);
    //void SetText(Paragraph paragraph, Option chosenOption, string prevImage);
    void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player);
    void Enable();
    void Disable();
    string GetImageName();
}

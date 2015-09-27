using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Inklewriter;
using Inklewriter.Player;
using Inklewriter.Unity;

public class ParagraphParser : TextParser
{
    //public StoryParser();

    private Paragraph           paragraphToParse;
    private GlobalVarContainer   m_globalVarContainer;

    public Paragraph ParagraphToParse
    {
        get { return paragraphToParse; }
        set { paragraphToParse = value; }
    }

    public ParagraphParser(GlobalVarContainer globalVarContainer)
    {
        m_globalVarContainer = globalVarContainer;
    }

    public ParagraphParser(GlobalVarContainer globalVarContainer, Paragraph paragraph)
    {
        m_globalVarContainer    = globalVarContainer;
        paragraphToParse        = paragraph;
    }

    public string ExtractTask()
    {
        return ExtractDataBetweenTags(paragraphToParse.Text, "< taak >", "< /taak >");
    }

    public string ExtractParagraphType()
    {
        return ExtractDataBetweenTags(paragraphToParse.Text, "< Type >", "< /Type >");
    }

    public string ExtractMessage()
    {
        string messageText = paragraphToParse.Text;

        //remove all tags from the text
        RemoveTagsFromText(ref messageText);

        //Replace the player name if there is any
        messageText = ReplacePlayerName(messageText);

        //Remove double spaces and tabs, newlines
        messageText = Regex.Replace(messageText, @"\s+", " ");

        return messageText;
    }

    public string ReplacePlayerName(string text)
    {
        return text.Replace("%naam%", m_globalVarContainer._playerName);
    }

    public string ExtractImageFileName()
    {
        return FileNameWithoutExtension(paragraphToParse.Image);
    }
   
}

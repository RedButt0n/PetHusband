using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class TextParser
{
    public string ExtractFileName(string path)
    {
        var pattern = @"[^/]*(?=\.[^.]+($|\?))";
        var match = Regex.Match(path, pattern);
        if (match.Success)
        {
            return match.Groups[0].Value;
        }
        else
        {
            return path;
        }
    }

    public string ExtractDataBetweenTags(ref string text, string beginTag, string endTag, bool removeTagsFromText = false)
    {
        string outputText = string.Empty;
        if (text.Contains(beginTag))
        {
            int beginTagIndex = text.IndexOf(beginTag);
            int endTagIndex = text.IndexOf(endTag);

            outputText = text.Substring(beginTagIndex + beginTag.Length, endTagIndex - (beginTagIndex + beginTag.Length));

            if (removeTagsFromText)
            {
                text = text.Remove(beginTagIndex, endTagIndex + endTag.Length - beginTagIndex);
            }
        }
        return outputText;
    }

    public string ExtractDataBetweenTags(string text, string tagWithoutSyntax)
    {
        string beginTag = "< " + tagWithoutSyntax + " >";
        string endTag   = "< /" + tagWithoutSyntax + " >";

        return ExtractDataBetweenTags(text, beginTag, endTag);
    }

    public string ExtractDataBetweenTags(string text, string beginTag, string endTag)
    {
        string outputText = string.Empty;
        if (text.Contains(beginTag))
        {
            int beginTagIndex = text.IndexOf(beginTag);
            int endTagIndex = text.IndexOf(endTag);

            outputText = text.Substring(beginTagIndex + beginTag.Length, endTagIndex - (beginTagIndex + beginTag.Length));
            outputText = RemoveSpaces(outputText);
        }
        return outputText;
    }

    public void RemoveTagsFromText(ref string text)
    {
        int openingTagStartIdx = text.IndexOf("< ");
        while (openingTagStartIdx != -1)
        {
            int range = FindRangeToDeleteOfTag(text, openingTagStartIdx);
            text = text.Remove(openingTagStartIdx, range);
            openingTagStartIdx = text.IndexOf("< ");
        }
    }

    private int FindRangeToDeleteOfTag(string text, int startIndex)
    {       
        int range = 0;

        //See if we can find an endtag
        int closingTagStartIdx = text.IndexOf("< /", startIndex);
        if (closingTagStartIdx != -1)
        {
            //we found the beginning of the endtag, find the end of the endtag
            int closingTagEndIdx = text.IndexOf(">", closingTagStartIdx);
            if (closingTagEndIdx != -1)
            {
                range = closingTagEndIdx - startIndex;
            }
            else
            {
                //delete everything from the startIndex to the beginning of  the endtag
                range = closingTagStartIdx - startIndex;
            }
        }
        else
        {
            //no endtag found, see if we can find the end of the starttag
            int closingTagEndIdx = text.IndexOf(">", startIndex);
            if (closingTagEndIdx != -1)
            {
                range = closingTagEndIdx - startIndex;
            }
        }

        range += 1;

        return range;
    }

    public string FileNameWithoutExtension(string path)
    {
        string fileNameWithoutExtension = path;

        if (!string.IsNullOrEmpty(path))
        {
            var pattern = @"[^/]*(?=\.[^.]+($|\?))";
            var match = Regex.Match(path, pattern);
            if (match.Success)
            {
                fileNameWithoutExtension = match.Groups[0].Value;
            }
        }

        return fileNameWithoutExtension;
    }

    public string RemoveSpaces(string text)
    {
        return Regex.Replace(text, @"\s+", "");
    }
}

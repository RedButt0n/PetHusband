using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Inklewriter;
using Inklewriter.Player;
using Inklewriter.Unity;

public abstract class APHParagraph :MonoBehaviour, IPHParagraph
{
    abstract public void Initialize(ParagraphParser parser, Option chosenOption, string prevImage);
    //abstract public void SetText(Paragraph paragraph, Option chosenOption, string prevImage);
    abstract public void SetOptions(List<BlockContent<Option>> optionList, PetHusbandPlayer player);
    abstract public void Enable();
    abstract public void Disable();
    abstract public string GetImageName();

    protected string ProcessImageFilename(ParagraphParser parser, string prevImageFileName)
    {
        string imageFileName = parser.ExtractImageFileName();
        if (string.IsNullOrEmpty(imageFileName))
        {
            //the current paragraph doesnt have an image, use the image from the previous one
            imageFileName = prevImageFileName;
        }

        return imageFileName;
    }

    protected void SetAndDisplayTaskText(string taskText)
    {
        if (!string.IsNullOrEmpty(taskText))
        {
            var taskBehaviour = GameObject.Find("Task").GetComponent<TaskBehavior>();
            if (taskBehaviour != null)
            {
                taskBehaviour.SetText(taskText);
                taskBehaviour.Show();
            }
        }
    }

    protected Sprite ConstructSprite(string imageFileName)
    {
        Sprite sprite = null;
        if (!string.IsNullOrEmpty(imageFileName))
        {
            sprite = Resources.Load<Sprite>(imageFileName);
        }
        else
        {
            Debug.Log("An empty filename has been provided as image file path, no image could be shown");           
        }

        return sprite;
    }

    protected void SetAndDisplayImage(Image image, string imageFileName)
    {
        var sprite = ConstructSprite(imageFileName);
        if (sprite != null)
        {
            image.sprite = sprite;
        }
        else
        {
            Debug.Log("Failed to construct image! imageFileName: " + imageFileName);
            //disable the image
            image.gameObject.SetActive(false);
        } 
    }

}

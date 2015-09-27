using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;

public class PHChunk : MonoBehaviour {

	public GameObject activeParagraph;
	List<GameObject> paragraphs = new List<GameObject> ();
	int activeParagraphIndex = 0;

    public ParagraphFactory _paragraphFactory;

    private string _lastShownImage;

	public void Set (PlayChunk chunk, Option chosenOption, PetHusbandPlayer player, string prevChunkImage)
	{
		activeParagraph.gameObject.SetActive (false);
		string prevImage = prevChunkImage;
        _lastShownImage = prevChunkImage;
		foreach (var p in chunk.Paragraphs) {

            var newParagraph = _paragraphFactory.CreateParagraph(p, chosenOption, prevImage, out prevImage);
            newParagraph.transform.SetParent(activeParagraph.transform.parent, false);
            paragraphs.Add(newParagraph);
            /*
			var newParagraph = Instantiate (activeParagraph) as GameObject;
			newParagraph.SetActive (false);
			newParagraph.transform.SetParent (activeParagraph.transform.parent,false);
			var paragraphComponent = newParagraph.GetComponent<PHParagraph>();
            //newParagraph.
			paragraphComponent.SetText(p,chosenOption,prevImage);
			prevImage = paragraphComponent.GetImageName();
			paragraphs.Add(newParagraph);*/
		}

        GetScriptComponentOfParagraph(paragraphs[paragraphs.Count - 1]).SetOptions(chunk.Options, player);

		activeParagraph = paragraphs[0];
		activeParagraph.SetActive(true);
        var paragraphComponent = GetScriptComponentOfParagraph(activeParagraph);
        paragraphComponent.Enable();
	}

	public void GoToNextparagraph()
	{
		++activeParagraphIndex;
		//Debug.Log("GoToNextparagraph: " + activeParagraphIndex); 
		//Debug.Log("paragrphas: " + paragraphs.Count);
        if (activeParagraphIndex < paragraphs.Count)
        {
            //activeParagraph.SetActive(false);
            //var paragraphComponent = activeParagraph.GetComponent<PHParagraph>();
            //paragraphComponent.Disable();

            //activeParagraph = paragraphs[activeParagraphIndex];
            //paragraphComponent = activeParagraph.GetComponent<PHParagraph>();
            //paragraphComponent.Enable();
            //activeParagraph.SetActive(true);

            activeParagraph.SetActive(false);
            var paragraphComponent = GetScriptComponentOfParagraph(activeParagraph);
            paragraphComponent.Disable();

            activeParagraph = paragraphs[activeParagraphIndex];
            paragraphComponent = GetScriptComponentOfParagraph(activeParagraph);
            paragraphComponent.Enable();
            activeParagraph.SetActive(true);
        }
	}
	
	public void Enable ()
	{
		/*option.transform.parent.gameObject.SetActive (true);
		foreach (var o in options) {
			o.Enable ();
		}
		chosenOptionText.gameObject.SetActive (false);	*/
	}
	
	public void Disable ()
	{
		/*option.transform.parent.gameObject.SetActive (false);
		foreach (var o in options) {
			o.Disable ();
		}*/
	}

	public string GetLastShownImage()
	{
		//return activeParagraph.GetComponent<PHParagraph>().GetImageName();
        string activeImage = GetScriptComponentOfParagraph(activeParagraph).GetImageName();
        if(!string.IsNullOrEmpty(activeImage))
        {
            _lastShownImage = activeImage;
        }
        return _lastShownImage;
	}

    private IPHParagraph GetScriptComponentOfParagraph(GameObject gameObject)
    {
        return gameObject.GetComponent(typeof(IPHParagraph)) as IPHParagraph;
    }
}

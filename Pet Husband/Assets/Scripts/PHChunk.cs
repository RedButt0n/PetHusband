using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;

public class PHChunk : MonoBehaviour {

	public GameObject activeParagraph;
	List<GameObject> paragraphs = new List<GameObject> ();
	int activeParagraphIndex = 0;

	public void Set (PlayChunk chunk, Option chosenOption, PetHusbandPlayer player, string prevChunkImage)
	{
		activeParagraph.gameObject.SetActive (false);
		string prevImage = prevChunkImage;
		foreach (var p in chunk.Paragraphs) {
			var newParagraph = Instantiate (activeParagraph) as GameObject;
			newParagraph.SetActive (false);
			newParagraph.transform.SetParent (activeParagraph.transform.parent,false);
			var paragraphComponent = newParagraph.GetComponent<PHParagraph>();
			paragraphComponent.SetText(p,chosenOption,prevImage);
			prevImage = paragraphComponent.GetImageName();
			paragraphs.Add(newParagraph);
		}

		paragraphs[paragraphs.Count - 1].GetComponent<PHParagraph>().SetOptions(chunk.Options,player);

		activeParagraph = paragraphs[0];
		activeParagraph.SetActive(true);
	}

	public void GoToNextparagraph()
	{
		++activeParagraphIndex;
		//Debug.Log("GoToNextparagraph: " + activeParagraphIndex); 
		//Debug.Log("paragrphas: " + paragraphs.Count);
		if(activeParagraphIndex < paragraphs.Count)
		{
			activeParagraph.SetActive(false);
			var paragraphComponent = activeParagraph.GetComponent<PHParagraph>();
			paragraphComponent.Disable();

			activeParagraph = paragraphs[activeParagraphIndex];
			paragraphComponent = activeParagraph.GetComponent<PHParagraph>();
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
		return activeParagraph.GetComponent<PHParagraph>().GetImageName();
	}
}

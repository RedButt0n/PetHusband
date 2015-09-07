using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Inklewriter.Player;
using Inklewriter;

public class PHChunk : MonoBehaviour {

	public GameObject activeParagraph;
	List<GameObject> paragraphs = new List<GameObject> ();
	int activeParagraphIndex = -1;

	public void Set (PlayChunk chunk, Option chosenOption, PetHusbandPlayer player)
	{
		activeParagraph.gameObject.SetActive (false);
		foreach (var p in chunk.Paragraphs) {
			var newParagraph = Instantiate (activeParagraph) as GameObject;
			newParagraph.SetActive (false);
			newParagraph.transform.SetParent (activeParagraph.transform.parent);
			var paragraphComponent = newParagraph.GetComponent<PHParagraph>();
			paragraphComponent.SetText(p,chosenOption);
			paragraphs.Add(newParagraph);
		}

		paragraphs[paragraphs.Count - 1].GetComponent<PHParagraph>().SetOptions(chunk.Options,player);

		activeParagraph = paragraphs[0];
		activeParagraph.SetActive(true);
	}

	public void GoToNextparagraph()
	{
		++activeParagraphIndex;
		activeParagraph.SetActive(false);

		activeParagraph = paragraphs[activeParagraphIndex];
		activeParagraph.SetActive(true);
	}
	
	public void Enable ()
	{
		/*option.transform.parent.gameObject.SetActive (true);
		foreach (var o in options) {
			o.Enable ();
		}
		chosenOptionText.gameObject.SetActive (false);*/
	}
	
	public void Disable ()
	{
		/*option.transform.parent.gameObject.SetActive (false);
		foreach (var o in options) {
			o.Disable ();
		}*/
	}
}

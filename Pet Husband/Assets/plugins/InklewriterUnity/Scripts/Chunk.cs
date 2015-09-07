﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inklewriter.Player;

namespace Inklewriter.Unity
{
	public class Chunk : MonoBehaviour
	{
		public TextBlock text;
		public OptionButton option;
		public Text chosenOptionText;

		List<OptionButton> options = new List<OptionButton> ();
		
		public void Set (PlayChunk chunk, Option chosenOption, InklewriterPlayer player)
		{
			text.gameObject.SetActive (false);
			foreach (var p in chunk.Paragraphs) {
				var obj = Instantiate (text.gameObject) as GameObject;
				obj.SetActive (true);
				obj.transform.SetParent (text.transform.parent);
				obj.GetComponent<TextBlock> ().Set (p);
			}

			/*var paragraph = Instantiate (text.gameObject) as GameObject;
			paragraph.SetActive (true);
			paragraph.transform.SetParent (text.transform.parent);
			paragraph.GetComponent<TextBlock> ().Set (chunk.Paragraphs[0]);*/

			option.gameObject.SetActive (false);
			foreach (var o in chunk.Options) {
				if (!o.isVisible) {
					continue;
				}
				var obj = Instantiate (option.gameObject) as GameObject;
				obj.SetActive (true);
				obj.transform.SetParent (option.transform.parent);
				var optionButton = obj.GetComponent<OptionButton> ();
				optionButton.Set (o.content, player);
				options.Add (optionButton);
			}
			option.transform.parent.SetAsLastSibling ();

			if (chosenOption != null) {
				chosenOptionText.gameObject.SetActive (true);
				chosenOptionText.text = chosenOption.Text;
			} else {
				chosenOptionText.gameObject.SetActive (false);
			}
		}

		public void Enable ()
		{
			option.transform.parent.gameObject.SetActive (true);
			foreach (var o in options) {
				o.Enable ();
			}
			chosenOptionText.gameObject.SetActive (false);
		}

		public void Disable ()
		{
			option.transform.parent.gameObject.SetActive (false);
			foreach (var o in options) {
				o.Disable ();
			}
		}
	}
	
}
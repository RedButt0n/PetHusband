using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Inklewriter;

public class NextButton : MonoBehaviour {

	public Text text;
	public Button button;
	public PetHusbandPlayer player;
	
	public void Start ()
	{		
		button.onClick.AddListener (delegate {
			player.GoToNextParagraph();
		});
	}
	
	public void Enable ()
	{
		button.interactable = true;
	}
	
	public void Disable ()
	{
		button.interactable = false;
	}
}

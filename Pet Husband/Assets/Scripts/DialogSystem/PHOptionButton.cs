using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Inklewriter;

public class PHOptionButton : MonoBehaviour {

	public Text text;
	public Button button;
	
	public void Set (Option option, PetHusbandPlayer player)
	{
		this.text.text = option.Text;
		
		button.onClick.AddListener (delegate {
			player.SelectOption (option);
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

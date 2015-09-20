using UnityEngine;
using System.Collections;

public class TaskBehavior : MonoBehaviour {

	public string _text;
	public Sprite _icon;
	public Sprite _bgImage;

	private void Start()
	{
		this.enabled = false;
	}

	public void Create(string text, string iconPath)
	{
		Sprite icon = Resources.Load(iconPath) as Sprite;
		this._text = text;
		this._icon = icon;
	}

	public void ShowFirstTime()
	{
		this.enabled = true;
	}
}

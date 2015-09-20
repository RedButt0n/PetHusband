using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TaskBehavior : MonoBehaviour {

	public string _text;
	public Sprite _icon;
	public float  _showTime = 4f;

	private float _counter = 0.0f;
	private Text _textComponent;
	private Image _bgImgComponent;
	private Canvas _canvas;

	private void Start()
	{
		_textComponent = this.GetComponentInChildren<Text>();
		_textComponent.enabled = false;
		
		_canvas = this.GetComponent<Canvas>();
		_canvas.enabled = false;

		
		_bgImgComponent = this.transform.FindChild ("BgImage").GetComponent<Image> ();
		_bgImgComponent.enabled = false;
	}

	private void Update()
	{
		if(_textComponent.enabled)
		{
			if (_counter < _showTime) {
				_counter += Time.deltaTime;
			}
			else
			{
				_textComponent.enabled = false;
				_bgImgComponent.enabled = false;
			}
		}
	}

	public void Create(string text, string iconPath)
	{
		Sprite icon = Resources.Load(iconPath) as Sprite;
		this._text = text;
		this._icon = icon;
	}

	public void ShowFirstTime()
	{
		ShowText ();
		_canvas.enabled = true;
	}

	public void ShowText()
	{
		_textComponent.enabled = true;
		_bgImgComponent.enabled = true;
		_counter = 0.0f;
	}
}

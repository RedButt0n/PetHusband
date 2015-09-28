using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TaskBehavior : MonoBehaviour {

	public string _text;
	public Sprite _icon;
	
	private Text _textComponent;
	private Image _bgImgComponent;
	private Canvas _canvas;
	
	private Color _originalImgColor;
	private Color _originalTxtColor;

	private Color _transparantImgColor;
	private Color _transparantTxtColor;

	private enum VisibilityState
	{
		Hidden,
		FadeIn,
		Visible,
		FadeOut
	}
	
	private VisibilityState _visibility = VisibilityState.Hidden;

	public float  _showTime = 4f;
	private float _timer = 0f;
	
	private void Start()
	{
		_canvas = this.GetComponent<Canvas>();
		_canvas.enabled = false;

		_bgImgComponent = this.transform.FindChild ("BgImage").GetComponent<Image> ();
		_textComponent = this.GetComponentInChildren<Text>();

		_originalImgColor = _bgImgComponent.color;
		_originalTxtColor = _textComponent.color;

		_transparantImgColor = new Color (_originalImgColor.r, _originalImgColor.g, _originalImgColor.b, 0);
		_transparantTxtColor = new Color (_textComponent.color.r, _textComponent.color.g, _textComponent.color.b, 0);
	}

	private void Update()
	{
		switch (_visibility)
		{
			case VisibilityState.FadeIn:
				FadeIn();
				break;

			case VisibilityState.Visible:
				_timer += Time.deltaTime;
				if(_timer > _showTime)
				{
					_visibility = VisibilityState.FadeOut;
					_timer = 0;
				}
				break;

			case VisibilityState.FadeOut:
				FadeOut();
				break;

			case VisibilityState.Hidden:
				_bgImgComponent.color = _transparantImgColor;
				_textComponent.color = _transparantTxtColor;
				break;
		}
	}

	public void Create(string text, string iconPath)
	{
		Sprite icon = Resources.Load(iconPath) as Sprite;
		this._text = text;
		this._icon = icon;
	}

	public void Show()
	{
		_visibility = VisibilityState.FadeIn;
		_canvas.enabled = true;
	}

	public void SetText(string text)
	{
		_textComponent.text = text;
	}

	private void FadeOut()
	{
		if (_timer < 1)
		{
			_timer += Time.deltaTime;
			_bgImgComponent.color = Color.Lerp (_originalImgColor, _transparantImgColor, _timer * 2f);
			_textComponent.color = Color.Lerp (_originalTxtColor, _transparantTxtColor, _timer * 2f);
		}

		else
		{
			_visibility = VisibilityState.Hidden;
			_timer=0;
		}
	}

	private void FadeIn()
	{
		if (_timer < 1) {
			_timer += Time.deltaTime;
			_bgImgComponent.color = Color.Lerp (_transparantImgColor, _originalImgColor, _timer * 2f);
			_textComponent.color = Color.Lerp (_transparantTxtColor, _originalTxtColor, _timer * 2f);
		} 

		else
		{
			_visibility = VisibilityState.Visible;
			_timer=0;
		}
	}
}

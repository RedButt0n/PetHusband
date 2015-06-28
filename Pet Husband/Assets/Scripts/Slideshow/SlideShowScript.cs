using UnityEngine;
using System.Collections;

public class SlideShowScript : MonoBehaviour {

	public FrameProperties[] _framelist;
	private Rect _screen;
	private float _timer = 0.0f;
	private float _delay;

	// Use this for initialization
	void Start () {
	
		_screen = new Rect (0, 0, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void OnGUI () {
			GUI.DrawTexture (_screen, _framelist[0]._image);
	}
}

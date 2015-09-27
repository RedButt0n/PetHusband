using UnityEngine;
using System.Collections;

public class CutSceneScript : MonoBehaviour {

	public FrameProperties[] _framelist;	//All frames that have to be loaded should be attached as children to the prefab with this script

	private int currentFrame = -1;			//start at -1 so NextFrame() can be called to input all correct values

	private Rect _screen;
	private Texture _image;

	private float _displayTime;
	private float _timer = 0.0f;

	public string _nextLevelName;			//name of the scene that should be loaded when cutscene is done

	void Start () {
	
		//Create full screen Rect for images
		_screen = new Rect (0, 0, Screen.width, Screen.height);

		//Fills the framelist with all the frame children in Hierarchy
		_framelist = this.GetComponentsInChildren<FrameProperties>();
	}

	void Update ()
	{
		//If cutscene didnt start yet, start
		if (currentFrame < 0) 
			NextFrame ();

		else
		{
			if (_timer <= _displayTime)						//as long as displayTime hasn't been reached yet
				_timer += Time.deltaTime;					//timer goes up

			else											//displayTime has been reached
			{
				if (currentFrame != _framelist.Length-1) 	//as long as current frame is not last frame
					NextFrame();							//go to next frame
				
				else										//if it is the last frame
				{
					if (Application.CanStreamedLevelBeLoaded(_nextLevelName))	//if next level exists
						Application.LoadLevel(_nextLevelName);					//load the next level
					else
						Debug.Log("The inputted next level name does not exist. Check if it has been added to the build");
				}
			}
		}
	}

	void NextFrame()
	{
		++currentFrame;
		_displayTime = _framelist[currentFrame]._time;
		_image = _framelist[currentFrame]._image;
		_framelist [currentFrame].PlaySound ();
		_timer = 0.0f;
	}
		
	
	void OnGUI()
	{
		GUI.DrawTexture(_screen,_image);	//draws the current frame image
	}
}


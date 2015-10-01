using UnityEngine;
using System.Collections;

public class FrameProperties : MonoBehaviour {

	public Texture _image;
	public float _time;
	public AudioClip _sound;

	// Use this for initialization
	public void PlaySound () {

		if(_sound)
		{
			GameObject camera = GameObject.Find("Main Camera");
			AudioSource audio = camera.GetComponent<AudioSource>();
			audio.PlayOneShot(_sound);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

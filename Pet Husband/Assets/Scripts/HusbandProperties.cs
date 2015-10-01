using UnityEngine;
using System.Collections;

public class HusbandProperties : MonoBehaviour {

	public bool _unlocked = false;
	public string _name;
	public Sprite _image;
	public Sprite _infoImage;
	[TextArea(1,5)] public string _description;
	[TextArea(3,10)] public string _longDescription;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

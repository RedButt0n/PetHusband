using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HusbandInfoPopupBehavior : MonoBehaviour {

	// Use this for initialization
	public Canvas _MainWindow;

	void Start () {
		
		this.enabled = false;
	}

	public void OpenHusbandInfo()
	{
		this.enabled = true;
		_MainWindow.enabled = false;
	}

	public void CloseHusbandInfo()
	{
		_MainWindow.enabled = true;
		this.enabled = false;
	}
}

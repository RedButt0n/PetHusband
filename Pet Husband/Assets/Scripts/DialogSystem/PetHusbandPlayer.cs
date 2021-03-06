﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Inklewriter;
using Inklewriter.Player;
using Inklewriter.Unity;

public class PetHusbandPlayer : MonoBehaviour {

	public string storyName;
	public RectTransform chunkContainer;
	public PHChunk chunk;

	private PHChunk activeChunk;

	StoryPlayer player;	
	List<PHChunk> chunks = new List<PHChunk> ();

	// Use this for initialization
	void Start () {
		var resource = Resources.Load (storyName) as TextAsset;
		if (!resource) {
			Debug.LogWarning ("Inklewriter story could not be loaded: " + storyName);
			return;
		}
		
		string storyJson = resource.text;
		StoryModel model = StoryModel.Create (storyJson);
		
		this.player = new StoryPlayer (model, new UnityMarkupConverter ());

		activeChunk = chunk;
		var firstChunk = player.CreateChunkForStitch (player.InitialStitch);
		InstantiateChunk (firstChunk);
	}

	public void InstantiateChunk (PlayChunk c, Option chosenOption = null)
	{
		//Debug.Log ("Instantiating Chunck");
		activeChunk.gameObject.SetActive (false);
		var chunkObj = Instantiate (chunk.gameObject) as GameObject;
		chunkObj.SetActive (true);
		chunkObj.transform.SetParent (chunk.transform.parent,false);
		var chunkComponent = chunkObj.GetComponent<PHChunk> ();
		string prevImage = activeChunk.GetLastShownImage();
		chunkComponent.Set (c, chosenOption, this,prevImage);
		chunks.Add (chunkComponent);
		activeChunk = chunkComponent;
		
		Canvas.ForceUpdateCanvases ();
		
		//StartCoroutine (AnimateScroll (scroll.verticalNormalizedPosition, 1, 0.5f));
	}

	public void SelectOption (Option option)
	{
		//Debug.Log ("Select Option");
		if (option.LinkStitch != null) {
			if (chunks.Count > 0) {
				chunks[chunks.Count - 1].Disable ();
			}
			var playChunk = player.CreateChunkForStitch (option.LinkStitch);
			InstantiateChunk (playChunk, option);
		}
	}
	
	public void RewindToChunk (PHChunk chunk)
	{
		for (int i = chunks.Count - 1; i >= 0; i--)
		{
			if (chunks[i] == chunk) {
				return;
			}
			Destroy (chunks[i]);
		}
	}

	public void GoToNextParagraph()
	{
		//Debug.Log ("GoToNextParagraph");
		if (activeChunk != null) {
			activeChunk.GoToNextparagraph();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

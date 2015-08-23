using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//using SimpleJSON;
using LitJson;

public class EditorData
{
	public float 	textSize		{ get; set; }
	public string 	authorName		{ get; set; }
	public bool		libraryVisible	{ get; set; }
}

public class StitchData
{
	public string content;
}

public class StoryData
{
	public EditorData 				editorData 		{ get; set; }
	public string 					initial			{ get; set; }
	public bool 					allowCheckpoints{ get; set; }
	public List<StitchData> 		stitches		{ get; set; }

	public StoryData()
	{
		stitches = new List<StitchData>();
	}
}

public class Story
{
	public string   	url_key     { get; set; }
	public DateTime  	updated_at  { get; set; }
	public DateTime		created_at 	{ get; set; }
	public string		title 		{ get; set; }
	public StoryData	data		{ get; set; }
}

public class JSONParser : MonoBehaviour {

	public string fileName;
	private string m_InGameLog = "";
	private Vector2 m_Position = Vector2.zero;

	// Use this for initialization
	void Start () {
		Parse();
		Debug.Log("Test results:\n" + m_InGameLog);
	}
	void OnGUI()
	{
		m_Position = GUILayout.BeginScrollView(m_Position);
		GUILayout.Label(m_InGameLog);
		GUILayout.EndScrollView();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Parse()
	{
		//var rootNode = JSONNode.LoadFromFile(fileName);
		//var rootNode = JSONNode.Parse("{\"name\":\"test/.://=*-Victor:-* Schat, ik ben thuis! En ik ben bekaf... /=(moe)=/\", \"array\":[1,{\"data\":\"value\"}]}");
		//Print(rootNode.ToString());
		LoadJsonFromResourceFolder();
	}

	void Print(string text)
	{
		m_InGameLog += text + "\n";
	}

	void LoadJsonFromResourceFolder() {
		string resource_path = fileName;  // Note that ".json" part must be left out.
		TextAsset text_asset = (TextAsset)Resources.Load(resource_path, typeof(TextAsset));
		if( text_asset == null ) {
			Debug.Log("ERROR: Could not find file: Assets/Resources/" + resource_path);
			return;
		}
		string json_string = text_asset.ToString();
		Print(json_string);
		Story story = JsonMapper.ToObject<Story>(json_string);
		Debug.Log ("Story's title from resource file: " + story.title);
		Debug.Log ("Story's updateTime from resource file " + story.updated_at.ToString());
		StoryData storyData = story.data;
		Debug.Log("EditorData textSize: " + storyData.editorData.textSize);
		Debug.Log("EditorData authorName: " + storyData.editorData.authorName);
		Debug.Log("EditorData libraryVisible: " + storyData.editorData.libraryVisible);
		Debug.Log("Initial Stitch: " + storyData.initial);
		Debug.Log ("StoryData allows Checkpoints: " + storyData.allowCheckpoints);
		Debug.Log("Stiches size: " + storyData.stitches.Count);
		//foreach(
		//Person steve = JsonMapper.ToObject<Person>(json_string);        // Json string to object.
		//Debug.Log ("steve's name from resource file: " + steve.name);   // Outputs: "Steve Irwin"
    }
}

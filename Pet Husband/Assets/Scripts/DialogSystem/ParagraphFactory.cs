using UnityEngine;
using System.Collections.Generic;
using Inklewriter;
using Inklewriter.Player;
using Inklewriter.Unity;

[System.Serializable]
public struct ParagraphType
{
    public string typeName;
    public GameObject paragraph;
}

public class ParagraphFactory : MonoBehaviour {

    public List<ParagraphType> paragraphTypes = new List<ParagraphType>();

    private PetHusbandPlayer player;  
    private Dictionary<string, GameObject> paragraphTypesDict = new  Dictionary<string, GameObject>();
    private ParagraphParser parser;

    public PetHusbandPlayer Player
    {
        get { return player; }
        set { player = value; }
    }

	// Use this for initialization
	void Start () {
        var globalVarContainer = GameObject.Find("GlobalVarContainer").GetComponent<GlobalVarContainer>();
        parser = new ParagraphParser(globalVarContainer);

        //Build up paragraph dictionary for easy lookup later
        foreach (var paragraphType in paragraphTypes)
        {
            paragraphTypesDict.Add(paragraphType.typeName, paragraphType.paragraph);          
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject CreateParagraph(Paragraph paragraph, Option chosenOption, string prevChunkImage, out string currentImageName)
    {
        parser.ParagraphToParse = paragraph;
        string paragraphType = parser.ExtractParagraphType();

        GameObject paragraphTemplate;
        GameObject newParagraph = null;
        currentImageName = string.Empty;

        bool paragraphTypeFound = paragraphTypesDict.TryGetValue(paragraphType, out paragraphTemplate);
        if (!paragraphTypeFound)
        {
            Debug.LogWarning("The requested paragraphType could not be found! type: " + paragraphType + ". Loading default paragraph!");
            paragraphTypeFound = paragraphTypesDict.TryGetValue("", out paragraphTemplate);
        }

        if (paragraphTypeFound)
        {
            newParagraph = InstantiateNewParagraph(paragraphTemplate, chosenOption, prevChunkImage, out currentImageName);
        }
        else
        {
            Debug.LogError("No paragraphType could be loaded (not even the default one)! type: " + paragraphType);
        }

        return newParagraph;
    }

    private GameObject InstantiateNewParagraph(GameObject paragraphTemplate, Option chosenOption, string prevChunkImage, out string currentImageName)
    {
        var newParagraph = Instantiate(paragraphTemplate) as GameObject;
        newParagraph.SetActive(false);

        var paragraphComponent = newParagraph.GetComponent(typeof(IPHParagraph)) as IPHParagraph;
        paragraphComponent.Initialize(parser, chosenOption, prevChunkImage);

        currentImageName = paragraphComponent.GetImageName();

        return newParagraph;
    }

}

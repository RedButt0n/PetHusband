using UnityEngine;
using System.Collections;

public class GlobalStoryVarContainer : MonoBehaviour
{

    private string _PreviousBackgroundImage = string.Empty;
    private string _selectedMovie = string.Empty;
	
	public Color _playerColor;
	public Color _husbandColor;

    public string PreviousBackgroundImage
    {
        get { return _PreviousBackgroundImage; }
        set { _PreviousBackgroundImage = value; }
    } 

    public string SelectedMovie
    {
        get { return _selectedMovie; }
        set { _selectedMovie = value; }
    }

    // Use this for initialization
    void Start()
    {
        if (this.name == "GlobalStoryVarContainer") DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetImageOfMovieCurrentlyPlaying()
    {
        string currentlyPlaying = string.Empty;

        switch (_selectedMovie)
        {
            case "tv_option_Sullen_Rain":
                currentlyPlaying = "tv_film_Sullen_Rain";
                break;
            case "tv_option_3Cats_And_Wilson_The_Dog":
                currentlyPlaying = "tv_film_3Cats_And_Wilson_The_Dog";
                break;
            case "tv_option_CopRacer2":
            case "":
            default:
                currentlyPlaying = "tv_film_CopRacer2";
                break;      
        }

        return currentlyPlaying;
    }

    void ResetStory()
    {
        _PreviousBackgroundImage = string.Empty;
        _selectedMovie = string.Empty;
    }
}

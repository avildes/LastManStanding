using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int points = 0;

    void Awake()
    {
        /*
		Application.ExternalCall("OnUnityLoaded");

        FacebookManager.Instance.FacebookInitiated += OnFacebookInitiated;
        FacebookManager.Instance.FacebookDataRetrieved += OnFacebookDataRetrieved;

        FacebookManager.Instance.Init();
        */

        Instance = this;
        DontDestroyOnLoad(this);
    }

	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnFacebookInitiated()
    {
    }

    void OnFacebookDataRetrieved()
    {
    }

    public static void playGameMusic()
    {
       // MusicPlayer.playMainMusic();
    }

    public static void stopGameMusic()
    {
       // MusicPlayer.stopMainMusic();
    }
}
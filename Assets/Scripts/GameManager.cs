using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int points = 0;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

	void Start ()
    {
	}
	
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
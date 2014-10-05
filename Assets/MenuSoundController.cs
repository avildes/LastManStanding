using UnityEngine;
using System.Collections;

public class MenuSoundController : MonoBehaviour
{
    private string actualScene;
    private string lastScene;
    private AudioSource source;

    private static MenuSoundController _instance;

    public static MenuSoundController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MenuSoundController>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

	void Start ()
    {
        actualScene = "Opening";
        source = GetComponent<AudioSource>();
        source.Play();
	}
	
    public void ChangeScene(string scene)
    {
        actualScene = scene;
    }

	void Update ()
    {
        if(actualScene.Equals("Game"))
        {
            lastScene = "Game";
            source.Stop();
        }
        else
        {
            if(!source.isPlaying)
            {
                source.Play();
            }
            if(lastScene == "Game")
            {
                lastScene = actualScene;
            }
        }
        
	}
}

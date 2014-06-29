using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    void Awake()
    {
		Application.ExternalCall("OnUnityLoaded");

        FacebookManager.Instance.FacebookInitiated += OnFacebookInitiated;
        FacebookManager.Instance.FacebookDataRetrieved += OnFacebookDataRetrieved;

        FacebookManager.Instance.Init();

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
}

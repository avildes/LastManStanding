using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameManager Instance;

    void Awake()
    {
        if(this.Instance == null)
        {
            //Error
        }
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

    void TryFacebookLogin()
    {
        FacebookManager.TryFacebookLogin();
    }

    void FacebookInit()
    {
        FacebookManager.InitFacebook();
    }
}

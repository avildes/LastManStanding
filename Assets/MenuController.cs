using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{

    public GameObject menuContainer;
    public GameObject credits;
    public GameObject game;
    public GameObject tutorial;

    private AudioSource source;

    public AudioClip menu_enter;
    public AudioClip menu_select;

    private bool freezeControls;

    private enum MENU
    {
        PLAY,
        CREDITS,
        TUTORIAL
    }

    private MENU selecao;

    private string gameLevel = "Game";
    private string creditsLevel = "Credits";
    private string tutorialLevel = "Tutorial";

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    IEnumerator NewScene(string level)
    {
        source.PlayOneShot(menu_enter, 1);
        freezeControls = true;
        menuContainer.GetComponent<Animator>().SetTrigger("ShutDown");
        menuContainer.GetComponent<Animator>().SetTrigger("ShutDown");
        yield return new WaitForSeconds(1.2f);
        Application.LoadLevel(level);


    }

    void SetSeletor(MENU menu)
    {
        switch (menu)
        {
            case MENU.PLAY:
                selecao = MENU.PLAY;
                break;
            case MENU.CREDITS:
                
                selecao = MENU.CREDITS;
                break;
        }
    }


    IEnumerator LoadLevel(string level)
    {
        if (!freezeControls)
        {
            if (level.Equals("Game")) SetSeletor(MENU.PLAY);
            else if (level.Equals("Credits")) SetSeletor(MENU.CREDITS);

            source.PlayOneShot(menu_enter, 1);
            freezeControls = true;
            //menuAnimator.SetTrigger("ShutDown");
            yield return new WaitForSeconds(1.2f);
            Application.LoadLevel(level);
        }
    }
}

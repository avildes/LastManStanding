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

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        selecao = MENU.PLAY;
        StartCoroutine(WaitAndSet(selecao));
        
        //SetSeletor(selecao);
    }

    IEnumerator WaitAndSet(MENU selecao)
    {
        yield return new WaitForSeconds(.5f);
        SetSeletor(selecao);
    }

    void Update()
    {
        if (!freezeControls)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                if(selecao != MENU.CREDITS)
                {
                    source.PlayOneShot(menu_select, .7f);
                    SetSeletor(MENU.CREDITS);
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if (selecao != MENU.TUTORIAL)
                {
                    source.PlayOneShot(menu_select, .7f);
                    SetSeletor(MENU.TUTORIAL);   
                }
            }else if (Input.GetAxis("Vertical") > 0)
            {
                if (selecao != MENU.PLAY)
                {
                    SetSeletor(MENU.PLAY);
                    source.PlayOneShot(menu_select, .7f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetAxis("Enter") > 0)
            {
                switch (selecao)
                {
                    case MENU.PLAY:
                        LoadLevel(gameLevel);
                        break;
                    case MENU.CREDITS:
                        LoadLevel(creditsLevel);
                        break;
                    case MENU.TUTORIAL:
                        LoadLevel(tutorialLevel);
                        break;
                }
            }
        }
    }

    IEnumerator NewScene(string level)
    {
        source.PlayOneShot(menu_enter, 1);
        freezeControls = true;
        //menuContainer.GetComponent<Animator>().SetTrigger("ShutDown");
        menuContainer.GetComponent<Animator>().SetTrigger("ShutDown");
        yield return new WaitForSeconds(1.2f);

        if(level.Equals("Game"))
        {
            GameObject.FindGameObjectWithTag("StaticManager").SendMessage("ChangeScene", "Game", SendMessageOptions.DontRequireReceiver);
            //MenuSoundController msc = GameObject.FindGameObjectWithTag("StaticManager").GetComponent<MenuSoundController>();
            //msc.nstance.
        }

        Application.LoadLevel(level);
    }

    void SetSeletor(MENU menu)
    {
        credits.SetActive(false);
        tutorial.SetActive(false);
        game.SetActive(false);
        switch (menu)
        {
            case MENU.PLAY:
                game.SetActive(true);
                selecao = MENU.PLAY;
                break;
            case MENU.CREDITS:
                credits.SetActive(true);
                selecao = MENU.CREDITS;
                break;
            case MENU.TUTORIAL:
                tutorial.SetActive(true);
                selecao = MENU.TUTORIAL;
                break;
        }
    }


    void LoadLevel(string level)
    {
        if (!freezeControls)
        {
            if (level.Equals(gameLevel))
            {
                game.SetActive(true);
                game.GetComponent<Animator>().SetTrigger("pressed");
            }
            else if (level.Equals(creditsLevel))
            {
                credits.SetActive(true);
                credits.GetComponent<Animator>().SetTrigger("pressed");
            }
            else if (level.Equals(tutorialLevel))
            {
                tutorial.SetActive(true);
                tutorial.GetComponent<Animator>().SetTrigger("pressed");
            }

            //source.PlayOneShot(menu_enter, 1);
            freezeControls = true;

            StartCoroutine(NewScene(level));
            //menuContainer.GetComponent<Animator>().SetTrigger("ShutDown");
            //yield return new WaitForSeconds(1.2f);
            //Application.LoadLevel(level);
        }
    }
}

using UnityEngine;
using System.Collections;

public class MenuSeletor : MonoBehaviour
{

    public AudioClip menu_enter;
    public AudioClip menu_select;

    private AudioSource source;

    private bool freezeControls;

    public GameObject menuContainer;
    private Animator menuAnimator;

    private enum MENU
    {
        PLAY,
        SCORE
    }

    private MENU selecao;

    private GameObject play_btn;
    private GameObject score_btn;

    private string gameLevel = "Game";
    private string scoreLevel = "Score";


    void Start()
    {
        menuAnimator = menuContainer.GetComponent<Animator>();
        source = gameObject.GetComponent<AudioSource>();

        play_btn = GameObject.FindGameObjectWithTag("Play_Btn");
        score_btn = GameObject.FindGameObjectWithTag("Score_Btn");

        gameObject.transform.position =
            new Vector3(
                gameObject.transform.position.x,
                play_btn.transform.position.y,
                gameObject.transform.position.z
                );

        selecao = MENU.PLAY;
    }


    void Update()
    {
        if (!freezeControls)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                if (selecao != MENU.PLAY)
                {
                    gameObject.transform.position =
                        new Vector3(
                            gameObject.transform.position.x,
                            play_btn.transform.position.y,
                            gameObject.transform.position.z
                            );
                    selecao = MENU.PLAY;
                    source.PlayOneShot(menu_select, 1);
                }

            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                if (selecao != MENU.SCORE)
                {
                    gameObject.transform.position =
                        new Vector3(
                            gameObject.transform.position.x,
                            score_btn.transform.position.y,
                            gameObject.transform.position.z
                            );
                    selecao = MENU.SCORE;
                    source.PlayOneShot(menu_select, 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetAxis("Enter") > 0)
            {
                switch (selecao)
                {
                    case MENU.PLAY:
                        StartCoroutine(Load(gameLevel));
                        break;
                    case MENU.SCORE:
                        StartCoroutine(Load(scoreLevel));
                        break;
                }
            }
        }
    }

    IEnumerator Load(string level)
    {
        source.PlayOneShot(menu_enter, 1);
        freezeControls = true;
        menuAnimator.SetTrigger("ShutDown");
        yield return new WaitForSeconds(1);
        Application.LoadLevel(level);
    }
}

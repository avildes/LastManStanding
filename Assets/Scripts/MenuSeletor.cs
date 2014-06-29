using UnityEngine;
using System.Collections;

public class MenuSeletor : MonoBehaviour
{

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
        if (Input.GetKey("up"))
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
            }

        }
        else if (Input.GetKey("down"))
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
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            switch (selecao)
            {
                case MENU.PLAY:
                    Load(gameLevel);
                    break;
                case MENU.SCORE:
                    Load(scoreLevel);
                    break;
            }
        }
    }

    void Load(string level)
    {
        Application.LoadLevel(level);
    }
}

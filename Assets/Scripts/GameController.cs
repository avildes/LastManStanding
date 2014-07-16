using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject gameElements;
    private Animator gameElementsAnimator;

    public GameObject scoreObject;
    private ScoreScript scoreClass;
    private int _points = 0;

    private AudioSource source;
    public AudioSource musicSource;

    public AudioClip start_game;
    public AudioClip start_game2;
    public AudioClip end_game;

    public GameObject finalScoreObject;
    public GameObject finalScoreValueObject;
    private GUIText finalScoreValue;

    //-----EVENT MANAGER-----
    public delegate void GameHandler(bool ativo);
    public static event GameHandler onSetAtivo;
    //-----------------------
	
    void Start ()
    {
        PlayerScript.onPlayerDeath += onPlayerDeath;

        source = gameObject.GetComponent<AudioSource>();
        gameElementsAnimator = gameElements.GetComponent < Animator >();
        StartCoroutine(StartGame());
	}

	void Update()
	{

	}
	
	IEnumerator StartGame()
    {
        //gameElementsAnimator.SetTrigger("Start");
        //play start sound
        source.PlayOneShot(start_game2);

        yield return new WaitForSeconds(.75f);
        source.PlayOneShot(start_game);
        onSetAtivo(true);
        yield return new WaitForSeconds(1.3f);
        musicSource.Play();
    }

    public void SetPoints(int points)
    {
        this._points = points;
    }

    void onPlayerDeath()
    {
		PlayerScript.onPlayerDeath -= onPlayerDeath;
        musicSource.Stop();

        source.PlayOneShot(end_game);
        gameElementsAnimator.SetTrigger("End");
		
        onSetAtivo(false);
        StartCoroutine(ShowFinalScore());
    }

    IEnumerator ShowFinalScore()
    {
        yield return new WaitForSeconds(.7f);

        finalScoreObject.SetActive(true);
        finalScoreValue = finalScoreValueObject.GetComponent<GUIText>();
        finalScoreValue.text = this._points+"";
    }
}

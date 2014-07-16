using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject gameElements;
    private Animator gameElementsAnimator;

    public GameObject scoreObject;
    private ScoreScript scoreClass;
    private int _points = 0;
	private string _time;

    private AudioSource source;
    public AudioSource musicSource;

    public AudioClip start_game;
    public AudioClip start_game2;
    public AudioClip end_game;

    public GameObject finalScoreObject;
    public GameObject finalScoreValueObject;
    private GUIText finalScoreValue;

	public GameObject finalTimeValueObject;
	private GUIText finalTimeValue;

	public GameObject bestScoreValueObject;
	private GUIText bestScoreValue;

    //-----EVENT MANAGER-----
    public delegate void GameHandler(bool ativo);
    public static event GameHandler onSetAtivo;

	public delegate void LoadSceneHandler();
	public static event LoadSceneHandler onLoadNewScene;
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
		if (Input.GetKeyDown(KeyCode.R) || Input.GetKey("joystick button 1"))
		{
			Load("Game");
		}
		
		if (Input.GetKey("joystick button 6") || Input.GetKeyDown(KeyCode.Space))
		{
			Load("Menu");
		}
	}

	void Load(string level)
	{
		PlayerScript.onPlayerDeath -= onPlayerDeath;

		onLoadNewScene();
		Application.LoadLevel(level);
	}

	IEnumerator StartGame()
    {
        //gameElementsAnimator.SetTrigger("Start");
        //play start sound
        source.PlayOneShot(start_game2);

        yield return new WaitForSeconds(.75f);
        source.PlayOneShot(start_game);
        onSetAtivo(true);
        yield return new WaitForSeconds(1.6f);
        musicSource.Play();
    }

    public void SetPoints(int points)
    {
        this._points = points;
    }

	public void SetTime(string time)
	{
		this._time = time;
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

		finalTimeValue = finalTimeValueObject.GetComponent<GUIText>();
		finalTimeValue.text = this._time;

		int highScore = PersistenceHelper.ReadInteger(PersistenceHelper.HIGHSCORE_KEY);

		if (_points > highScore)
		{
			highScore = _points;
			PersistenceHelper.PersistInteger(PersistenceHelper.HIGHSCORE_KEY, highScore);
		}

		bestScoreValue = bestScoreValueObject.GetComponent<GUIText>();
		bestScoreValue.text = highScore + "";
    }
}

using UnityEngine;
using System.Collections;

public class NGUIGameController : MonoBehaviour
{
    public GameObject gameElements;
    private Animator gameElementsAnimator;

    public GameObject scoreObject;
    private ScoreScript scoreClass;
    private int _points = 0;
    private float _time;

    private AudioSource source;
    public AudioSource musicSource;

    public AudioClip start_game;
    public AudioClip start_game2;
    public AudioClip end_game;

    public GameObject scoreScreen;

    public GameObject scoreValues;

    public GameObject finalTimeSecondsValueObject;
    public GameObject finalTimeMillisValueObject;

    public GameObject bestTimeSecondsValueObject;
    public GameObject bestTimeMillisValueObject;
    
    /*
    //-----EVENT MANAGER-----
    public delegate void GameHandler(bool ativo);
    public static event GameHandler onSetAtivo;

    public delegate void LoadSceneHandler();
    public static event LoadSceneHandler onLoadNewScene;
    //-----------------------
    */
    void Start()
    {
        EventManager.onPlayerDeath += onPlayerDeath;

        source = gameObject.GetComponent<AudioSource>();
        gameElementsAnimator = gameElements.GetComponent<Animator>();
        StartCoroutine(StartGame());
    }

    void Load(string level)
    {
        EventManager.onPlayerDeath -= onPlayerDeath;

        //onLoadNewScene();
        EventManager.Instance.onLoadNewSceneEvent();
        Application.LoadLevel(level);
    }

    IEnumerator StartGame()
    {
        //gameElementsAnimator.SetTrigger("Start");
        //play start sound
        source.PlayOneShot(start_game2);

        yield return new WaitForSeconds(.75f);
        source.PlayOneShot(start_game);
        //onSetAtivo(true);
        EventManager.Instance.onSetAtivoEvent(true);
        yield return new WaitForSeconds(1.6f);
        musicSource.Play();
    }

    public void SetPoints(int points)
    {
        this._points = points;
    }

    public void SetTime(float time)
    {
        this._time = time;
    }

    void onPlayerDeath()
    {
        EventManager.onPlayerDeath -= onPlayerDeath;
        musicSource.Stop();

        source.PlayOneShot(end_game);
        gameElementsAnimator.SetTrigger("End");

        //onSetAtivo(false);
        EventManager.Instance.onSetAtivoEvent(false);
        StartCoroutine(ShowFinalScore());
    }

    IEnumerator ShowFinalScore()
    {
        yield return new WaitForSeconds(.7f);

        scoreScreen.SetActive(true);
        scoreValues.SetActive(true);

        Debug.Log("time: " + _time);

        finalTimeSecondsValueObject.GetComponent<UILabel>().text = string.Format("{0}", (int)_time);
        Debug.Log("secs: " + string.Format("{0}", (int)_time));
        Debug.Log("secs.text: " + finalTimeSecondsValueObject.GetComponent<UILabel>().text);
        finalTimeMillisValueObject.GetComponent<UILabel>().text = string.Format("{0}{1}", ((int)(_time * 10)) % 10, ((int)(_time * 100)) % 10);
        Debug.Log("millis: " + string.Format("{0}{1}", ((int)(_time * 10)) % 10, ((int)(_time * 100)) % 10));
        Debug.Log("millis.text: " + finalTimeMillisValueObject.GetComponent<UILabel>().text);

        float highScore = (float)PersistenceHelper.ReadFloat(PersistenceHelper.HIGHTIME_KEY);

        if (_time > highScore)
        {
            highScore = _time;
            PersistenceHelper.PersistFloat(PersistenceHelper.HIGHTIME_KEY, highScore);
        }

        bestTimeSecondsValueObject.GetComponent<UILabel>().text = string.Format("{0}", (int)highScore);
        bestTimeMillisValueObject.GetComponent<UILabel>().text = string.Format("{0}{1}", ((int)(highScore * 10)) % 10, ((int)(highScore * 100)) % 10);
    }

    public void SetHiScore(float value)
    {
        PersistenceHelper.PersistFloat(PersistenceHelper.HIGHTIME_KEY, value);
    }

    public float GetHiScore()
    {
        return PersistenceHelper.ReadFloat(PersistenceHelper.HIGHTIME_KEY);
    }
}

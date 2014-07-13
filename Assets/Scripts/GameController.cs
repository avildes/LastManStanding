using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject gameElements;
    private Animator gameElementsAnimator;

    private int _points;

    private AudioSource source;
    public AudioSource musicSource;

    public AudioClip start_game;
    public AudioClip start_game2;

	// Use this for initialization
	void Start ()
    {
        source = gameObject.GetComponent<AudioSource>();
        gameElementsAnimator = gameElements.GetComponent < Animator >();
        StartCoroutine(StartGame());
	}
	
	// Update is called once per frame
	void Update ()
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

    public delegate void GameHandler(bool ativo);
    public static event GameHandler onSetAtivo;
}

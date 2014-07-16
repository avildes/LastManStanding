using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour
{
    public AudioClip buttonSound;

    private AudioSource audioSource;

    private bool playOnce = true;

    void Start()
    {
		audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update ()
    {
        if (Input.anyKey && playOnce)
        {
            playOnce = false;
			audioSource.PlayOneShot(buttonSound, 1);
            StartCoroutine(LoadMenu());
        }
	}

    IEnumerator LoadMenu()
    {
        gameObject.GetComponent<Animator>().SetTrigger("CreditsExit");

        yield return new WaitForSeconds(.75f);

        Application.LoadLevel("Menu");
    }
}

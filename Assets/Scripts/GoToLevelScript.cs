using UnityEngine;
using System.Collections;

public class GoToLevelScript : MonoBehaviour
{
	public string level;

    void Start()
    {
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(3f);

        OnTouchDown();
    }

	void OnTouchDown()
	{
        gameObject.GetComponent<Animator>().SetTrigger("ShutDown");


        StartCoroutine(Load(level));
		
	}

    IEnumerator Load(string level)
    {
        yield return new WaitForSeconds(1f);
        Application.LoadLevel(level);
    }
}

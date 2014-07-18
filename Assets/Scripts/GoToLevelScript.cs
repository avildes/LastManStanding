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
        yield return new WaitForSeconds(5f);

        OnTouchDown();
    }

	void OnTouchDown()
	{
        gameObject.GetComponent<Animator>().SetTrigger("ShutDown");

		Application.LoadLevel(level);
	}
}

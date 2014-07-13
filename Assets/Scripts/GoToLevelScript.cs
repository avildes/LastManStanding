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
        yield return new WaitForSeconds(.1f);

        OnTouchDown();
    }

	void OnTouchDown()
	{
		Application.LoadLevel(level);
	}
}

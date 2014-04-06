using UnityEngine;
using System.Collections;

public class OpeningScreenScript : MonoBehaviour
{

	public int levelNumberInBuild;

	void OnTouchDown()
	{
		Application.LoadLevel(levelNumberInBuild);
	}
}

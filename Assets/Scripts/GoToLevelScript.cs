using UnityEngine;
using System.Collections;

public class GoToLevelScript : MonoBehaviour
{

	public int levelNumberInBuild;

	void OnTouchDown()
	{
		Application.LoadLevel(levelNumberInBuild);
	}
}

using UnityEngine;
using System.Collections;

public class GoToLevelScript : MonoBehaviour
{

	public string level;

	void OnTouchDown()
	{
		Application.LoadLevel(level);
	}
}

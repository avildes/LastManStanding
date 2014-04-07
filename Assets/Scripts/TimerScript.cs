using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour
{
	private float _totalTime;

	// Use this for initialization
	void Start ()
	{
		_totalTime = 0;
		guiText.text = "TIME 0:00";
	}
	
	// Update is called once per frame
	void Update ()
	{
		_totalTime += Time.deltaTime;
		guiText.text = string.Format("TIME {0}:{1}{2}", (int) _totalTime, ((int)(_totalTime * 10)) % 10, ((int)(_totalTime * 100)) % 10);
	}
}

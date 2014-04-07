using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour
{
	public static TimerScript Instance;

	private float _totalTime;

	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SoundEffectsHelper!");
		}
		Instance = this;
	}

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

	public float GetTotalTime()
	{
		return _totalTime;
	}
}

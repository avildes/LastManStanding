using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

	private int _totalScore;
    private float _totalTime;

    public bool _ativo;


	// Use this for initialization
	void Start ()
	{
        PlusScoreScript.onPointsChange += onPointsChange;
        GameController.onSetAtivo += onSetAtivo;

		_totalTime = 0;
		guiText.text = "0000";
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(_ativo)
        {
		    _totalTime += Time.deltaTime;

            _totalScore += (int) _totalTime;

		    guiText.text = _totalScore+"";
        }
	}

	public float GetTotalTime()
	{
		return _totalTime;
	}

    void onSetAtivo(bool ativo)
    {
        _ativo = ativo;
    }

    public int GetFinalScore()
    {
        return this._totalScore;
    }

    void onPointsChange(int points)
    {
        this._totalScore += points;
    }
}

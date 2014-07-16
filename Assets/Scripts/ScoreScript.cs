using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {

	private int _totalScore = 0;
    private float timeOffset = .1f;
    private float _totalTime;

    private GameController controller;

    public bool _ativo;

	// Use this for initialization
	void Start ()
	{
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

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

            if (_totalTime > timeOffset)
            {
                _totalScore += 2;
                _totalTime = 0;
            }

            controller.SetPoints(_totalScore);

        }
       
		    guiText.text = _totalScore+"";
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

    //TODO e se score estourar o int?
    public int GetTotalScore()
    {
        return _totalScore;
    }
}

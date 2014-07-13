using UnityEngine;
using System.Collections;

public class PlusScoreScript : MonoBehaviour
{
    private int plusPoints = 0;

    public int pointsPerMob = 100;

	void Start ()
    {
        FollowPlayer.onMobDie += onMobDie;
	}

    void onMobDie()
    {
        plusPoints += pointsPerMob;

        onPointsChange(pointsPerMob);    
	}

    IEnumerator ShowPlusPoints()
    {
        guiText.text = "+" + plusPoints;

        yield return new WaitForSeconds(2);

        guiText.text = "";
    }

    public delegate void PointsHandler(int points);
    public static event PointsHandler onPointsChange;
}

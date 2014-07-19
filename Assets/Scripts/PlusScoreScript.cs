using UnityEngine;
using System.Collections;

public class PlusScoreScript : MonoBehaviour
{
    private int plusPoints = 0;

    public int pointsPerMob = 100;

    private float showTime;
    private float maxShowTime = 2f;
    
    /*
    //-----EVENT MANAGER-----
    public delegate void PointsHandler(int points);
    public static event PointsHandler onPointsChange;
    //-----------------------
	*/

    void Start ()
    {
        EventManager.onMobDie += onMobDie;
	}

    void onMobDie()
    {
        plusPoints += pointsPerMob;
        EventManager.Instance.onPointsChangeEvent(pointsPerMob);
        showTime = maxShowTime;
        
        //StartCoroutine(ShowPlusPoints());
	}

    void Update()
    {
        if(showTime > 0)
        {
            showTime -= Time.deltaTime;

            //guiText.text = "+" + plusPoints;
        }
        else
        {
            plusPoints = 0;
            //guiText.text = "";
        }
    }
    /*
    IEnumerator ShowPlusPoints()
    {
        Debug.Log("plus: "+ plusPoints);
        guiText.text = "+" + plusPoints;

        yield return new WaitForSeconds(2);

        plusPoints = 0;
        guiText.text = "";
    }
    */
}

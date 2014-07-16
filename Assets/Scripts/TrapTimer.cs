using UnityEngine;
using System.Collections;

public class TrapTimer : MonoBehaviour
{
    public float TrapLifeTime;

	// Use this for initialization
	void Start ()
    {
        GameController.onSetAtivo += onSetAtivo;

        StartCoroutine(Timer());
	}

    void onSetAtivo(bool ativo)
    {
        /*
        if (!ativo)
        {
            Destroy(gameObject);
        }*/
    }
	
	IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}

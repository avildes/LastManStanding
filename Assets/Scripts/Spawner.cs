using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject mob;
    public GameObject trap;

    public float trapSpawnTime = 12f;
    public float mobSpawnTime = 2;

	public float boundsX1 = 19f;
	public float boundsX2 = 39f;
	public float boundsY1 = 9f;
	public float boundsY2 = -7f;

    private bool _ativo = false;
    private bool oneTime = true;

	private GameObject player;

    private float time;

    void onSetAtivo(bool ativo)
    {
        _ativo = ativo;
    }

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");

        EventManager.onSetAtivo += onSetAtivo;
		//StartCoroutine(SpawnMob());
        //StartCoroutine(SpawnTrap());
	}
	
    void Destroy()
    {
        EventManager.onSetAtivo -= onSetAtivo;
    }

	// Update is called once per frame
	void Update ()
	{
        if(_ativo && oneTime)
        {
            StartCoroutine(SpawnMob(mobSpawnTime));
            StartCoroutine(SpawnTrap());
            oneTime = false;
        }

        time += Time.deltaTime;
	}

	IEnumerator SpawnMob(float spawnTime)
	{
        yield return new WaitForSeconds(spawnTime);

		if (_ativo)
		{
			Instantiate(mob, GetRandomBoundsPosition(), Quaternion.identity);


            if (spawnTime > (mobSpawnTime / 4)) spawnTime -= .05f;
            StartCoroutine(SpawnMob(spawnTime));

		}
	}

    IEnumerator SpawnTrap()
    {
		yield return new WaitForSeconds(trapSpawnTime);
        
		if(_ativo)
		{
			Instantiate(trap, GetRandomPosition(), Quaternion.identity);

            int times = calcTimes();

            for (int i = 0; i < times; i++)
            {
        	    StartCoroutine(SpawnTrap());
            }

		}
    }

    int calcTimes()
    {
        int times = 1;

        if (time > 20) times = 2;
        if (time > 40) times = 3;
        if (time > 60) times = 4;

        return times;
    }

    Vector3 GetRandomPosition()
    {
        Vector3 vector = new Vector3(0, 0, 0);
        
        vector.x = Random.Range(boundsX2, boundsX1);
        vector.y = Random.Range(boundsY2, boundsY1);
            
        return vector;
    }

	Vector3 GetRandomBoundsPosition()
	{
        float orientation = Random.Range (-1.0F, 1.0F);

        Vector3 vector = new Vector3(0, 0, 0);

		do
		{
			if (orientation < 0)  //Vertical - left, right
	        {
	            vector.y = Random.Range(boundsY2, boundsY1);

	            orientation = Random.Range (-1.0F, 1.0F); // Check if it is X1 or X2 (leftOrRight)

	            if(orientation < 0) // X1 left
	            {
	                vector.x = boundsX1;
	            }
	            else // X2 right
	            {
	                vector.x = boundsX2;
	            }
	        }
	        else  // Horizontal - top, bot
	        {
	            vector.x = Random.Range(boundsX2, boundsX1);

	            orientation = Random.Range (-1.0F, 1.0F); // Check if it is Y1 or Y2 (topOrBot)
	            
	            if(orientation < 0) // Y1 Top
	            {
	                vector.y = boundsY1;
	            }
	            else // Y2 Bot
	            {
	                vector.y = boundsY2;
	            }
	        }
		} while (Vector3.Distance(vector, player.transform.position) < 2f);
        return vector;
	}
}

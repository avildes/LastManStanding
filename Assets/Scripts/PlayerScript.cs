using UnityEngine;
using System.Collections;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// Velocidade do player
	/// </summary>
	public int speed = 8;
	
    /// <summary>
    /// Texto usado para mostrar o tempo final
    /// </summary>
    public GUIText endTimeText;

	/// <summary>
    /// Vetor que representa o movimento do player
    /// </summary>
	private Vector2 movement;

    /// <summary>
    /// Vetor de direçao do movimento do jogador no momento do pulo
    /// </summary>
    private Vector2 jumpMomentVector;

    /// <summary>
    /// Flag que indica se o player esta pulando
    /// </summary>
    private bool isJumping;

    /// <summary>
    /// Tempo total de jogo
    /// </summary>
    private float totalTime;

	private bool alive = true;
	
	void Update()
	{
		if(alive)
		{
			// Recupera o input do jogador
			float inputX = Input.GetAxis("Horizontal");
        	float inputY = Input.GetAxis("Vertical");
        
        	// Seta o vetor de movimento de acordo com o input
			movement = (new Vector2(inputX, inputY)).normalized * speed;


		/*
        // FIXME Quando o player esta parado antes de pular, e possivel mudar de direçao
        // Se o player esta pulando, ele pode mover-se apenas na mesma direçao com que iniciou o pulo
        if (isJumping && jumpMomentVector.magnitude != 0) && !_IsInSameDirection(movement, jumpMomentVector))
        {
            movement = new Vector2(0, 0);
        }*/

        	if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        	{
	            jumpMomentVector = new Vector2(inputX, inputY);
            	StartCoroutine(JumpCoroutine ());
        	}
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(2);
		}
	}
	
	void FixedUpdate()
	{
		rigidbody2D.velocity = movement;
	}

	void OnCollisionEnter2D(Collision2D hit)
	{
		if (hit.gameObject.tag == "Enemy")
		{
            OnPlayerDeath();
		}
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Trap")
        {
            if(!isJumping)
            {
                OnPlayerDeath();
            }
        }
    }

    /// <summary>
    /// Metodo que realiza as atividades relacionadas a morte do player
    /// </summary>
    private void OnPlayerDeath()
    {
		if(alive)
		{
			totalTime = TimerScript.Instance.GetTotalTime();
			endTimeText.text = string.Format("TIME {0:f2}", totalTime);
			//Destroy(this);
			alive = false;
		}
    }

    /// <summary>
    /// Verifica se dois vetores estao na mesma direçao
    /// </summary>
    private bool _IsInSameDirection(Vector2 vector1, Vector2 vector2)
    {
        Vector2 vector1Normalized = vector1.normalized;
        Vector2 vector2Normalized = vector2.normalized;

        // Os vetores estao no mesmo sentido ou em sentidos opostos
        return (vector1Normalized.x == vector2Normalized.x && vector1Normalized.y == vector2Normalized.y) ||
            (vector1Normalized.x == -vector2Normalized.x && vector1Normalized.y == -vector2Normalized.y);
    }

    /// <summary>
    /// Corotina de pulo
    /// </summary>
    private IEnumerator JumpCoroutine()
    {
        isJumping = true;
        renderer.material.color = new Color(0, 255, 255);
        
        yield return new WaitForSeconds(0.5f);
        
        isJumping = false;
        renderer.material.color = new Color(255, 0, 0);
    }
}
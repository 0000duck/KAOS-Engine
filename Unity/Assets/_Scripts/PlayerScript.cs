using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed;
	private int score;
	public GUIText scoreText, winText;

	void Start()
	{
		score = 0;
		scoreText.text = "Score: " + score.ToString();
		winText.text = "";
	}
    
	void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementForce = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidbody.AddForce(movementForce * speed * Time.deltaTime);

		if (score == 12)
		{
			winText.text = "You Win";
		}
    }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Pickup"){

			other.gameObject.SetActive(false);
			score++;
			scoreText.text = "Score :" + score.ToString();
		}
	}
}

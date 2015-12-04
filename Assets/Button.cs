using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject target;

	private SpriteRenderer spriteRenderer;

	public Sprite nonPressedButton;
	public Sprite pressedButton;

	private string quemSubiu = "";

	void Awake() {

		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	void OnTriggerEnter2D (Collider2D col) {

		if ((col.gameObject.tag == "Player" || col.gameObject.tag == "Box" || col.gameObject.tag == "Player2") && quemSubiu == "") {

			target.SetActive(false);
			spriteRenderer.sprite = pressedButton; 

			quemSubiu = col.gameObject.tag;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.tag == quemSubiu) {
			target.SetActive (true);
			spriteRenderer.sprite = nonPressedButton;
			quemSubiu = "";
		}
	}
}

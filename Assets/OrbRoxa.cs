using UnityEngine;
using System.Collections;

public class OrbRoxa : MonoBehaviour {

	bool segurando = false;
	string quemTaSegurando = "";

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Player2" || col.gameObject.tag == "Player") {

			this.transform.parent =  col.gameObject.transform;
			segurando = true;
			quemTaSegurando = col.tag;
			
		}
	}

	void Update () {

		if (segurando) {

			if (quemTaSegurando == "Player" && Input.GetKeyDown (Controller.dropBob)) {

				this.transform.parent = null;
			}

			if (quemTaSegurando == "Player2" && Input.GetKeyDown (Controller.dropJoe)) {
				
				this.transform.parent = null;
			}
		}
	}
}


//VAI SE FUDEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
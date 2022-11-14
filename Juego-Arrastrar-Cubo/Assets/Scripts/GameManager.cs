using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager: MonoBehaviour {

    int floating = 0;

    void Update () {
        // Si el número de veces clicado en pantalla es mayor o igual que uno Y se ha dejado de pulsar el clic izquierdo, o si se está pulsando el botón:
        if (((Input.touchCount >= 1 && Input.GetTouch (0).phase == TouchPhase.Ended) || (Input.GetMouseButtonDown (0)))) { // (0 = botón izq., 1 = botón cent., 2= botón der.)
            Vector2 mousePos = Input.mousePosition;

            if (Application.platform == RuntimePlatform.Android) {
                mousePos = Input.GetTouch (0).position;
            }
            Ray rayo = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            floating = (floating - 1) * -1; 

            if (Physics.Raycast (rayo, out hitInfo)) {

                if (hitInfo.collider.tag.Equals ("Player")) {

                    if (floating >= 1) {
                        GameObject cubo = hitInfo.collider.gameObject;
                        Rigidbody rigidbodyCubo = cubo.GetComponent<Rigidbody> ();
                        rigidbodyCubo.AddForce (new Vector3 (cubo.transform.position.x - mousePos.x, 0, cubo.transform.position.z - mousePos.y), ForceMode.Impulse);
                        //cubo.transform.Translate (new Vector3 (mousePos.x / Screen.width, 0f, mousePos.y / Screen.height));
                        cubo.SetActive (false);
                        Debug.Log ("Posición cubo: " + cubo.transform.position);
                        cubo.SetActive (true);
                    }
                }
            }
        }

        Debug.Log ("floating: " + floating);
    }    
}


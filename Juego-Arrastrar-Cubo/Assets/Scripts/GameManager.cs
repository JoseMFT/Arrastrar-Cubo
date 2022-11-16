using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager: MonoBehaviour {

    bool floating = false; 

    void Update () {
        // Si el número de veces clicado en pantalla es mayor o igual que uno Y se ha dejado de pulsar el clic izquierdo, o si se está pulsando el botón:
        //if (((Input.touchCount >= 1 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0)))) { // (0 = botón izq., 1 = botón cent., 2= botón der.)
        if (Input.GetMouseButton (0)) {
            clicking ();
        }

        void clicking () {
            Ray rayo = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast (rayo, out hitInfo)) {

                if (hitInfo.collider.tag.Equals ("Player")) {
                    GameObject cubo = hitInfo.collider.gameObject;
                    var posCubo = hitInfo.collider.GetComponent<Transform> ();
                    cubo.SetActive (false);
                    floating = true;

                    if (hitInfo.collider.tag.Equals ("Player") || hitInfo.collider.tag.Equals ("Escenario")) {

                        if (cubo.activeSelf == false) {
                            Ray segundoRayo = Camera.main.ScreenPointToRay (Input.mousePosition);

                            if (Physics.Raycast (segundoRayo, out hitInfo)) {

                                if (floating == true) {
                                    cubo.SetActive (true);
                                    posCubo.position = hitInfo.point + new Vector3 (0f, 1f, -1f);
                                }
                            }
                        } 
                    }                    
                }
            }
        }
    }    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager: MonoBehaviour {
    void Update () {
        // Si el número de veces clicado en pantalla es mayor o igual que uno Y se ha dejado de pulsar el clic izquierdo, o si se ha dejado de pulsar el botón:
        if (((Input.touchCount >= 1 && Input.GetTouch (0).phase == TouchPhase.Moved) || (Input.GetMouseButtonUp (0)))) // (0 = botón izq., 1 = botón cent., 2= botón der.)
        {

            Vector2 pos = Input.mousePosition;
            // Se ejecuta si el juego está corriendo en Android
            if (Application.platform == RuntimePlatform.Android) {
                pos = Input.GetTouch (0).position;
            }

            Ray rayo = Camera.main.ScreenPointToRay (pos);
            RaycastHit hitInfo;
            if (Physics.Raycast (rayo, out hitInfo)) {
                if (hitInfo.collider.tag.Equals ("Player")) {
                    //Rigidbody rigidbodyCubo = hitInfo.collider.GetComponent<Rigidbody> ();
                    //rigidbodyLata.AddForce (rayo.direction * shotStrength, ForceMode.VelocityChange);
                    hitInfo.collider.GetComponent<GameObject> ();

                }
            }
        }

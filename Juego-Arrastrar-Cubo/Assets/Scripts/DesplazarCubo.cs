using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplazarCubo: MonoBehaviour {
    public GameObject cubo;
    Vector3 tamañoOriginal;

    // Update is called once per frame
    void Update () {
        if (((Input.touchCount >= 1) && Input.GetTouch (0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp (0))) {

            if (cubo == null) {
                detectarCubo ();
            } else {
                soltarCubo ();
            }
        } else if (cubo != null) {
            moverCubo ();
        }

        void detectarCubo () {
            Vector3 mousePos = Input.mousePosition;


            if (Application.platform == RuntimePlatform.Android) {
                mousePos = Input.GetTouch (0).position;
            }
            Ray detectarRayo = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast (detectarRayo, out hitInfo) == true) {

                if (hitInfo.collider.tag.Equals ("Player")) {

                    cubo = hitInfo.collider.gameObject;
                    tamañoOriginal = cubo.transform.localScale;
                    LeanTween.scale (cubo, cubo.transform.localScale * 1.2f, 0.75f).setEaseInCubic ().setLoopPingPong ();
                }
            }
        }

        void soltarCubo () {
            LeanTween.cancel (cubo);
            LeanTween.scale (cubo, tamañoOriginal, 0.75f).setEaseOutBounce ();
            cubo = null;
        }

        void moverCubo () {
            Vector3 mousePos = Input.mousePosition;
            Ray moverRayo = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            cubo.SetActive (false);

            if (Physics.Raycast (moverRayo, out hitInfo) == true) {
                cubo.transform.position = hitInfo.point + Vector3.up * (cubo.transform.localScale.y / 2f);
            }
            cubo.SetActive (true);
        }
    }
}
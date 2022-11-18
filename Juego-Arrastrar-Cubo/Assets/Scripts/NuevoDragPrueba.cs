using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NuevoDragPrueba: MonoBehaviour {
    public GameObject cube;
    public CursorMode cursorMode;
    public Texture2D cursorNormal, cursorAgarrar;
    public bool create = false;
    //bool estadoSeleccion = false;
    Vector3 originalScale;
    int accionCubo = 0;

    [SerializeField]
    TextMeshProUGUI textButton1, textButton2, textButton3, textButton4;

    [SerializeField]
    GameObject prefabGoldenCube, prefabBlueCylinder, prefabPinkSphere;

    public enum StateSelector {
        ObjectSelection,
        Move,
        Scale,
        Rotate,
        Release,
    }

    [SerializeField]
    StateSelector currentState = StateSelector.ObjectSelection;

    //[SerializeField]
    //TextMeshProUGUI mensajeUI, mensajeSeleccion, mensajeMoverORotar;

    void Update () {

        Vector2 mousePos = Input.mousePosition;

        switch (currentState) {

            case StateSelector.ObjectSelection:
                DetectCube ();
                break;

            case StateSelector.Move:
                MoveCube ();
                break;

            case StateSelector.Release:
                ReleaseCube ();
                break;

            case StateSelector.Rotate:
                cube.GetComponent<Rigidbody> ().isKinematic = true;
                RotateCube ();
                break;

            case StateSelector.Scale:
                ScaleCube ();
                break;
        }

        void DetectCube () {
            Ray detectRay = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            //mensajeSeleccion.text = "You can grab";

            if (Physics.Raycast (detectRay, out hitInfo) == true) {

                if (hitInfo.collider.tag.Equals ("Player")) {
                    cube = hitInfo.collider.gameObject;
                    originalScale = cube.transform.localScale;

                    if (Input.GetMouseButtonUp (0)) {
                        if (accionCubo == 1) {
                            currentState = StateSelector.Move;
                        } else if (accionCubo == 2) {
                            currentState = StateSelector.Rotate;
                        } else if (accionCubo == 3) {
                            currentState = StateSelector.Scale;
                        }
                    }
                }
            }
        }

        void MoveCube () {
            Ray moveRay = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            cube.SetActive (false);

            if (Physics.Raycast (moveRay, out hitInfo) == true) {
                cube.transform.position = hitInfo.point + Vector3.up * cube.transform.localScale.y / 2f;
            }
            cube.SetActive (true);

            if (Input.GetMouseButtonUp (0)) {
                currentState = StateSelector.Release;
            }
        }


        void RotateCube () {
            Vector2 mouseDelta = mousePos - (Vector2) Input.mousePosition;
            cube.transform.Rotate (mouseDelta.y, mouseDelta.x, 0f);
            mousePos = Input.mousePosition;

            if (Input.GetMouseButtonUp (0)) {
                cube.GetComponent<Rigidbody> ().isKinematic = false;
                currentState = StateSelector.Release;
            }
        }

        void ScaleCube () {
            cube.transform.localScale += Vector3.one * Input.mouseScrollDelta.y;

            if (Input.GetMouseButtonUp (0)) {
                currentState = StateSelector.Release;
            }
        }



        void ReleaseCube () {
            LeanTween.cancel (cube);
            LeanTween.scale (cube, originalScale, 0.75f).setEaseOutCubic ();
            cube = null;
            currentState = StateSelector.ObjectSelection;
        }
        /*public void EnableSelection () {
            estadoSeleccion = !estadoSeleccion;

            if (estadoSeleccion == true) {
                currentState = StateSelector.ObjectSelection;
            } else if (estadoSeleccion == false) {
                currentState = StateSelector.Idle;
            }
        }*/
    }
    public void ClickedCreate (GameObject CreatedObject) {
        create = !create;

        if (create == true) {
            Instantiate (CreatedObject, Vector3.zero, Quaternion.identity);
            currentState = StateSelector.Move;
        }

    }

    public void ClickedEscalar () {
        accionCubo = 3;
    }
    public void ClickedRotar () {
        accionCubo = 2;
    }
    public void ClickedMover () {
        accionCubo = 1;
    }
}

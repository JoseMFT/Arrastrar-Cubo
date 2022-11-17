using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NuevoDragPrueba: MonoBehaviour {
    public GameObject cube, textUI;
    public CursorMode cursorMode;
    public Texture2D cursorNormal, cursorAgarrar;
    bool estadoSeleccion = false;
    Vector3 originalScale, textSize;
    int accionCubo = 0;

    public enum stateSelector {
        Idle,
        ObjectSelection,
        Move,
        Scale,
        Rotate,
        Release,
    }
    [SerializeField]
    stateSelector currentState = stateSelector.Idle;

    [SerializeField]
    TextMeshProUGUI mensajeUI, mensajeSeleccion, mensajeMoverORotar;
    // Update is called once per frame

    void Start () {
        textSize = textUI.transform.localScale;
    }

    void Update () {

        switch (currentState) {
            case stateSelector.ObjectSelection:
                DetectCube ();
                break;

            case stateSelector.Move:
                MoveCube ();
                break;

            case stateSelector.Release:
                ReleaseCube ();
                break;

            case stateSelector.Idle:
                mensajeSeleccion.text = "You cannot grab";
                textUI.SetActive (false);
                break;

            case stateSelector.Rotate:
                RotateCube ();
                break;

            case stateSelector.Scale:
                ScaleCube ();
                break;
        }

        void DetectCube () {
            //InitialUISettings ();
            Vector3 mousePos = Input.mousePosition;
            Ray detectRay = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            mensajeSeleccion.text = "You can grab";

            if (Physics.Raycast (detectRay, out hitInfo) == true) {

                if (hitInfo.collider.tag.Equals ("Player")) {
                    cube = hitInfo.collider.gameObject;
                    originalScale = cube.transform.localScale;

                    if (Input.GetMouseButtonUp (0)) {
                        if (accionCubo == 1) {
                            currentState = stateSelector.Move;
                        } else if (accionCubo == 2) {
                            currentState = stateSelector.Rotate;
                        } else if (accionCubo == 3) {
                            currentState = stateSelector.Scale;
                        }
                    }
                }
            }
        }

        void MoveCube () {
            //GrabbingSettings ();
            Vector3 mousePos = Input.mousePosition;
            Ray moveRay = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            cube.SetActive (false);

            if (Physics.Raycast (moveRay, out hitInfo) == true) {
                cube.transform.position = hitInfo.point + Vector3.up * cube.transform.localScale.y / 2f;
            }
            cube.SetActive (true);

            if (Input.GetMouseButtonUp (0)) {
                currentState = stateSelector.Release;
            }
        }

        void RotateCube () {
            Vector2 mousePosRotation = Input.mousePosition;
            Ray rotateRay = Camera.main.ScreenPointToRay (mousePosRotation);
            RaycastHit hitInfo;
            Vector2 modifiedmousePos = mousePosRotation - new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

            if (Physics.Raycast (rotateRay, out hitInfo) == true) {

                cube.transform.rotation = Quaternion.Euler (modifiedmousePos.x, 0f, modifiedmousePos.y);
            }
            cube.SetActive (true);

            if (Input.GetMouseButtonUp (0)) {
                currentState = stateSelector.Release;
            }
        }

        void ScaleCube () {
            Vector3 mousePos = Input.mousePosition;
            Ray moveRay = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            cube.SetActive (false);
            Vector3 originalCubeSize = cube.transform.localScale;

            if (Physics.Raycast (moveRay, out hitInfo) == true) {
                cube.transform.localScale = cube.transform.localScale + (mousePos - cube.transform.position) / (Screen.width / Screen.height);
            }
            cube.SetActive (true);

            if (Input.GetMouseButtonUp (0)) {
                currentState = stateSelector.Release;
            }
        }
    }
    void ReleaseCube () {
        LeanTween.cancel (cube);
        LeanTween.scale (cube, originalScale, 0.75f).setEaseOutCubic ();
        cube = null;
        //InitialUISettings ();
        currentState = stateSelector.ObjectSelection;
    }
    /*void InitialUISettings () {
        Cursor.SetCursor (cursorNormal, Vector2.zero, cursorMode);
        textUI.SetActive (true);
        mensajeUI.text = "Click on an object to grab it";
        LeanTween.scale (textUI, textUI.transform.localScale * 1.2f, 1f).setEaseInSine ().setLoopPingPong ();
    }

    void GrabbingSettings () {
        textUI.SetActive (true);
        mensajeUI.text = "Click again to release the object";
        Cursor.SetCursor (cursorAgarrar, Vector2.zero, cursorMode);
        LeanTween.cancel (textUI);
        LeanTween.scale (textUI, textSize, 1f).setEaseOutCubic ();
        LeanTween.scale (cube, cube.transform.localScale * 1.15f, 0.75f).setEaseInSine ().setLoopPingPong ();
    }*/
    public void EnableSelection () {
        estadoSeleccion = !estadoSeleccion;

        if (estadoSeleccion == true) {
            currentState = stateSelector.ObjectSelection;
        } else if (estadoSeleccion == false) {
            currentState = stateSelector.Idle;
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

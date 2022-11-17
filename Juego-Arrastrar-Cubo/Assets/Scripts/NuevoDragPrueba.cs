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

    public enum stateSelector {
        Idle,
        ObjectSelection,
        Moving,
        Scaling,
        Release,
    }
    [SerializeField]
    stateSelector currentState = stateSelector.Idle;

    [SerializeField]
    TextMeshProUGUI mensajeUI, mensajeSeleccion;
    // Update is called once per frame

    void Start () {
        textSize = textUI.transform.localScale;
    }

    void Update () {

        switch (currentState) {
            case stateSelector.ObjectSelection:
                DetectCube ();

                break;
            case stateSelector.Moving:
                MoveCube ();
                break;
            case stateSelector.Release:
                ReleaseCube ();
                break;

            case stateSelector.Idle:
                mensajeSeleccion.text = "You cannot grab";
                textUI.SetActive (false);
                break;
        }

        void DetectCube () {
            InitialUISettings ();
            Vector3 mousePos = Input.mousePosition;
            Ray detectRay = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            mensajeSeleccion.text = "You can grab";

            if (Physics.Raycast (detectRay, out hitInfo) == true) {

                if (hitInfo.collider.tag.Equals ("Player")) {
                    cube = hitInfo.collider.gameObject;
                    originalScale = cube.transform.localScale;
                    LeanTween.scale (cube, cube.transform.localScale * 1.15f, 0.75f).setEaseInSine ().setLoopPingPong ();
                    if (Input.GetMouseButtonUp (0)) {
                        currentState = stateSelector.Moving;
                    }
                }
            }
        }

        void ReleaseCube () {
            LeanTween.cancel (cube);
            LeanTween.scale (cube, originalScale, 0.75f).setEaseOutCubic ();
            cube = null;
            InitialUISettings ();
            currentState = stateSelector.ObjectSelection;
        }

        void MoveCube () {
            GrabbingSettings ();
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
    }
    void InitialUISettings () {
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
    }
    public void ClickedButton () {
        estadoSeleccion = !estadoSeleccion;
        if (estadoSeleccion == true) {
            currentState = stateSelector.ObjectSelection;
        } else if (estadoSeleccion == false) {
            currentState = stateSelector.Idle;
        }
    }
}

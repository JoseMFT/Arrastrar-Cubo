using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NuevoDragPrueba: MonoBehaviour {
    public GameObject cube, textUI;
    public CursorMode cursorMode;
    public Texture2D cursorNormal, cursorAgarrar;
    Vector3 originalScale, textSize;


    [SerializeField]
    TextMeshProUGUI mensajeUI;
    // Update is called once per frame

    void Start () {
        InitialUISettings ();
        textSize = textUI.transform.localScale;
    }

    void Update () {

        if (Input.GetMouseButtonDown (0)) {

            if (cube == null) {
                DetectCube ();
            } else {
                ReleaseCube ();
            }

        } else if (cube != null) {
            MoveCube ();
        }

        void DetectCube () {
            Vector3 mousePos = Input.mousePosition;
            Ray detectRay = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;

            if (Physics.Raycast (detectRay, out hitInfo) == true) {

                if (hitInfo.collider.tag.Equals ("Player")) {
                    cube = hitInfo.collider.gameObject;
                    originalScale = cube.transform.localScale;
                    LeanTween.scale (cube, cube.transform.localScale * 1.15f, 0.75f).setEaseInSine ().setLoopPingPong ();
                }
            }
        }

        void ReleaseCube () {
            LeanTween.cancel (cube);
            LeanTween.scale (cube, originalScale, 0.75f).setEaseOutCubic ();
            cube = null;
            InitialUISettings ();
        }

        void MoveCube () {
            GrabbingSettings ();
            Vector3 mousePos = Input.mousePosition;
            Ray moveRay = Camera.main.ScreenPointToRay (mousePos);
            RaycastHit hitInfo;
            Cursor.SetCursor (cursorAgarrar, Vector2.zero, cursorMode);
            cube.SetActive (false);

            if (Physics.Raycast (moveRay, out hitInfo) == true) {
                cube.transform.position = hitInfo.point + Vector3.up * cube.transform.localScale.y / 2f;
            }
            cube.SetActive (true);
        }
    }

    void InitialUISettings () {
        Cursor.SetCursor (cursorNormal, Vector2.zero, cursorMode);
        mensajeUI.text = "Click on an object to grab it";
        LeanTween.scale (textUI, textUI.transform.localScale * 1.2f, 1f).setEaseInSine ().setLoopPingPong ();
    }

    void GrabbingSettings () {
        mensajeUI.text = "Click again to release the object";
        LeanTween.cancel (textUI);
        LeanTween.scale (textUI, textSize, 1f).setEaseOutCubic ();
    }
}

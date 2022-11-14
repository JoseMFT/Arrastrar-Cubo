using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDrag : MonoBehaviour
{
    // Start is called before the first frame update
    public void desplazar (Vector3 mousePos) {
        transform.Translate ((mousePos.x / Screen.width) * Time.deltaTime, 0f, (mousePos.y / Screen.height) * Time.deltaTime);
    }
}

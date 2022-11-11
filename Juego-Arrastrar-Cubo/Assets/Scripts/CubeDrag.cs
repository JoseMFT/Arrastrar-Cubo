using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDrag : MonoBehaviour
{
    // Start is called before the first frame update
    public void desplazar (Vector3 mousePos) {
        this.Transform.position (mousePos.x / Screen.width, 0f, mousePos.y / Screen.height);
    }
}

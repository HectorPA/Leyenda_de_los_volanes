using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    Vector3 posicionRaton;
    Camera thisCam;

    void Awake()
    {
        thisCam = GetComponent<Camera>();
    }
    
    void Update()
    {
        posicionRaton = thisCam.ScreenToViewportPoint(Input.mousePosition);
    }
}

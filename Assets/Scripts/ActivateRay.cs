using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateRay : MonoBehaviour
{
    public GameObject rightRayObj;
    public InputActionProperty rightActivate;
    void Update()
    {
        rightRayObj.SetActive(rightActivate.action.ReadValue<float>() > 0.01f);
    }
}

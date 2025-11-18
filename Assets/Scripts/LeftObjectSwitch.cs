using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftObjectSwitch : MonoBehaviour
{
    public InputActionReference popcornBtn;
    public InputActionReference phoneBtn;
    GameObject popcorn;
    GameObject phone;
    GameObject hand;
    bool isPopcornActive = false;
    bool isPhoneActive = false;

    private void Awake()
    {
        phone = transform.GetChild(0).gameObject;
        popcorn = transform.GetChild(1).gameObject;
        hand = transform.GetChild(2).gameObject;
    }

    void Update()
    {
        popcornBtn.action.performed += ctx => SwitchToPopcorn();
        phoneBtn.action.performed += ctx => SwitchToPhone();
    }

    void SwitchToPopcorn()
    {
        if(!isPopcornActive)
        {
            hand.SetActive(false);
            popcorn.SetActive(true);
            phone.SetActive(false);
            isPopcornActive = true;
            isPhoneActive = false;
        }
        else
        {
            hand.SetActive(true);
            popcorn.SetActive(false);
            isPopcornActive = false;
        }
    }

    void SwitchToPhone()
    {
        if(!isPhoneActive)
        {
            hand.SetActive(false);
            phone.SetActive(true);
            popcorn.SetActive(false);
            isPhoneActive = true;
            isPopcornActive = false;
        }
        else
        {
            hand.SetActive(true);
            phone.SetActive(false);
            isPhoneActive = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCanvas : MonoBehaviour
{
    public GameObject home;
    public GameObject shoot;
    public GameObject call;
    public GameObject list;

    private void Awake()
    {
        home = transform.GetChild(0).gameObject;
        shoot = transform.GetChild(1).gameObject;
        call = transform.GetChild(2).gameObject;
        list = transform.GetChild(3).gameObject;
    }

    public void ShowList()
    {
        list.SetActive(true);
        home.SetActive(false);
        shoot.SetActive(false);
        call.SetActive(false);
    }

    public void ShowShoot()
    {
        shoot.SetActive(true);
        home.SetActive(false);
        call.SetActive(false);
        list.SetActive(false);
    }

    public void ShowCall()
    {
        call.SetActive(true);
        home.SetActive(false);
        shoot.SetActive(false);
        list.SetActive(false);
    }

    public void ShowHome()
    {
        home.SetActive(true);
        shoot.SetActive(false);
        call.SetActive(false);
        list.SetActive(false);
    }
}

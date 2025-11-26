using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedPopcorn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Instantiate(Resources.Load("Prefabs/popVfx"), transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

}

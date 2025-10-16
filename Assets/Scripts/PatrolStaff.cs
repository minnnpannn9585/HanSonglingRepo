using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStaff : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    private bool toPointOne;
    public Transform character;

    // Update is called once per frame
    void Update()
    {
        if(toPointOne)
        {
            character.position = Vector3.MoveTowards(character.position, points[1].position, speed * Time.deltaTime);
            if(character.position == points[1].position)
            {
                toPointOne = false;
            }
        }
        else
        {
            character.position = Vector3.MoveTowards(character.position, points[0].position, speed * Time.deltaTime);
            if(character.position == points[0].position)
            {
                toPointOne = true;
            }
        }

    }
}

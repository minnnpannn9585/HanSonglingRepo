using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStaff : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public bool toPointOne;
    public Transform character;
    public float ToPointOneDir;
    public float ToPointTwoDir;
    public float rot01;
    public float rot02;
    public float rot03;

    private bool isLooking = false;

    // rotation speed in degrees per second (exposed to Inspector)
    public float rotationSpeed = 180f;

    // Update is called once per frame
    void Update()
    {
        if (isLooking) return; // pause movement/rotation while looking around

        if (toPointOne)
        {
            // face forward while moving to points[1]
            character.localRotation = Quaternion.Euler(0f, ToPointOneDir, 0f);
            character.position = Vector3.MoveTowards(character.position, points[1].position, speed * Time.deltaTime);

            if (Vector3.Distance(character.position, points[1].position) < 0.01f)
            {
                StartCoroutine(LookAroundThenReturn());
            }
        }
        else
        {
            // face backwards while moving to points[0]
            character.localRotation = Quaternion.Euler(0f, ToPointTwoDir, 0f);
            character.position = Vector3.MoveTowards(character.position, points[0].position, speed * Time.deltaTime);

            if (Vector3.Distance(character.position, points[0].position) < 0.01f)
            {
                toPointOne = true;
            }
        }
    }

    private IEnumerator LookAroundThenReturn()
    {
        isLooking = true;

        // sequence:
        // 1) left 270° (rot01)
        // 2) right 180° (rot02)
        // 3) left 90° (rot03)
        yield return RotateByDegrees(rot01);
        yield return RotateByDegrees(rot02);
        yield return RotateByDegrees(rot03);

        // ensure final facing is toward point[0] (180°)
        character.localRotation = Quaternion.Euler(0f, 180f, 0f);

        isLooking = false;
        toPointOne = false; // start moving back to point[0]
    }

    // rotates the character by 'degrees' (can be negative for left) using rotationSpeed (deg/sec)
    private IEnumerator RotateByDegrees(float degrees)
    {
        // handle zero rotation quickly
        if (Mathf.Approximately(degrees, 0f))
            yield break;

        float start = character.localEulerAngles.y;
        float target = start + degrees;

        // compute duration from rotationSpeed; if invalid, do instant rotation
        float speed = Mathf.Abs(rotationSpeed);
        if (speed <= Mathf.Epsilon)
        {
            character.localRotation = Quaternion.Euler(0f, target, 0f);
            yield break;
        }

        float duration = Mathf.Abs(degrees) / speed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            float current = Mathf.Lerp(start, target, t);
            character.localRotation = Quaternion.Euler(0f, current, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ensure exact final angle
        character.localRotation = Quaternion.Euler(0f, target, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POsitonsfdsdofnklknlsdf : MonoBehaviour
{
   public Transform startPoint;
    public Transform endPoint;
    public float duration = 5f;

    private float elapsedTime = 0f;

    void Update()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate the interpolation factor using SmoothStep for ease-in and ease-out
        float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);

        // Use Vector3.Lerp to interpolate the position
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);

        // Use Quaternion.Slerp to interpolate the rotation (optional)
        transform.rotation = Quaternion.Slerp(startPoint.rotation, endPoint.rotation, t);

        // If the interpolation is complete, reset the time
        if (t >= 1f)
        {
            // Reverse the direction by swapping start and end points
            Transform temp = startPoint;
            startPoint = endPoint;
            endPoint = temp;

            elapsedTime = 0f;
        }
    }
}

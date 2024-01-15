using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Update()
    {
        Vector3 targetPosition = (player.position + Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f))) / 2f;

        targetPosition += offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}

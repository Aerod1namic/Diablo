using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SmartCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float Height = 2f;
    [SerializeField] private float SpeedZoom = 2f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 15f;
    [SerializeField] private float RotateSpeed = 120f;
    private float CurrentRotate = 0f;
    private float CurrentZoom = 10f;

    private void Update()
    {
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * SpeedZoom;
        CurrentZoom = Mathf.Clamp(CurrentZoom, minZoom, maxZoom);

        CurrentRotate -= Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = target.position- offset * CurrentZoom;
        transform.LookAt(target.position + Vector3.up * Height);

        transform.RotateAround(target.position, Vector3.up, CurrentRotate);
    }
}

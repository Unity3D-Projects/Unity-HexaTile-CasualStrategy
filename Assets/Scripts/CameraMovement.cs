﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 startMousePosition;
    private Vector3 currentMousePosition;
    private Vector3 startCameraPosition;

    private Camera mainCamera;

    [SerializeField]
    private float maxZoom = 7f;
    [SerializeField]
    private float minZoom = 3f;

    public float moveSpeed = 0.01f;
    public float zoomSpeed = 5f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startMousePosition = Input.mousePosition;
            startCameraPosition = transform.position;
        }

        else if (Input.GetMouseButton(1))
        {
            currentMousePosition = Input.mousePosition;

            Vector3 diff = startMousePosition - currentMousePosition;

            diff = new Vector3(diff.x, 0, diff.y);

            transform.position = startCameraPosition + diff * moveSpeed;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (mainCamera.orthographicSize < maxZoom && scroll < 0)
        {
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - scroll, minZoom, maxZoom);
        }
        else if (mainCamera.orthographicSize >= minZoom && scroll > 0)
        {
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - scroll, minZoom, maxZoom);
        }
    }
}
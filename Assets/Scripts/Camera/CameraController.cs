using System;
using System.Collections;
using System.Collections.Generic;
// using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables

    [Header("Speed and Movement")]
    [SerializeField] private float normalSpeed = 0.1f;
    [SerializeField] private float fastSpeed = 0.25f;
    [SerializeField] private float speed;
    [SerializeField] private float movementTime = 5f;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;

    [Header("Rotation")] 
    [SerializeField] private float normalSpeedRotaion = 1f;
    [SerializeField] private float fastSpeedRotation = 2.5f;
    [SerializeField] private float rotationAmount = 1f;
    private Quaternion newRotation;

    private Vector3 startRotationPosition;
    private Vector3 currentRotationPosition;
    private bool isRotating;

    [Header("Zoom")] 
    [SerializeField] private Transform cameraTransform;

    [SerializeField][Range(25,100)] private float speedOfScroll;
    [SerializeField] private Vector3 zoomAmount;
    private Vector3 newZoom;

    [Header("Limit of Camera")]
    [SerializeField] private Vector2 limitOfCamera;
    [SerializeField] private float limitMinZoom;
    [SerializeField] private float limitMaxZoom;

    //Others
    private Vector3 newPosition;
    
    #endregion
    
    

    private void Start() {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    private void Update() {
        MouseInput();
        LimitsOfCamera();
    }

    private void FixedUpdate() {
        MovementInput();
    }

    #region MovementCamera
    private void MouseInput() {
        //Movement Camera
        if (Input.GetMouseButtonDown(2)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry)) {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(2)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (Camera.main != null) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry)) {
                    dragCurrentPosition = ray.GetPoint(entry);

                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 15f);
        }
        
        
        //Zoom Camera
        newZoom += zoomAmount * (Input.mouseScrollDelta.y * speedOfScroll);
        
    }

    private void MovementInput() {
        //Speed Of Movement and Rotation Camera
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            speed = fastSpeed;
            rotationAmount = fastSpeedRotation;
        }
        else {
            speed = normalSpeed;
            rotationAmount = normalSpeedRotaion;
        }

        //Movement Camera
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            newPosition += (transform.forward * speed);
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            newPosition += (transform.forward * -speed);
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            newPosition += (transform.right * speed);
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            newPosition += (transform.right * -speed);
        
        
        
        //Rotation Camera
        if(Input.GetKey(KeyCode.Q))
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        if (Input.GetKey(KeyCode.E))
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);

        //Zoom Camera
        if (Input.GetKey(KeyCode.R))
            newZoom += zoomAmount;
        if (Input.GetKey(KeyCode.F))
            newZoom -= zoomAmount;

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
    #endregion

    #region LimitOfCamera

    private void LimitsOfCamera() {
        newPosition.x = Mathf.Clamp(newPosition.x, -limitOfCamera.x, limitOfCamera.x);
        newPosition.z = Mathf.Clamp(newPosition.z, -limitOfCamera.y, limitOfCamera.y);
        
        //Limit of zoom
        newZoom.y = Mathf.Clamp(newZoom.y, limitMinZoom, limitMaxZoom);
        newZoom.z = Mathf.Clamp(newZoom.z,-limitMaxZoom, -limitMinZoom);
    }

    #endregion
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private bool useEdgeScrolling = false;
    [SerializeField] private bool useDragPan = false;
    [SerializeField] private float orthographicSizeMin = 5;
    [SerializeField] private float orthographicSizeMax = 35;

    [SerializeField] private Rigidbody2D rigidBody;

    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;
    private float targetOrthographicSize = 5;

    private void Update() {
        HandleCameraMovement();

        if (useEdgeScrolling) {
            HandleCameraMovementEdgeScrolling();
        }

        if (useDragPan) {
            HandleCameraMovementDragPan();
        }

        //HandleCameraRotation();

        HandleCameraZoom_OrthographicSize();
    }

    private void HandleCameraMovement() {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDir.y = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.y = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        Vector3 moveDir = transform.up * inputDir.y + transform.right * inputDir.x;

        float moveSpeed = 15f;
        //transform.position += moveDir * moveSpeed * Time.deltaTime;

        rigidBody.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    private void HandleCameraMovementEdgeScrolling() {
        Vector3 inputDir = new Vector3(0, 0, 0);

        int edgeScrollSize = 20;

        if (Input.mousePosition.x < edgeScrollSize) {
            inputDir.x = -1f;
        }
        if (Input.mousePosition.y < edgeScrollSize) {
            inputDir.y = -1f;
        }
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) {
            inputDir.x = +1f;
        }
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) {
            inputDir.y = +1f;
        }

        Vector3 moveDir = transform.up * inputDir.y + transform.right * inputDir.x;

        float moveSpeed = 50f;
        //transform.position += moveDir * moveSpeed * Time.deltaTime;
        rigidBody.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);

    }

    private void HandleCameraMovementDragPan() {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1)) {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1)) {
            dragPanMoveActive = false;
        }

        if (dragPanMoveActive) {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

            float dragPanSpeed = 1f;
            inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            inputDir.y = mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;
        }

        Vector3 moveDir = transform.up * inputDir.y + transform.right * inputDir.x;

        float moveSpeed = 50f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation() {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        float rotateSpeed = 100f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }

    private void HandleCameraZoom_OrthographicSize() {
        if (Input.mouseScrollDelta.y > 0) {
            targetOrthographicSize -= 2;
        }
        if (Input.mouseScrollDelta.y < 0) {
            targetOrthographicSize += 2;
        }

        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, orthographicSizeMin, orthographicSizeMax);

        float zoomSpeed = 10f;
        cinemachineVirtualCamera.m_Lens.OrthographicSize =
            Mathf.Lerp(cinemachineVirtualCamera.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
    }

}


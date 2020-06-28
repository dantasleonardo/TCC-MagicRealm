using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D attackCursor;
    [SerializeField] private Texture2D collectCursor;
    [SerializeField] private Texture2D defaultCursor;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        SetCursor();
    }

    private void SetCursor()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.CompareTag("Resources"))
                Cursor.SetCursor(collectCursor,new Vector2(10.0f,10.0f), CursorMode.Auto);
            else if (hit.collider.CompareTag("Mages"))
            {
                Cursor.SetCursor(attackCursor,new Vector2(10.0f,10.0f), CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(defaultCursor,new Vector2(10.0f,10.0f), CursorMode.Auto);
            }
        }
    }
}

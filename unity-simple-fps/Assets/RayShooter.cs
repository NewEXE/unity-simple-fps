using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        this._camera = GetComponent<Camera>();
        SetupCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                StartCoroutine(sphereIndicator(hit.point));
            }
        }
    }
    
    private IEnumerator sphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        
        yield return new WaitForSeconds(1);
        
        Destroy(sphere);
    }

    private static void SetupCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = this._camera.pixelWidth / 2 - size/4;
        float posY = this._camera.pixelHeight / 2 - size/2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}

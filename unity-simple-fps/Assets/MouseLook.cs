using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }

    public RotationAxes axes;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45f;
    public float maximumVert = 45f;

    private float _rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.axes)
        {
            case RotationAxes.MouseX:
                float axisX = Input.GetAxis("Mouse X");

                // Optimization (?): there is no reason to call 
                // transform.Rotate if mouse doesn't moved by X coordinate.
                if (axisX != 0)
                {
                    transform.Rotate(0, axisX * sensitivityHor, 0);
                    //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + (Input.GetAxis("Mouse X") * this.sensitivityHor), 0);
                }
                break;
            case RotationAxes.MouseY:
                _rotationX -= Input.GetAxis("Mouse Y") * this.sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, this.minimumVert, this.maximumVert);

                float rotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
                break;
            default:
                _rotationX -= Input.GetAxis("Mouse Y") * this.sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, this.minimumVert, this.maximumVert);

                float delta = Input.GetAxis("Mouse X") * this.sensitivityHor;
                rotationY = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
                break;
        }
    }
}

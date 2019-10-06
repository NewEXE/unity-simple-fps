using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _characterController;
    // Start is called before the first frame update
    void Start()
    {
        this._characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * this.speed;
        float deltaZ = Input.GetAxis("Vertical") * this.speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, this.speed);
        movement.y = this.gravity;
        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);
        
        _characterController.Move(movement);
    }
}

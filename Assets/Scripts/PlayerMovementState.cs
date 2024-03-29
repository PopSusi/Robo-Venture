using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState
{
    Vector2 moveVelocity;
    InputAction moveAction;
    CharacterController controller;
    Transform characterTransform;
    float ySpeed;
    float gravity, accel, airAccel;
    public PlayerMovementState(InputAction moveAction,CharacterController controller,Transform transform,float accel,float airAccel, float gravity)
    {
        this.moveAction = moveAction;
        this.controller = controller;
        this.characterTransform = transform;
        this.gravity = gravity;
        this.accel = accel;
        this.airAccel = airAccel;
    }
    public void MovePlayer(Vector2 targetVelocity)
    {
        moveVelocity = Vector2.MoveTowards(new Vector2(controller.velocity.x, controller.velocity.z), targetVelocity, (controller.isGrounded ? accel : airAccel));

        controller.Move(new Vector3(moveVelocity.x, ySpeed, moveVelocity.y) * Time.fixedDeltaTime);
        if (moveAction.IsPressed())
        {
            characterTransform.rotation = Quaternion.LookRotation(new Vector3(moveVelocity.x, 0, moveVelocity.y), Vector3.up);
        }
        if (!controller.isGrounded)
        {
            ySpeed += gravity * Time.fixedDeltaTime;

        }
        else
        {
            ySpeed = -0.1f;
        }


    }
}

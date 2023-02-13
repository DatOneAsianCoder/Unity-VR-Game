using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput, curBrakeForce, curSteerAngle;
    private bool isBraking;

    //Collider Variables
    [SerializeField] private WheelCollider Front_Wheel_Left_Collider;
    [SerializeField] private WheelCollider Back_Wheel_Left_Collider;
    [SerializeField] private WheelCollider Front_Wheel_Right_Collider;
    [SerializeField] private WheelCollider Back_Wheel_Right_Collider;

    //Driving Variables
    [SerializeField] private float motorForce, brakeForce, maxSteerAngle;
    //Same if you created a new SerializedField private float for brakeForce!

    //Transform Variables (Steering)
    [SerializeField] private Transform Front_Wheel_Left;
    [SerializeField] private Transform Back_Wheel_Left;
    [SerializeField] private Transform Front_Wheel_Right;
    [SerializeField] private Transform Back_Wheel_Right;

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);

        Back_Wheel_Left_Collider.motorTorque = verticalInput * motorForce;
        Back_Wheel_Right_Collider.motorTorque = verticalInput * motorForce;
        curBrakeForce = isBraking ? brakeForce : 0f;

        //Above same as below
        //if (isBraking)
        //{
        //    curBrakeForce = brakeForce;
        //}
        //else
        //{
        //    curBrakeForce = 0;
        //}

        ApplyBrakeForce();
        HandleSteering();
        UpdateWheels();
    }

    void ApplyBrakeForce()
    {
        Front_Wheel_Left_Collider.brakeTorque = curBrakeForce;
        Front_Wheel_Right_Collider.brakeTorque = curBrakeForce;
        Back_Wheel_Left_Collider.brakeTorque = curBrakeForce;
        Back_Wheel_Right_Collider.brakeTorque = curBrakeForce;
    }

    void HandleSteering()
    {
        curSteerAngle = maxSteerAngle * horizontalInput;
        Front_Wheel_Left_Collider.steerAngle = curSteerAngle;
        Front_Wheel_Right_Collider.steerAngle = curSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(Front_Wheel_Left_Collider, Front_Wheel_Left);
        UpdateSingleWheel(Front_Wheel_Right_Collider, Front_Wheel_Right);
        UpdateSingleWheel(Back_Wheel_Left_Collider, Back_Wheel_Left);
        UpdateSingleWheel(Back_Wheel_Right_Collider, Back_Wheel_Right);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}

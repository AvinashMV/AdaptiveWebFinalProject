using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {

	public void GetInput(){
		m_horizontalInput = Input.GetAxis ("Horizontal");
		m_verticalInput = Input.GetAxis ("Vertical");
	}

	private void Steer(){
		m_steerAngle = maxSteerAngle * m_horizontalInput;
		frontLeftW.steerAngle = m_steerAngle;
		frontRightW.steerAngle = m_steerAngle;
	}

	private void Accelerate(){
		rearLeftW.motorTorque = m_verticalInput * motorForce;
		rearRightW.motorTorque = m_verticalInput * motorForce;
	}

	private void UpdateWheelPoses(){
		UpdateWheelPose (frontLeftW, frontLeftT);
		UpdateWheelPose (frontRightW, frontRightT);
		UpdateWheelPose (rearLeftW, rearLeftT);
		UpdateWheelPose (rearRightW, rearRightT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform){
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose (out _pos, out _quat);
		_transform.position = _pos;
		_transform.rotation = _quat;

	}

	private void FixedUpdate(){
		GetInput ();
		Steer ();
		Accelerate ();
		UpdateWheelPoses ();
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steerAngle;

	public WheelCollider frontLeftW, frontRightW;
	public WheelCollider rearLeftW, rearRightW;

	public Transform frontLeftT, frontRightT;
	public Transform rearLeftT, rearRightT;

	public float maxSteerAngle = 30;
	public float motorForce = 50;
}

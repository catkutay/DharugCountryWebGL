using System;

using UnityEngine;
using System.Collections;

public class MouseOrbit: MonoBehaviour {
	public Transform target ;
	float distance = 10.0f;

	float xSpeed = 250.0f;
	float ySpeed = 120.0f;

	int yMinLimit = -20;
	int yMaxLimit = 80;

	private float x = 0.0f;
	private float y = 0.0f;

		//@script AddComponentMenu("Camera-Control/Mouse Orbit")
	void Start () {
			Vector3 angles = transform.eulerAngles;
			x = angles.y;
			y = angles.x;

			// Make the rigid body not change rotation
			if (GetComponent<Rigidbody>())
				GetComponent<Rigidbody>().freezeRotation = true;
		}
	void LateUpdate () {
			if (target) {
				x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

				y = ClampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);

			Vector3 position = new Vector3(0.0f, 0.0f, -distance);
				position = rotation *  position + target.position;

				transform.rotation = rotation;
				transform.position = position;
			}
		}
	float ClampAngle (float angle , float min , float max ) {
			if (angle < -360)
				angle += 360;
			if (angle > 360)
				angle -= 360;
			return Mathf.Clamp (angle, min, max);
	}

}






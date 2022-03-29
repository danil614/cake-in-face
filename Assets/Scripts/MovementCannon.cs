using UnityEngine;
using System.Collections;
public class MovementCannon : MonoBehaviour
{
    private WheelJoint2D[] wheels;
    private Rigidbody2D cannonRigidbody;

    [SerializeField]
    private Rigidbody2D cannonWheelRigidbody;

    [SerializeField]
    private float leftSpeed;
    [SerializeField]
    private float leftWait;

    [SerializeField]
    private float rightSpeed;
    [SerializeField]
    private float rightWait;

    private void Start()
    {
        wheels = gameObject.GetComponents<WheelJoint2D>();
        cannonRigidbody = GetComponent<Rigidbody2D>();
    }

    public IEnumerator DoCannonKickback()
    {
        SetRigidbodyType(cannonRigidbody, false);
        SetRigidbodyType(cannonWheelRigidbody, false);

        SetMotorSpeed(leftSpeed, true);
        yield return new WaitForSeconds(leftWait);
        SetMotorSpeed(0, false);

        SetMotorSpeed(-rightSpeed, true);
        yield return new WaitForSeconds(rightWait);
        SetMotorSpeed(0, false);

        SetRigidbodyType(cannonRigidbody, true);
        SetRigidbodyType(cannonWheelRigidbody, true);
    }

    private void SetMotorSpeed(float speed, bool useMotor)
    {
        foreach (WheelJoint2D wheel in wheels)
        {
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed = speed;
            wheel.motor = motor;
            wheel.useMotor = useMotor;
        }
    }

    private void SetRigidbodyType(Rigidbody2D rigidbody, bool isStatic)
    {
        if (isStatic)
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
using UnityEngine;
using System.Collections;
public class MovementCannon : MonoBehaviour
{
    private WheelJoint2D[] wheels;
    private Rigidbody2D cannonRigidbody;

    [SerializeField]
    private Rigidbody2D cannonWheelRigidbody;

    void Start()
    {
        wheels = gameObject.GetComponents<WheelJoint2D>();
        cannonRigidbody = GetComponent<Rigidbody2D>();
        //StartCoroutine(MoveBackCannon());
    }

    void Update()
    {
        //if (Input.GetAxis("Horizontal") < -0.8 || Input.GetAxis("Horizontal") > 0.8)
        //{
        //    foreach (WheelJoint2D joint in wheels)
        //    {
        //        var motor = joint.motor;
        //        motor.motorSpeed = 1000 * Input.GetAxis("Horizontal") * -1;
        //        joint.motor = motor;
        //        joint.useMotor = true;
        //    }
        //}
        //else
        //{
        //    foreach (WheelJoint2D joint in wheels)
        //    {
        //        var motor = joint.motor;
        //        motor.motorSpeed = 0;
        //        joint.motor = motor;
        //        joint.useMotor = false;
        //    }
        //}

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

    public IEnumerator DoCannonKickback()
    {
        SetRigidbodyType(cannonRigidbody, false);
        SetRigidbodyType(cannonWheelRigidbody, false);

        Debug.Log("Start Right");
        SetMotorSpeed(10000, true);
        yield return new WaitForSeconds(0.2f);
        SetMotorSpeed(0, false);
        Debug.Log("Stop Right");

        Debug.Log("Start Left");
        SetMotorSpeed(-1000, true);
        yield return new WaitForSeconds(0.5f);
        SetMotorSpeed(0, false);
        Debug.Log("Stop Left");

        SetRigidbodyType(cannonRigidbody, true);
        SetRigidbodyType(cannonWheelRigidbody, true);
    }
}
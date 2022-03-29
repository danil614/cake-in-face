using UnityEngine;
using System.Collections;
public class MovementCannon : MonoBehaviour
{
    private WheelJoint2D[] wheels; // Колеса
    private Rigidbody2D cannonRigidbody; // Rigidbody самой пушки

    [SerializeField]
    private Rigidbody2D cannonWheelRigidbody; // Rigidbody колеса от пушки

    [SerializeField]
    private float leftSpeed; // Скорость пушки влево
    [SerializeField]
    private float leftWait; // Ожидание при левом движении

    [SerializeField]
    private float rightSpeed; // Скорость пушки вправо
    [SerializeField]
    private float rightWait; // Ожидание при правом движении

    private void Start()
    {
        wheels = gameObject.GetComponents<WheelJoint2D>(); // Получаем все колеса от пушки
        cannonRigidbody = GetComponent<Rigidbody2D>(); // Получаем Rigidbody самой пушки
    }

    public IEnumerator DoCannonKickback()
    {
        // Устанавливаем Dynamic на Rigidbody пушки и колеса
        SetRigidbodyType(cannonRigidbody, false);
        SetRigidbodyType(cannonWheelRigidbody, false);

        // Сначала двигаем пушку влево
        SetMotorSpeed(leftSpeed, true);
        yield return new WaitForSeconds(leftWait);
        SetMotorSpeed(0, false);

        // Далее, двигаем вправо
        SetMotorSpeed(-rightSpeed, true);
        yield return new WaitForSeconds(rightWait);
        SetMotorSpeed(0, false);

        // Устанавливаем Static на Rigidbody пушки и колеса
        SetRigidbodyType(cannonRigidbody, true);
        SetRigidbodyType(cannonWheelRigidbody, true);
    }

    /// <summary>
    /// Устанавливает скорость колеса.
    /// </summary>
    /// <param name="speed">Скорость колеса</param>
    /// <param name="useMotor">Использовать поворот колеса?</param>
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

    /// <summary>
    /// Устанавливает тип Rigidbody.
    /// </summary>
    /// <param name="rigidbody">Rigidbody</param>
    /// <param name="isStatic">Установить статичный тип?</param>
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
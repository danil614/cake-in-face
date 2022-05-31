using System.Collections;
using UnityEngine;

public class MovementCannon : MonoBehaviour
{
    [SerializeField] private Rigidbody2D cannonWheelRigidbody; // Rigidbody колеса от пушки

    [SerializeField] private float leftSpeed; // Скорость пушки влево
    [SerializeField] private float leftWait; // Ожидание при левом движении

    [SerializeField] private float rightSpeed; // Скорость пушки вправо
    [SerializeField] private float rightWait; // Ожидание при правом движении
    private Rigidbody2D _cannonRigidbody; // Rigidbody самой пушки

    private WheelJoint2D[] _wheels; // Колеса

    private void Start()
    {
        _wheels = GetComponents<WheelJoint2D>(); // Получаем все колеса от пушки
        _cannonRigidbody = GetComponent<Rigidbody2D>(); // Получаем Rigidbody самой пушки

        // Устанавливаем Static на Rigidbody пушки и колеса
        SetRigidbodyType(_cannonRigidbody, true);
        SetRigidbodyType(cannonWheelRigidbody, true);
    }

    public IEnumerator DoCannonKickback()
    {
        // Устанавливаем Dynamic на Rigidbody пушки и колеса
        SetRigidbodyType(_cannonRigidbody, false);
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
        SetRigidbodyType(_cannonRigidbody, true);
        SetRigidbodyType(cannonWheelRigidbody, true);
    }

    /// <summary>
    ///     Устанавливает скорость колеса.
    /// </summary>
    /// <param name="speed">Скорость колеса</param>
    /// <param name="useMotor">Использовать поворот колеса?</param>
    private void SetMotorSpeed(float speed, bool useMotor)
    {
        foreach (var wheel in _wheels)
        {
            // Для перемещения пушки будем использовать мотор
            var motor = wheel.motor;
            // Устанавливаем скорость мотора
            motor.motorSpeed = speed;
            wheel.motor = motor;
            wheel.useMotor = useMotor;
        }
    }

    /// <summary>
    ///     Устанавливает тип Rigidbody.
    /// </summary>
    /// <param name="rigidbody">Rigidbody</param>
    /// <param name="isStatic">Установить статичный тип?</param>
    private static void SetRigidbodyType(Rigidbody2D rigidbody, bool isStatic)
    {
        rigidbody.bodyType = isStatic ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }
}
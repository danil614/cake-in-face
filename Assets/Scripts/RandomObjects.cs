using System.Collections;
using UnityEngine;

public class RandomObjects : MonoBehaviour
{
    [SerializeField] [Header("Максимальное случайное число")]
    private int maxRandomNumber;

    [SerializeField] [Header("Задержка проверки случайного числа")]
    private float checkDelay;

    [SerializeField] [Header("Задержка отключения объекта")]
    private float disableDelay;

    [SerializeField] [Header("Объекты")] private GameObject[] objects;
    private GameObject _currentObject;
    private bool _isEnable;

    private int _originalRandomNumber;

    private void Start()
    {
        _isEnable = false;
        _originalRandomNumber = Random.Range(0, maxRandomNumber);
        StartCoroutine(CheckRandomNumber());
    }

    private IEnumerator CheckRandomNumber()
    {
        while (true)
        {
            var randomNumber = Random.Range(0, maxRandomNumber);
            if (!_isEnable && randomNumber == _originalRandomNumber)
            {
                _currentObject = objects[Random.Range(0, objects.Length)];
                _currentObject.SetActive(true);
                _isEnable = true;
                StartCoroutine(DisableObjectWithDelay());
            }

            yield return new WaitForSeconds(checkDelay);
        }
    }

    private IEnumerator DisableObjectWithDelay()
    {
        yield return new WaitForSeconds(disableDelay);
        _currentObject.SetActive(false);
        _isEnable = false;
    }
}
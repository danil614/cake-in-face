using UnityEngine;
using System.Collections;

public class RandomObjects : MonoBehaviour
{
    [SerializeField][Header("Максимальное случайное число")] private int maxRandomNumber;
    [SerializeField][Header("Задержка проверки случайного числа")] private float checkDelay;
    [SerializeField][Header("Задержка отключения объекта")] private float disableDelay;
    [SerializeField][Header("Объекты")] private GameObject[] objects;

    private int originalRandomNumber;
    private GameObject currentObject;
    private bool isEnable;

    void Start()
    {
        isEnable = false;
        originalRandomNumber = Random.Range(0, maxRandomNumber);
        StartCoroutine(CheckRandomNumber());
    }

    private IEnumerator CheckRandomNumber()
    {
        while (true)
        {
            int randomNumber = Random.Range(0, maxRandomNumber);
            if (!isEnable && randomNumber == originalRandomNumber)
            {
                currentObject = objects[Random.Range(0, objects.Length)];
                currentObject.SetActive(true);
                isEnable = true;
                StartCoroutine(DisableObjectWithDelay());
            }

            yield return new WaitForSeconds(checkDelay);
        }
    }

    private IEnumerator DisableObjectWithDelay()
    {
        yield return new WaitForSeconds(disableDelay);
        currentObject.SetActive(false);
        isEnable = false;
    }
}

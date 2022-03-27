using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]
    private float lifeTime; // Время жизни объекта

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}

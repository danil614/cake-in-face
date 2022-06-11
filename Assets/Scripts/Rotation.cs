using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] [Header("Скорость вращения")]
    private float speed;

    private void Update()
    {
        transform.Rotate(0, 0, speed);
    }
}
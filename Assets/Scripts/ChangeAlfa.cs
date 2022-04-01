using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlfa : MonoBehaviour
{
    Color _color;
    float curAlpha = 1.0f;
    public bool isDisappearing = false;
    Renderer _renderer;
    GameObject Hero;

    private List<GameObject> spots = new List<GameObject>();

    [SerializeField]
    private float stepColor;

    [SerializeField]
    private float delayColor;

    //    System.Random rnd = new System.Random(); // Для рандомного смещения пятен
    Animator anim;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
        curAlpha = _renderer.material.color.a;
        isDisappearing = true;
        StartCoroutine(SlowDelete());
    }
    /// <summary>
    /// При столкновении уничтожается объект
    /// </summary>
    /// <param name="collision"></param>

    private void Update()
    {
        if (isDisappearing)
        {
            curAlpha -= Time.deltaTime * stepColor;
            _color.a = curAlpha;
            _renderer.material.color = _color;
            if (curAlpha <= 0.0f)
            {
                Destroy(gameObject);
                isDisappearing = false;
            }
        }
    }
    private IEnumerator SlowDelete()
    {
        Debug.Log("Start SlowDelete");
        yield return new WaitForSeconds(delayColor);
    }
}

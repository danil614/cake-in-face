using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] [Header("Максимальное здоровье")]
    private int maxHealth;

    [SerializeField] [Header("Картинка шкалы")]
    private Image bar;

    private int _currentHealth; // Текущее здоровье

    [SerializeField] [Header("Персонаж")] private GameObject hero;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void ChangeHealth(int damage)
    {
        Debug.Log($"Damage = {damage}");
        _currentHealth += damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Death();
        }
        else if (_currentHealth > maxHealth) _currentHealth = maxHealth;

        bar.fillAmount = (float)_currentHealth / maxHealth;
    }

    private void Death()
    {
        var heroHingeJoints = hero.GetComponentsInChildren<HingeJoint2D>();
        foreach (var joint in heroHingeJoints)
        {
            joint.enabled = false;
        }
    }
}
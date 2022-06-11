using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] [Header("Максимальное здоровье")]
    private int maxHealth;

    [SerializeField] [Header("Картинка шкалы")]
    private Image bar;

    [SerializeField] [Header("Кнопка перезапуска")]
    private GameObject restartButton;

    [SerializeField] [Header("Кнопка меню")]
    private GameObject menuButton;

    [SerializeField] [Header("Повар")] private GameObject cook;

    [SerializeField] [Header("Пряник")] private GameObject gingerbread;

    private int _currentHealth; // Текущее здоровье

    private void Start()
    {
        SetMaxHealth();
    }

    /// <summary>
    ///     Устанавливает полный уровень здоровья.
    /// </summary>
    public void SetMaxHealth()
    {
        _currentHealth = maxHealth;
        // Изменяем шкалу здоровья
        bar.fillAmount = (float)_currentHealth / maxHealth;
    }

    /// <summary>
    ///     Изменяет здоровье персонажа.
    /// </summary>
    public void ChangeHealth(int damage, string currentTag)
    {
        _currentHealth += damage; // Изменяем здоровье

        if (_currentHealth <= 0) // Если у персонажа нет здоровья
        {
            _currentHealth = 0;
            switch (currentTag)
            {
                case "Cook":
                    DisableJoints(); // Отключаем джоинты
                    break;
                case "Gingerbread": // Выключаем пряник
                    DisableGingerbread();
                    break;
            }

            restartButton.SetActive(true); // Активируем кнопку перзапуска
            menuButton.SetActive(false); // Убираем кнопку меню
        }
        // При превышении оставляем максимальный
        else if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }

        // Изменяем шкалу здоровья
        bar.fillAmount = (float)_currentHealth / maxHealth;
    }

    /// <summary>
    ///     Выключает джоинты у повара.
    /// </summary>
    private void DisableJoints()
    {
        var heroHingeJoints = cook.GetComponentsInChildren<HingeJoint2D>();
        foreach (var joint in heroHingeJoints) joint.enabled = false;
    }

    /// <summary>
    ///     Выключает активность пряника.
    /// </summary>
    private void DisableGingerbread()
    {
        gingerbread.SetActive(false);
    }
}
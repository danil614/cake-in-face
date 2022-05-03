using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private MovementCannon movementCannon;
	[SerializeField] private CakeShooting cakeShooting;
	[SerializeField] private Trajectory trajectory;
	[SerializeField] private DragHero dragHero;
	[SerializeField] private Transform turningPoint; // Точка поворота
	[SerializeField] private float pushForce; // Сила нажатия
	[SerializeField] private float startPushForce; // Стартовая сила нажатия

	private bool isClicking = false; // Нажатие на экран
	private Vector2 force; // Сила метания торта
	private float time; // Подсчет времени для роста траектории

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void Update()
	{
		if (!dragHero.IsDragging) // Если не перетаскиваем персонажа
        {
			if (Input.GetMouseButtonDown(0)) // Если кнопка была нажата
			{
				isClicking = true;
				SetStartPushForce(); // Обнуляем время
				trajectory.Show(); // Показываем траекторию
			}

			if (isClicking && Input.GetMouseButtonUp(0)) // Если кнопка была отпущена, но перед этим была нажата
			{
				isClicking = false;
				cakeShooting.Push(force); // Метаем торт
				trajectory.Hide(); // Прячем траекторию
				StartCoroutine(movementCannon.DoCannonKickback()); // Делаем отдачу пушки при стрельбе
			}

			if (isClicking) // Если держим нажатие кнопки
			{
				time += Time.deltaTime; // Считаем время нажатия
				OnClick(); // Обновление траектории при нажатии
			}
		}
	}

	/// <summary>
	/// Устанавливает стартовую силу полета торта в зависимости от расстояния до нажатия.
	/// </summary>
	private void SetStartPushForce()
    {
		Vector2 startPoint = turningPoint.position;
		Vector2 endPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		time = Vector2.Distance(endPoint, startPoint) * startPushForce; // Устанваливаем стартовое время
	}

	/// <summary>
	/// Построение траектории при нажатии.
	/// </summary>
	private void OnClick()
	{
		// Точки начала и конца направляющей линии траектории
		Vector2 startPoint = turningPoint.position;
		Vector2 endPoint = cakeShooting.Position;

		// Направляющий вектор длиной 1
		Vector2 direction = (endPoint - startPoint).normalized;

		// Сила метания торта
		force = time * pushForce * direction;

		// Расставить все точки по траектории полета
		trajectory.UpdateDots(cakeShooting.Position, force);
	}

	/// <summary>
	/// Перезапускает компонент для плавного удаления по времени.
	/// </summary>
	public static void StartSmoothDestroyer(GameObject gameObject, ObjectPool objectPool, float delayColor, float stepColor)
    {
		SmoothDestroyer smoothDestroyer = gameObject.GetComponent<SmoothDestroyer>();
		smoothDestroyer.ObjectPool = objectPool;
		smoothDestroyer.DelayColor = delayColor;
		smoothDestroyer.StepColor = stepColor;
		smoothDestroyer.StartSmoothDestroyer();
	}

	/// <summary>
	/// Останавливает компонент для плавного удаления по времени.
	/// </summary>
	public static void StopSmoothDestroyer(GameObject gameObject)
	{
		SmoothDestroyer smoothDestroyer = gameObject.GetComponent<SmoothDestroyer>();
		smoothDestroyer.ResetSettings(); // Сбрасываем все настройки
	}

	/// <summary>
	/// Перезапускает компонент для активации и деактивации системы частиц.
	/// </summary>
	public static void StartParticleSystemManager(GameObject gameObject, ObjectPool objectPool)
	{
		ParticleSystemManager particleSystemDestroyer = gameObject.GetComponent<ParticleSystemManager>();
		particleSystemDestroyer.ObjectPool = objectPool;
		particleSystemDestroyer.StartParticleSystem();
	}
}

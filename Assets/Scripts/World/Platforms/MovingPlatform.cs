using UnityEngine;

public class MovingObstacle : StaticObstacle
{
    [SerializeField] private float moveSpeed = 3f; // Скорость движения
    [SerializeField] private float moveDistance = 5f; // Расстояние движения вправо и влево

    private bool moveRight = true; // Флаг движения вправо

    void Update()
    {
        // Движение вправо и влево
        Vector3 newPos = transform.position;
        newPos.x += moveRight ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime;
        transform.position = newPos;

        // Проверка достижения края движения, смена направления движения
        if (newPos.x >= moveDistance || newPos.x <= -moveDistance)
        {
            moveRight = !moveRight;
        }
    }
}

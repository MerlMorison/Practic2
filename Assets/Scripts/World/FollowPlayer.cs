using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // Ссылка на целевой объект (игрока)
    public Vector3 offset;   // Смещение камеры относительно цели

    void LateUpdate()
    {
        if (target != null)
        {
            // Позиционирование камеры следом за игроком с учетом смещения
            transform.position = target.position + offset;

            // Камера всегда ориентирована в сторону цели
            transform.LookAt(target);
        }
    }
}

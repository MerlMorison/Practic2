using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // —сылка на игрока
    [SerializeField] private Vector3 offset; // —мещение камеры относительно игрока

    private void Update()
    {
        // ≈сли есть ссылка на игрока, обновл€ем позицию камеры
        if (player != null)
        {
            // ”станавливаем позицию камеры как позицию игрока плюс смещение
            transform.position = player.position + offset;
        }
    }
}

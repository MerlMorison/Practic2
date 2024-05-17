using UnityEngine;

public class MovingObstacle : StaticObstacle
{
    [SerializeField] private float moveSpeed = 3f; // �������� ��������
    [SerializeField] private float moveDistance = 5f; // ���������� �������� ������ � �����

    private bool moveRight = true; // ���� �������� ������

    void Update()
    {
        // �������� ������ � �����
        Vector3 newPos = transform.position;
        newPos.x += moveRight ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime;
        transform.position = newPos;

        // �������� ���������� ���� ��������, ����� ����������� ��������
        if (newPos.x >= moveDistance || newPos.x <= -moveDistance)
        {
            moveRight = !moveRight;
        }
    }
}

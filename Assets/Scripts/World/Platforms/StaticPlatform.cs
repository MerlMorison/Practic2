using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    private UIManager uiManager;
    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 relativeVelocity = collision.relativeVelocity;
            if (relativeVelocity.z > 0)
            {
                DestroyPlayer(collision.gameObject);
            }
        }
    }

    private void DestroyPlayer(GameObject player)
    {
        Destroy(player);
        uiManager.ShowGameOverMenu();
    }
}

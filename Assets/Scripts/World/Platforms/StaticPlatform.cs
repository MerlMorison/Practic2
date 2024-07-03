using InputS;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    private UIManager uiManager;

    private void Start()
    {
        // Поиск UIManager в сцене
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }
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
     
        if (uiManager != null)
        {
            uiManager.ShowGameOverMenu();
        }
        else
        {
            Debug.LogError("UIManager reference is null!");
        }
    }
}

using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string collectEventName = "CollectItem";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalEvents.FireEvent(collectEventName);
            Destroy(gameObject);
        }
    }
}

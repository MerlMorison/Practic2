using TMPro;
using UnityEngine;

public class CollectibleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text itemCountText;
    private int itemCount = 0;

    private void OnEnable()
    {
        GlobalEvents.OnEvent += OnItemCollected;
    }

    private void OnDisable()
    {
        GlobalEvents.OnEvent -= OnItemCollected;
    }

    private void OnItemCollected(string eventName)
    {
        if (eventName == "CollectItem")
        {
            itemCount++;
            itemCountText.text = "Items Collected: " + itemCount;
        }
    }
}

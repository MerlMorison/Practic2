using TMPro;
using UnityEngine;

public class CollectibleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textFields;

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
            foreach (TMP_Text textField in textFields)
            {
                textField.text = " " + itemCount;
            }
        }
    }
}

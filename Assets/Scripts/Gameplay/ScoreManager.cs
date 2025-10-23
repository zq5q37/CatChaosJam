using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int totalItems;
    int collectedItems;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalItems = EnemyManager.CountItems();
        collectedItems = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void increaseScore(int increment)
    {
        collectedItems += increment;
        updateScore(collectedItems);
    }

    void updateScore(int collectedItems)
    {
        GetComponent<TextMeshProUGUI>().text = collectedItems.ToString("D2") + " / " + totalItems.ToString("D2");
    }
}

using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CountItems();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int CountItems()
    {
        GameObject[] KnockableItems = GameObject.FindGameObjectsWithTag("KnockableObjects");
        int itemsCount = KnockableItems.Length;
        Debug.Log("Number of knockable items in the scene: " + itemsCount);
        return itemsCount;
    }
}

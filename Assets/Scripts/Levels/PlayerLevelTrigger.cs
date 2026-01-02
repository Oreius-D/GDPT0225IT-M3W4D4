using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLevelTrigger : MonoBehaviour
{
    [SerializeField] private SortingGroup sortingGroup;

    [SerializeField] private int orderL0 = 7;
    [SerializeField] private int orderL1 = 14;
    [SerializeField] private int orderL2 = 20;

    int currentLevel = 0;

    public int CurrentLevel => currentLevel;

    void Awake()
    {
        if (sortingGroup == null)
            sortingGroup = GetComponent<SortingGroup>();

        ApplySorting();
    }

    public void SetLevel(int level)
    {
        if (currentLevel == level) return;
        currentLevel = level;
        ApplySorting();
    }

    void ApplySorting()
    {
        int order =
            currentLevel == 0 ? orderL0 :
            currentLevel == 1 ? orderL1 :
            orderL2;

        sortingGroup.sortingOrder = order;
    }
}

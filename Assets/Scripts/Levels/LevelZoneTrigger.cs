using UnityEngine;

public class LevelZoneTrigger : MonoBehaviour
{
    private PlayerLevelTrigger level;

    void Awake()
    {
        level = GetComponentInParent<PlayerLevelTrigger>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LevelZone zone = other.GetComponent<LevelZone>();
        if (zone == null || level == null) return;

        // toggle tra previous e next
        if (level.CurrentLevel == zone.previousLvl)
            level.SetLevel(zone.nextLvl);
        else
            level.SetLevel(zone.previousLvl);
    }
}


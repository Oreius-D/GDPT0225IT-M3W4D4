using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife = 10;
    [SerializeField] private bool destroyOnDeath = true;

    [SerializeField] private AudioClip enemyDeathSoundClip;

    private int currentLife;

    private void Awake()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(int amount)
    {
        if (currentLife <= 0) return;

        currentLife -= amount;

        if (currentLife <= 0)
        {
            currentLife = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (currentLife <= 0) return;

        currentLife += amount;
        if (currentLife > maxLife)
            currentLife = maxLife;
    }

    private void Die()
    {
        //PLay death sound
        SoundFXManager.Instance.PlaySoundFXClip(enemyDeathSoundClip, transform, 1f);

        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public int GetMaxLife()
    {
        return maxLife;
    }
}

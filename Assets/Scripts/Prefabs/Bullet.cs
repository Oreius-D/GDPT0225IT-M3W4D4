using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime = 1f;
    [SerializeField] int damage = 1;

    Rigidbody2D rb;
    float _speed;
    Vector2 _dir;

    [SerializeField] private AudioClip shootSoundClip;      //Shoot sound

    public float Speed => _speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); // muore comunque dopo X secondi
    }

    public void Init(Vector2 direction, float speed)
    {
        //PLay shoot sound
        SoundFXManager.Instance.PlaySoundFXClip(shootSoundClip, transform, 1f);

        _dir = direction.normalized;
        _speed = speed;


        // Ruota il proiettile verso la direzione di volo
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        // -90 perché la sprite della freccia punta verso l'alto

        // velocità costante verso il bersaglio
        rb.velocity = _dir * _speed;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Controlliamo il LAYER (non il tag)
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            LifeController life = collision.GetComponent<LifeController>();
            if (life != null)
            {
                life.TakeDamage(damage);
            }
            Destroy(gameObject);           // distruggi il proiettile
        }
    }
}

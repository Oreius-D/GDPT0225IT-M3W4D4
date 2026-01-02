using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;   // prefab dell'arma da dare al player
    [SerializeField] private Transform attachPoint;      // opzionale: punto dove attaccarla (se null, usa player)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Transform parent = attachPoint != null ? attachPoint : other.transform;

        // 1) Rimuovi arma/e già equipaggiata/e (se vuoi UNA sola arma)
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

        // 2) Instanzia e attacca senza ereditare trasformazioni “strane”
        GameObject weaponInstance = Instantiate(weaponPrefab);
        weaponInstance.transform.SetParent(parent, false); // false = usa transform locali puliti
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localRotation = Quaternion.identity;

        // 3) (opzionale) niente "(Clone)"
        weaponInstance.name = weaponPrefab.name;

        Destroy(gameObject);
    }
}
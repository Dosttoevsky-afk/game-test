
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyType keyType;

    public void PickUp(KeyInventory inventory)
    {
        if (inventory == null)
            return;

        inventory.AddKey(keyType);

        Destroy(gameObject);
    }
}

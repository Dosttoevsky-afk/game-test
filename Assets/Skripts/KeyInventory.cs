
using UnityEngine;
using System.Collections.Generic;

public enum KeyType
{
    RedKey,
    BlueKey
}

public class KeyInventory : MonoBehaviour
{
    private HashSet<KeyType> keys = new HashSet<KeyType>();

    public void AddKey(KeyType type)
    {
        if (keys.Add(type))
        {
            Debug.Log("Подобран ключ: " + type);
        }
    }

    public bool HasKey(KeyType type)
    {
        return keys.Contains(type);
    }
}

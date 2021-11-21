using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventoryItem
{
    public List<Key> keys = new List<Key>();

    public bool CheckKey(Key key)
    {
        bool result = false;
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i] == key) result = true;
        }
        return result;
    }

    public void AddKey(Key key)
    {
        keys.Add(key);
    }
}

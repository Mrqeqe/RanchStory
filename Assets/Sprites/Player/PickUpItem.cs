using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFarm.Inventory
{
    public class PickUpItem : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            Item item = col.GetComponent<Item>();
                if (item != null)
                {
                    if (InventoryManager.Instance.AddItemToBag(item.ItemID))
                    {
                        Destroy(item.gameObject);
                    }
                }
            
        }
    }
}


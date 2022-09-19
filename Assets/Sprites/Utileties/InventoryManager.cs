using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品集合")]
        public ItemDataList_SO ItemDataListSo;
        [Header("玩家背包")]
        public InventoryData_SO PlayerBag;
      
        public ItemDetails FindItemDetailsToID(int itemID)
        {
            return ItemDataListSo.ItemDetailsList.Find(i => i.itemID == itemID);
        }
        
      
        public bool AddItemToBag(int ItemID)
        {
            ItemDetails itemDetails = FindItemDetailsToID(ItemID);

           var index = GetBagItemIndex(ItemID);

           AddItemAtIndex(index, 1, ItemID);
            
            Debug.Log($"id{itemDetails.itemID}+Name{itemDetails.itemName}");
            return itemDetails.canPickedup;
        }

        private int GetBagItemIndex(int itemID)
        {
            for (int i = 0; i < PlayerBag.Inventory.Count; i++)
            {
                if (PlayerBag.Inventory[i].ItemID == itemID)
                {
                    return i;
                   
                }
            }

            return -1;
        }
        
        private void AddItemAtIndex(int index, int itemAmount, int itemID)
        {
            if (index < 0 &&  IsBagHaveCapacity() )
            {
                var item = new InventoryItem() {ItemID = itemID, ItemAmount = itemAmount};
                for (int i = 0; i < PlayerBag.Inventory.Count; i++)
                {
                    if (PlayerBag.Inventory[i].ItemID == 0)
                    {
                        PlayerBag.Inventory[i] = item;
                        break;
                    }
                }
            }
            else
            {
                var item = new InventoryItem()
                {
                    ItemAmount = PlayerBag.Inventory[index].ItemAmount + itemAmount, 
                    ItemID = PlayerBag.Inventory[index].ItemID
                };
                PlayerBag.Inventory[index] = item;
            }
        }


        private bool IsBagHaveCapacity()
        {
            for (int i = 0; i < PlayerBag.Inventory.Count; i++)
            {
                if (PlayerBag.Inventory[i].ItemID == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData_SO",menuName = "Invertory/InventoryData_SO")]
public class InventoryData_SO:ScriptableObject
{

    public List<InventoryItem> Inventory;

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerItemFader : MonoBehaviour
{

    private ItemFader[] itemFaders;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
      itemFaders = col.transform.GetComponentsInChildren<ItemFader>();
        if (itemFaders.Length > 0)
        {
            foreach (var Ifader in itemFaders)
            {
                Ifader.FeadOut();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (itemFaders.Length > 0)
        {
            foreach (var Ifader in itemFaders)
            {
                Ifader.FeadIn();
            }
        }
    }
}

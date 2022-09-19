using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyFarm.Inventory
{
    public class Item : MonoBehaviour
    {
        public int ItemID;

        private ItemDetails _itemDetails;
        
        private SpriteRenderer _spriteRenderer;

        private BoxCollider2D _itemBoxCollider2D;
        
        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _itemBoxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            Init(ItemID);
        }


        private void Init(int ID)
        {
            ItemID = ID;
            _itemDetails = InventoryManager.Instance.FindItemDetailsToID(ID);

            _spriteRenderer.sprite = _itemDetails.itemOnWorldSprite;

            _itemBoxCollider2D.size = new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.size.y);
            
            _itemBoxCollider2D.offset = new Vector2(0, _spriteRenderer.sprite.bounds.center.y);
        }
    }
}


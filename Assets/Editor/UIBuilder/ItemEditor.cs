using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Collections.Generic;
using System.Linq;

public class ItemEditor : EditorWindow
{
    [MenuItem("MyTools/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    private ItemDataList_SO   dataBase;
    private List<ItemDetails> itemList;
    private VisualTreeAsset   itemRollTemplate;
    private ListView          itemListView;
    private ScrollView        itemDetailsSection;
    private ItemDetails       selectedItemDetails;
    private Sprite            defultIcon;
    
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIBuilder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        
        itemRollTemplate     = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIBuilder/UIRollItemTemplate.uxml");
        itemListView         = root.Q<VisualElement>("ItemListArea").Q<ListView>("ItemList");
        itemDetailsSection   = root.Q<ScrollView>("ItemDetailsSection");
        defultIcon           = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/M Studio/Art/Items/Icons/icon_Game.png");
        
        LoadDataBase();
        GenterateListView();
        BtnRegisted(root);

    }


    private void BtnRegisted( VisualElement root )
    {
        root.Q<Button>("AddBtn").clicked += () =>
        {
          itemList.Add(   new ItemDetails() {itemID = 1000+ itemList.Count + 1}  ); 
          itemListView.Rebuild();
        };
        root.Q<Button>("DeleteBtn").clicked += () =>
        {
            itemList.Remove(selectedItemDetails);
            itemListView.Rebuild();
            itemDetailsSection.visible = false;
        };

    }
    
    

    private void LoadDataBase()
    {
        var dataArray =   AssetDatabase.FindAssets("ItemDataList_SO");

       if (dataArray.Length > 1)
       {
           var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
           dataBase = AssetDatabase.LoadAssetAtPath<ItemDataList_SO>(path);
           itemList = dataBase.ItemDetailsList;
       }
       
       EditorUtility.SetDirty(dataBase);
    }

    private void GenterateListView()
    {
        Func<VisualElement> makeitem = () => itemRollTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < itemList.Count)
            {
                
                e.Q<VisualElement>("ItemIcon").style.backgroundImage = itemList[i].itemIcon  != null  ? itemList[i].itemIcon.texture : defultIcon.texture;

                e.Q<Label>("ItemName").text = itemList[i].itemName == "" ? "未添加名字" : itemList[i].itemName;
            }
        };

        itemListView.fixedItemHeight = 60;
        itemListView.itemsSource     = itemList;
        itemListView.makeItem        = makeitem;
        itemListView.bindItem        = bindItem;

        itemListView.onSelectionChange += OnSelectitemChange;

        itemDetailsSection.visible = false;
    }

    private void OnSelectitemChange(IEnumerable<object> selectedItem)
    {
        selectedItemDetails = selectedItem.First() as ItemDetails;
        
        GetItemDetails();
    }

    private void GetItemDetails()
    {
        itemDetailsSection.MarkDirtyRepaint();
        
        itemDetailsSection.Q<TextField>("Name").value = selectedItemDetails.itemName;
        itemDetailsSection.Q<TextField>("Name").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.itemName = args.newValue;
            itemListView.Rebuild();
        });
        
        itemDetailsSection.Q<IntegerField>("ItemID").value = selectedItemDetails.itemID;
        itemDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.itemID = args.newValue;
        });
        
        itemDetailsSection.Q<EnumField>("ItemType").Init(selectedItemDetails.itemType);
        itemDetailsSection.Q<EnumField>("ItemType").value = selectedItemDetails.itemType;
        itemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.itemType =(ItemType) args.newValue;
        });
        
       
        itemDetailsSection.Q<ObjectField>("ItemIcon").value = selectedItemDetails.itemIcon;
        itemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.itemIcon =(Sprite) args.newValue;

            itemDetailsSection.Q<VisualElement>("Icon").style.backgroundImage =
                args.newValue == null ? defultIcon.texture : ((Sprite) args.newValue).texture;
            
            itemListView.Rebuild();
        });

        itemDetailsSection.Q<ObjectField>("ItemOnWorldSprite").value = selectedItemDetails.itemOnWorldSprite;
        itemDetailsSection.Q<ObjectField>("ItemOnWorldSprite").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.itemOnWorldSprite = (Sprite) args.newValue;
        });
        
        itemDetailsSection.Q<TextField>("ItemDescription").value = selectedItemDetails.itemDescription;
        itemDetailsSection.Q<TextField>("ItemDescription").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.itemDescription =  args.newValue;
        });
        
        itemDetailsSection.Q<IntegerField>("UseRadius").value = selectedItemDetails.itemUseRadius;
        itemDetailsSection.Q<IntegerField>("UseRadius").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.itemUseRadius =  args.newValue;
        });
        
        itemDetailsSection.Q<Toggle>("CanPickUp").value = selectedItemDetails.canPickedup;
        itemDetailsSection.Q<Toggle>("CanPickUp").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.canPickedup =  args.newValue;
        });
        
        itemDetailsSection.Q<Toggle>("CanDropped").value = selectedItemDetails.canDropped;
        itemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.canDropped =  args.newValue;
        });
        
        itemDetailsSection.Q<Toggle>("CanCarried").value = selectedItemDetails.canCarried;
        itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.canCarried =  args.newValue;
        }); 
        
        itemDetailsSection.Q<IntegerField>("Price").value = selectedItemDetails.itemPrice;
        itemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.itemPrice =  args.newValue;
        });

        itemDetailsSection.Q<Slider>("SellPercentage").value = selectedItemDetails.sellPercentage;
        itemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(args =>
        {
            selectedItemDetails.sellPercentage = args.newValue;
        });
        
        
        itemDetailsSection.visible = true;
    }
    
}
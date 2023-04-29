using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private PlayerManager player;
    private List<bool> inventory;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject unequipButton;
    private Item itemData;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        itemData = gameObject.GetComponent<ItemData>().item;
        
        List<Item> itemList = player.PublicCheckEquipItem();
    
        if(itemList != null)
        {
            foreach(Item item in player.PublicCheckEquipItem())
            {
                if(itemData == item)
                {
                    equipButton.SetActive(false);
                    unequipButton.SetActive(true);
                }
            }
        }
    }

    private void ItemEquip(Item item)
    {
        player.PublicEquipItem(item);
    }

    private void ItemUnequip(Item item)
    {
        player.PublicUnequipItem(item);
    }

    public void PublicItemEquip(ItemData itemData)
    {
        ItemEquip(itemData.item);
    }

    public void PublicItemUnequip(ItemData itemData)
    {
        ItemUnequip(itemData.item);
    }
}

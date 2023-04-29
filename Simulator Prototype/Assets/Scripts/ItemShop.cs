using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    private PlayerManager player;

    private void Start()
    {
       player = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void ItemBuy(Item item)
    {
        if(player.coins >= item.buy)
        {
            player.PublicGetItem(item);
        }
    }

    private void ItemSell(Item item)
    {
        player.PublicRemoveItem(item);
    }

    public void PublicItemBuy(ItemData itemData)
    {
        ItemBuy(itemData.item);
    }

    public void PublicItemSell(ItemData itemData)
    {
        ItemSell(itemData.item);
    }
}

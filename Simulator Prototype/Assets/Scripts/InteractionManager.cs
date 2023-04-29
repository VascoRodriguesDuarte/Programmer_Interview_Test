using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject template;
    [SerializeField] private GameObject textBox;

    private bool isShop;

    public TMP_Text mainText;

    private void InteractionState(bool value, bool inShop)
    {
        if(value)
        {
            textBox.gameObject.SetActive(true);
            player.PublicMovementWhileInteracting(value, inShop, this);
        }
        else
        {
            StopInteraction();
        }
    }

    private void StopInteraction()
    {
        mainText.text = null;
        textBox.gameObject.SetActive(false);
        if(isShop)
        {
            foreach (Transform child in shopUI.GetComponent<Transform>())
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void PublicInteractionState(bool value, bool inShop)
    {
        InteractionState(value, inShop);
    }

    public void PublicPrepareShop(Interact shop)
    {
        PrepareShop(shop);
    }

    private void PrepareShop(Interact shop)
    {
        isShop = true;
        foreach(Item item in shop.GetItems())
        {
            GameObject instance = Instantiate(template, shopUI.GetComponent<Transform>()); 
            instance.GetComponent<ItemData>().item = item;
            instance.GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = item.image;
            instance.GetComponent<Transform>().GetChild(1).GetComponent<TMP_Text>().text = item.name;
            instance.GetComponent<Transform>().GetChild(2).GetComponent<TMP_Text>().text = item.buy + " coins";
        }
    }
}

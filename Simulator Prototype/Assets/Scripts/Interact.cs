using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    [SerializeField] private InteractType interactType;
    [SerializeField] private string text;
    [SerializeField] private Item[] items;

    private InteractionManager manager;
    private TMP_Text textMesh;

    private void Start()
    {
        manager = this.gameObject.GetComponentInParent<InteractionManager>();
        textMesh = manager.mainText;
    }

    private void InteractMethod()
    {
        if(interactType == InteractType.Text)
        {
            TextInteract();
        }
        else if(interactType == InteractType.Shop)
        {
            ShopOpen();
        }
    }

    private void TextInteract()
    {
        textMesh.text = text;
        manager.PublicInteractionState(true, false);
    }

    private void ShopOpen()
    {
        textMesh.text = text;
        manager.PublicInteractionState(true, true);
        manager.PublicPrepareShop(this);
    }

    public void PublicInteract()
    {
        InteractMethod();
    }

    public Item[] GetItems()
    {
        return items;
    }
}

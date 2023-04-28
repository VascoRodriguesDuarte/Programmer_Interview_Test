using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private InteractCheck interact;
    [SerializeField] private InteractionManager interactionManager;
    [SerializeField] private List<Item> inventory;
    [SerializeField] private TMP_Text coinUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject template;
    [SerializeField] private GameObject inventoryBox;
    [SerializeField] private Transform[] hatPos;
    [SerializeField] private Transform[] shoesPos;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool canMove = true;
    private bool isShop = false;
    private List<Item> equipedItems;

    public PlayerInput p1;
    public int coins;
    public bool isInteracting = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        coinUI.text = "Coins: " + coins;
        equipedItems = new List<Item>();
    }

    private void LateUpdate()
    {
        cameraObject.position = new Vector3(transform.position.x, transform.position.y, cameraObject.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    private void Move(Vector2 value)
    {
        moveDirection = value;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;

            angle = Mathf.Round(angle / 90f) * 90f;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Interact()
    {
        if(canMove)
        {
            interact.ActivateInteract();
        }
        else if(!isShop)
        {
            interactionManager.PublicInteractionState(false, false);
            MovementWhileInteracting(canMove);
            isInteracting = false;
        }
    }

    private void GetItem(Item item)
    {
        coins -= item.buy;

        coinUI.text = "Coins: " + coins;

        inventory.Add(item);
    }

    private void RemoveItem(Item item)
    {
        foreach(Item itemInInventory in inventory)
        {
            if(itemInInventory.name == item.name)
            {
                coins += item.sell;

                coinUI.text = "Coins: " + coins;

                inventory.Remove(itemInInventory);
                break;
            }
        }
    }

    private void EquipItem(Item item)
    {
        if(item.itemType == "Hat")
        {
            foreach(Transform singleObject in hatPos)
            {
                if(singleObject.childCount > 0)
                {
                    foreach(Transform child in singleObject)
                    {
                        equipedItems.RemoveAll(item => item.itemType == "Hat");
                        Destroy(child.gameObject);
                    }
                }
                equipedItems.Add(item);
                Instantiate(item.prefab, singleObject); 
            }
        }
        else if(item.itemType == "Shoes")
        {
            foreach(Transform singleObject in shoesPos)
            {
                if(singleObject.childCount > 0)
                {
                    foreach(Transform child in singleObject)
                    {
                        equipedItems.RemoveAll(item => item.itemType == "Shoes");
                        Destroy(child.gameObject);
                    }
                }
                equipedItems.Add(item);
                Instantiate(item.prefab, singleObject); 
            }
        }
    }

    private void UnequipItem(Item item)
    {
       if(item.itemType == "Hat")
        {
            foreach(Transform singleObject in hatPos)
            {
                foreach(Transform child in singleObject)
                {
                    equipedItems.Remove(item);
                    Destroy(child.gameObject);
                }
            }
        }
        else if(item.itemType == "Shoes")
        {
            foreach(Transform singleObject in shoesPos)
            {
                foreach(Transform child in singleObject)
                {
                    equipedItems.Remove(item);
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private void MovementWhileInteracting(bool value)
    {
        canMove = !value;
    }

    private void CloseInventory()
    {
        canMove = true;

        inventoryBox.gameObject.SetActive(false);
        
        foreach (Transform child in inventoryUI.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
    }

    private void EnterInventory()
    {
        moveDirection = Vector2.zero;

        canMove = false;

        inventoryBox.gameObject.SetActive(true);
        
        foreach(Item item in inventory)
        {
            GameObject instance = Instantiate(template, inventoryUI.GetComponent<Transform>()); 
            instance.GetComponent<ItemData>().item = item;
            instance.GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = item.image;
            instance.GetComponent<Transform>().GetChild(1).GetComponent<TMP_Text>().text = item.name;
        }
    }

    private List<Item> CheckEquipItem()
    {
        foreach(Item item in equipedItems)
        {
            Debug.Log(item.name);
        }
        return equipedItems;
    }

/*--------------------------------------------------------------------------------------------------*/

    public void PublicMove(InputAction.CallbackContext context)
    {
        if(canMove)
        {
            Move(context.ReadValue<Vector2>());
        }
    }

    public void PublicInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Interact();
        }
    }

    public void PublicMovementWhileInteracting(bool value, bool shop, InteractionManager intManager)
    {
        interactionManager = intManager;
        MovementWhileInteracting(value);
        if(shop)
        {
            isShop = shop;
        }
    }

    public void EndShop()
    {
        isInteracting = false;
        interactionManager.PublicInteractionState(false, false);
        MovementWhileInteracting(canMove);
        isShop = false;
    }

    public void PublicGetItem(Item item)
    {
        GetItem(item);
    }

    public void PublicRemoveItem(Item item)
    {
        RemoveItem(item);
    }

    public void PublicEquipItem(Item item)
    {
        EquipItem(item);
    }

    public void PublicUnequipItem(Item item)
    {
        UnequipItem(item);
    }

    public List<Item> PublicCheckEquipItem()
    {
        return CheckEquipItem();
    }

    public void PublicInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && !isInteracting)
        {
            if(inventoryBox.gameObject.activeSelf)
            {
                CloseInventory();
            }
            else
            {
                EnterInventory();
            }
        }
    }
}

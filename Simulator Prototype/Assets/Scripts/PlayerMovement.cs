using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private InteractCheck interact;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public PlayerInput p1;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        cameraObject.position = new Vector3(transform.position.x, transform.position.y, cameraObject.position.z);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    public void PublicMove(InputAction.CallbackContext context)
    {
        Move(context.ReadValue<Vector2>());
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

    public void PublicInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Interact();
        }
    }

    private void Interact()
    {
        interact.ActivateInteract();
    }
}

using UnityEngine;

public class DraggingObject : MonoBehaviour
{
    Vector2 offset;

    [SerializeField] private LayerMask DragableObjectLayer;

    Camera cam;
    Rigidbody2D targetRB;

    bool isDragging;

    [SerializeField] private float DragSpeed;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();



        if (isDragging)
        {
            HandleDrag();
        }



    }

    private void HandleDrag()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 targetPos = mousePos + offset;

        targetRB.linearVelocity = (targetPos - (Vector2)targetRB.transform.position) * DragSpeed;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("I clicked");

            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1000, DragableObjectLayer);

            if(hit.collider == null)
            {
                Debug.Log("What are you aiming for");
            }

            if (hit.collider.TryGetComponent(out Rigidbody2D rb))
            {
                Debug.Log("Ýs it the target");
                offset = (Vector2)rb.transform.position - mousePos;

                targetRB = rb;
                isDragging = true;
            }

        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isDragging = false;
        }
        

    }


}




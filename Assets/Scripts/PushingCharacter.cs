using UnityEngine;

public class PushingCharacter : MonoBehaviour
{
    [SerializeField] private float addingForce = 500;

    private Rigidbody selectedRigidbody;
    private Camera targetCamera;
    private Vector3 targetPos;
    private Vector3 startPos;
    private float selectionDistance;
    private int pushingMask;

    private void Start()
    {
        targetCamera = GetComponent<Camera>();

        pushingMask = 1 << 8;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedRigidbody = GetRigidbodyFromMouseClick();
        }
        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
        {
            selectedRigidbody = null;
        }
    }

    private void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            Vector3 mousePositionOffset = Vector3.Scale(targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - targetPos, new Vector3(1, 1, 0));
            selectedRigidbody.velocity = (startPos + mousePositionOffset - selectedRigidbody.transform.position) * addingForce * Time.deltaTime;
        }
    }

    private Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, pushingMask);
        if (hit)
        {
            Rigidbody hitRigidbody = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            if (hitRigidbody)
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                targetPos = Vector3.Scale(targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)), new Vector3(1, 1, 0));
                startPos = Vector3.Scale(hitInfo.collider.transform.position, new Vector3(1, 1, 0));
                return hitRigidbody;
            }
        }

        return null;
    }
}

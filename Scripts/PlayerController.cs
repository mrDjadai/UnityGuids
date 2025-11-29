using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public bool IsAlive = true;
    [SerializeField] private float speed;
    [SerializeField] private float distanceFromFootOffset;
    [SerializeField] private float sensability = 3;

    [SerializeField] private float minAngler;
    [SerializeField] private float maxAngler;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;

    private float rotationX;
    private float xInputR;
    private float yInputR;

    private Vector3 gravitySpeed;

    private CharacterController characterController;
    private Transform _transform;
    private float xInput;
    private float yInput;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void GetDirectionR()
    {
        xInputR = Input.GetAxis("Mouse X") * sensability;
        yInputR = Input.GetAxis("Mouse Y") * sensability;
    }

    private void Update()
    {
        GetDirectionR();
        rotationX = Mathf.Clamp(rotationX - yInputR, minAngler, maxAngler);
        playerTransform.Rotate(playerTransform.up * xInputR);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void FixedUpdate()
    {
        if (characterController.enabled == false)
        {
            return;
        }
        GravityMove();
        GetDirection();

        Vector3 moveDirection = _transform.forward * yInput + _transform.right * xInput;
        if (moveDirection.magnitude > 1)
        {
            moveDirection = moveDirection.normalized;
        }
        characterController.Move(moveDirection * speed * Time.deltaTime);



    }

    private void GravityMove()
    {
        if (characterController.isGrounded == false)
        {
            gravitySpeed += Physics.gravity * Time.deltaTime;
        }
        else
        {
            gravitySpeed = Vector3.zero;
        }
        characterController.Move(gravitySpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        float floorDistanceFromFoot = characterController.stepOffset + distanceFromFootOffset;

        if (characterController.isGrounded)
        {
            return true;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, floorDistanceFromFoot))
        {
            return true;
        }

        return false;
    }

    private void GetDirection()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up * distanceFromFootOffset);
    }
}
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _velocity;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] float _rotationSpeed = 0.5f;

    Camera mainCamera;
    Transform objToFaced;
    Vector3 movement = new();
    Vector3 verticalGravity;
    Vector3 moveTowardPos;

    bool isFacingTarget = false;
    bool isMoveTowardsActivated = false;
    public bool IsActivated { get; private set; } = true;

    private void Start()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        if (isMoveTowardsActivated)
        {
            MoveTowardsAnObj();
        }
        else
        {
            _characterController.SimpleMove((movement + verticalGravity) * Time.deltaTime);
        }


        if (isFacingTarget && objToFaced != null)
        {
            Vector3 direction = objToFaced.position - transform.position;
            direction.Normalize();
            direction.y = 0f;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);
        }

        if (!IsActivated) return;

        CalculateMovementAndRotationFromInput();

    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        
    }


    public void SetMoveTowardDestination(Vector3 pos)
    {
        moveTowardPos = pos;
        transform.position = moveTowardPos;
        isMoveTowardsActivated = true;
    }

    private void MoveTowardsAnObj()
    {
        //Vector3 dir = moveTowardPos - transform.position;
        //dir.y = 0f;
        //dir.Normalize();

        //_characterController.SimpleMove(dir * _velocity * 2 * Time.deltaTime);


        //Vector3 curPos = transform.position;
        //curPos.y = 0f;

        //Vector3 destPos = moveTowardPos;
        //destPos.y = 0f;

        //if(Vector3.Distance(curPos, destPos) <= 0.2f)
        //{
        //    isMoveTowardsActivated = false;
        //}

        transform.position = moveTowardPos;
        isMoveTowardsActivated = false;

    }

    private void CalculateMovementAndRotationFromInput()
    {
        float horizontalValue = _inputReader.MovementValue.x;
        float verticalValue =_inputReader.MovementValue.y;

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();


        movement.x = horizontalValue;
        movement.y = 0f;
        movement.z = verticalValue;

        movement = verticalValue * forward + horizontalValue * right;

        movement *= _velocity;

        if(_inputReader.MovementValue == Vector2.zero) { return; }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), _rotationSpeed * Time.deltaTime);
    }

    public void ActivateMovement()
    {
        IsActivated = true;
    }

    public void DisableMovement()
    {
        IsActivated = false;
    }

    public void FaceTarget(Transform transformObj)
    {
        isFacingTarget = true;
        objToFaced = transformObj;      
    }

    public void StopFacingTarget()
    {
        isFacingTarget = false;
        objToFaced = null;
    }
}

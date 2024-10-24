using Cinemachine;
using FishNet.Object;
using UnityEngine;

public sealed class Player : NetworkBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private float moveTime;
    private Animator playerAnimator;

    private Vector2 lookDirection;
    private Vector2 currentInput;
    private Rigidbody2D rigidbody2d;

    [SerializeField]
    private LayerMask solidObjectsLayer;

    [SerializeField]
    private LayerMask NPC;

    [SerializeField]
    private float rotationIntensity;

    private void Awake()
    {
        
        SetValues();

    }

    private void SetValues()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        rigidbody2d = GetComponentInChildren<Rigidbody2D>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!IsOwner) return;
        virtualCamera.gameObject.SetActive(true);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
    }

    // Update is called once per frame
    void Update()
    {
        //move the player transform
        Vector2 move;
        GetFaceDirAndTransform(out move);

        //rotate the torch with the player using "face dir"
        RotateTorch();

        //animate the player moving
        AnimateMove(move);

        //player interactions
        Interact();
    }

    /// <summary>
    /// Check if player is interacting with something
    /// </summary>
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, NPC);
            if (hit.collider != null)
            {
                NPC001 character = hit.collider.GetComponent<NPC001>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }

    private void AnimateMove(Vector2 move)
    {
        playerAnimator.SetFloat("Look X", lookDirection.x);
        playerAnimator.SetFloat("Look Y", lookDirection.y);
        playerAnimator.SetFloat("Speed", move.magnitude);
    }

    private void GetFaceDirAndTransform(out Vector2 move)
    {
        move = Vector2.zero;
        if (!base.IsOwner) return;
        //=======Ruby Movement======
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        currentInput = move;
    }

    void FixedUpdate()
    {
        //put the 'get transform' to use
        SetTransform();

    }

    /// <summary>
    /// Set a transform
    /// </summary>
    private void SetTransform()
    {
        if (!IsOwner) return;
        Vector2 position = rigidbody2d.position;

        position = position + currentInput * moveTime * Time.deltaTime;

        if (isWalkable(position))
            rigidbody2d.MovePosition(position);
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer)!= null)
        {
            return false;
        }
        return true;
    }


    /// <summary>
    /// will rotate your torch (in bri-ish accent)
    /// </summary>
    private void RotateTorch()
    {
        if(lookDirection != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, lookDirection);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationIntensity * Time.deltaTime);

            transform.GetChild(1).rotation = rotation;
        }
    }
}

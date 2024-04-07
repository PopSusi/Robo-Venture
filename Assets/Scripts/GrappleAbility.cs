using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GrappleAbility : Ability
{

    Camera cam;
	[field: Header("Ability Sub-Class")]
    [SerializeField] [Tooltip("Distance to begin grappling.")] float grappleDistance;
    [SerializeField] [Tooltip("Radius for distance check.")] float detectDistance;
    Transform detectedPoint;
    Transform grappleTarget;
    [SerializeField] [Tooltip("Debug bool - Is character moving towards grapplepoint.")] bool isGrappling;
    [SerializeField] [Tooltip("Speed to move at.")] float grappleSpeed;
    [SerializeField] [Tooltip("Layer to search for points. Default is 'Grapple'.")] LayerMask grappleLayers;
    [SerializeField] [Tooltip("Minimum Size of UI. Default is 205.")] float minSize = 205;
    [SerializeField] [Tooltip("Maximum Size of UI. Default is 305.")] float maxSize = 350;
    private float distanceToGrapple;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main;
        player = this.gameObject;
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<ThirdPersonPlayerController>();
        input.actions["Grapple"].performed += OnGrapple;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrappling)
        {
            return;
        }
        //RaycastHit hit;
        if (unlocked)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectDistance, grappleLayers);
            if (colliders.Length > 0)
            {
                detectedPoint = colliders[0].transform;
                distanceToGrapple = Vector3.Distance(detectedPoint.position, this.transform.position);
                float mod = ClampSize(distanceToGrapple);
                Debug.Log(mod);
                detectedPoint.gameObject.GetComponent<GrapplePoint>().UpdateAnchors(mod);
            }
        }
        else
        {
            detectedPoint = null;
        }
        print(detectedPoint.gameObject.tag);
    }
    private void FixedUpdate()
    {
        if (!isGrappling)
        {
            return;
        }
        distanceToGrapple = Vector3.Distance(detectedPoint.position, this.transform.position);
        this.transform.position = Vector3.MoveTowards(this.transform.position, grappleTarget.position, grappleSpeed * Time.fixedDeltaTime);
       
        if (Vector3.Distance(grappleTarget.position, this.transform.position) <= 0.1)
        {
            GetComponent<Animator>().SetBool("Grapple", false);
            isGrappling = false;
        }
        else
        {
            this.transform.rotation = Quaternion.LookRotation(new Vector3((grappleTarget.position - this.transform.position).x, 0, (grappleTarget.position - this.transform.position).z), Vector3.up);
        }
    }
        

    void OnGrapple(InputAction.CallbackContext context)
    {
        if (detectedPoint == null) { return; }
        grappleTarget = detectedPoint;
        if (unlocked)
        {
            isGrappling = true;
            anim.SetBool("Grapple", true);
            StartCooldown();
            CooldownManager.CDMInstance.CooldownMaskStart(mySprite, cooldown);
        }
    }
    float ClampSize(float distance)
    {
        
        float clamped = distanceToGrapple / detectDistance;
        clamped *= maxSize;
        return Mathf.Clamp(clamped, minSize, maxSize);
    }
    void OnDrawGizmos()
    {
        //VISUALLY SEE DETECTED
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, detectDistance);
    }
}   

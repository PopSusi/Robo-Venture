using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GrappleAbility : Ability
{

    Camera cam;
	[field: Header("Ability Sub-Class")]
    [SerializeField] [Tooltip("Distance to begin grappling.")] float grappleDistance;
    [SerializeField] [Tooltip("Radius for distance check.")] float detectDistance;
    GameObject detectedPoint;
    GrapplePoint currentPoint;
    GrapplePoint pastPoint;
    Vector3 grappleTarget;
    [SerializeField] [Tooltip("Debug bool - Is character moving towards grapplepoint.")] bool isGrappling;
    [SerializeField] [Tooltip("Speed to move at.")] float grappleSpeed;
    [SerializeField] [Tooltip("Layer to search for points. Default is 'Grapple'.")] LayerMask grappleLayers;
    [SerializeField] [Tooltip("Minimum Size of UI. Default is 205.")] float minSize = 205;
    [SerializeField] [Tooltip("Maximum Size of UI. Default is 305.")] float maxSize = 350;
    [Tooltip("Distance from Target before stopping. Default is .2.")] float tolerance;
    private float distanceToGrapple;
    [SerializeField]
    GameObject grappleUIPrefab;
    GameObject grappleUI;
    Canvas canvas;
    private void Awake()
    {
        canvas= (Canvas)Canvas.FindFirstObjectByType(typeof(Canvas));
        
        grappleUI = Instantiate(grappleUIPrefab, canvas.transform);
        grappleUI.SetActive(false);
    }
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
            grappleUI.SetActive(false);
            return;
        }
        //RaycastHit hit;
        if (unlocked)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectDistance, grappleLayers);
            if (colliders.Length > 0)
            {
                detectedPoint = CheckForSelected(colliders);
                distanceToGrapple = Vector3.Distance(detectedPoint.transform.position, this.transform.position);
                float mod = ClampSize(distanceToGrapple);
                Debug.Log(mod);
                //detectedPoint.gameObject.GetComponent<GrapplePoint>().UpdateAnchors(mod);
                if (!grappleUI.activeSelf)
                {
                    grappleUI.SetActive(true);
                }
                grappleUI.transform.position = cam.WorldToScreenPoint(detectedPoint.transform.position);

            }
            else
            {
                grappleUI.SetActive(false);
            }
            print(detectedPoint.gameObject.tag);
        }
        else
        {
            detectedPoint = null;
            grappleUI.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (!isGrappling)
        {
            return;
        }
        distanceToGrapple = Vector3.Distance(grappleTarget, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, grappleTarget, grappleSpeed * Time.fixedDeltaTime);
        if (Vector3.Distance(grappleTarget, transform.position) <= tolerance)
        {
            GetComponent<Animator>().SetBool("Grapple", false);
            isGrappling = false;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(new Vector3((grappleTarget - transform.position).x, 0, (grappleTarget - this.transform.position).z), Vector3.up);
        }
    }
        

    void OnGrapple(InputAction.CallbackContext context)
    {
        if (detectedPoint == null) { return; }
        if (unlocked & canAbility)
        {
            currentPoint = detectedPoint.GetComponent<GrapplePoint>();
            grappleTarget = currentPoint.transform.GetChild(0).position; 
            currentPoint.Deactivate();
            pastPoint = currentPoint;
            StartCoroutine("Reenable");
            isGrappling = true;
            anim.SetBool("Grapple", true);
            StartCooldown();
            CooldownManager.CDMInstance.CooldownMaskStart(mySprite, cooldown);
        }
    }
    IEnumerator Reenable()
    {
        WaitForSeconds wait = new WaitForSeconds(cooldown);
        yield return wait;
        if(distanceToGrapple < detectDistance)
        {
            pastPoint.Activate();
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
    private GameObject CheckForSelected(Collider[] possiblePoints)
    {
        int i = 0;
        float dotMax = 0f;
        int chosen = 0;
        foreach (Collider c in possiblePoints)
        {
            Vector3 targetDirection = (possiblePoints[i].transform.position - cam.transform.position).normalized;
            if(dotMax < Vector3.Dot(cam.transform.forward, targetDirection))
            {
                dotMax = Vector3.Dot(cam.transform.forward.normalized, targetDirection);
                chosen = i;
            }
            i++;
        }
        Debug.Log($"{i} {dotMax} {possiblePoints[chosen].name}");
        return possiblePoints[chosen].gameObject;
    }
}   

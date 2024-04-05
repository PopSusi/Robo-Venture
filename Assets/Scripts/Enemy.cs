using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour, Damageable
{
    //Damageable Variables
    public float HP { get; set; } = 6f;
    public float damageDelay { get; set; } = 1.5f;
    public bool vulnerable { get; set; } = true;
    
    //Components
    private AudioSource mainAudio;
    [Tooltip("AudioSource for damage.")]
    [SerializeField]
    private AudioSource scndryAudio;
    [Tooltip("Filled on Spawn - Controlling area Trigger.")] public CombatTriggers myTrigger;
    private Vector3 triggPos;
    private float triggSize;
    [SerializeField] 
    private AudioClip hitSFX, hitVariantSFX, deathSFX;
    [Tooltip("Which layer to search for Trigger - 'Triggers'")]
    [SerializeField] private LayerMask layermask;
    private NavMeshAgent agent;
    [Tooltip("If bug is in combat - Serialized for debugging.")]
    [SerializeField] private bool combat = false;
    [Tooltip("Radius to declare that enemy is close enough to target.")]
    [SerializeField] private float stopDistance;
    [Tooltip("Current Object.")]
    [SerializeField] private GameObject playerObj;
    private Vector3 currTarget;
    [Tooltip("Serialized for debugging.")]
    [SerializeField] private bool canPunch;
    [Tooltip("Cooldown before punching.")]
    [SerializeField] private float cooldownPunch;
    [Tooltip("Prefab for damage area.")]
    [SerializeField] private GameObject hitBox;
    [Tooltip("Layer for finding Y-Level of Terrain - Default is 'Terrain'.")]
    [SerializeField] private LayerMask terrainLayer;
    [Tooltip("How far to overshoot actual target.")]
    [SerializeField] private float dirMod = 5f;
    public void Awake(){
        mainAudio = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        Collider[] tempHold = Physics.OverlapBox(transform.position, transform.localScale/2, Quaternion.identity, layermask);
        myTrigger = tempHold[0].gameObject.GetComponent<CombatTriggers>();
        triggPos = myTrigger.transform.position;
        currTarget = CalculateDestinationRandom();
        triggSize = myTrigger.transform.localScale.x;
        hitBox = transform.GetChild(0).gameObject;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Recieved");
        if (vulnerable)
        {
            Debug.Log("Calculating");
            HP -= damage;
            if(HP > 0){ //Not at zero
                Debug.Log("Damaged " + HP);
                vulnerable = false;
                scndryAudio.clip = hitSFX;
                scndryAudio.Play();
                StartCoroutine("DamageDelay");
            } else {
                GetComponent<Collider>().enabled = false;
                Debug.Log("Dead");
                StartCoroutine("DeathAudioDelay");
            }
        }
    }
    public void Die(){
        Debug.Log("Dying");
        Destroy(gameObject);
    }
    private IEnumerator DeathAudioDelay(){
        scndryAudio.clip = deathSFX;
        scndryAudio.Play();
        WaitForSeconds wait = new WaitForSeconds(deathSFX.length);
        yield return wait;
        Die();
    }
    private void FixedUpdate()
    {
        if (combat)
        {
            OverShoot();
        }
        agent.destination = currTarget;
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(currTarget.x, currTarget.z)) < stopDistance && !combat)
        {
            //Debug.Log("Close Enough");
            currTarget = CalculateDestinationRandom();
        } else if(Vector3.Distance(transform.position, triggPos) > triggSize)
        {
            //Debug.Log("Too far");
            currTarget = triggPos;
        }
        if (playerObj != null)
        {
            hitBox.SetActive(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(playerObj.transform.position.x, playerObj.transform.position.z)) < .2);
        }
    }
    private Vector3 CalculateDestinationRandom()
    {
        RaycastHit hit;
        Vector2 tempRand = Random.insideUnitSphere;
        Vector3 tempTarget = new Vector3(tempRand.x * Random.Range(.5f, triggSize * .66f), 0, tempRand.y * Random.Range(.5f, triggSize * .66f)) + triggPos;
        if (Physics.Raycast(tempTarget, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, terrainLayer))
        {
            tempTarget.y = hit.point.y;
        } else if (Physics.Raycast(tempTarget, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, terrainLayer))
        {
            tempTarget.y = hit.point.y;
        }
        return tempTarget;
        //Debug.Log($"Calculated {currTarget} at {Time.timeSinceLevelLoad}");
    }
    public void StartCombat(GameObject player)
    {
        combat= true;
        //Debug.Log("Starting Combat");
        StopAllCoroutines();
        playerObj = player;
        currTarget = player.transform.position;
    }
    public void EndCombat()
    {
        combat= false;
        currTarget = CalculateDestinationRandom();
    }
    IEnumerator DamageDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(damageDelay);
        yield return wait;
        vulnerable = true;
    }
    void OnPunch(InputAction.CallbackContext context)
    {
        if (canPunch)
        {
            canPunch = false;
            hitBox.SetActive(true);
            StartCoroutine("PunchDelay");
        }
    }
    IEnumerator PunchDuration()
    {
        yield return new WaitForSeconds(.2f);
        hitBox.SetActive(false);
    }
    IEnumerator PunchDelay()
    {
        yield return new WaitForSeconds(cooldownPunch);
        canPunch = true;
    }
    private void OverShoot()
    {
        Vector3 direction = (playerObj.transform.position - transform.TransformPoint(Vector3.zero)).normalized;
        //direction.x = direction.x * Random.Range(1, 1.5f);
        //direction.z = direction.z * Random.Range(1, 1.5f);
        Debug.DrawLine(transform.position, transform.position + direction * 5, Color.red, Mathf.Infinity);
        currTarget = transform.position + direction * dirMod;
        /*Vector3 direction = (playerObj.transform.position - transform.position).normalized;
        currTarget = (direction * Random.Range(1, 2)) + currTarget;*/
    }

}

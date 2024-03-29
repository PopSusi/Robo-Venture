using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, Damageable
{
    //Damageable Variables
    public float HP { get; set; } = 6f;
    public float damageDelay { get; set; } = 1.5f;
    public bool vulnerable { get; set; } = true;
    
    //Components
    private AudioSource mainAudio;
    [SerializeField]
    private AudioSource scndryAudio;
    public CombatTriggers myTrigger;
    private Vector3 triggPos, currTarget;
    private float triggSize;
    [SerializeField] private AudioClip hitSFX, hitVariantSFX, deathSFX;
    [SerializeField] private LayerMask layermask;
    private NavMeshAgent agent;
    private bool combat = false;
    public void Start(){
        mainAudio = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        Collider[] tempHold = Physics.OverlapBox(transform.position, transform.localScale/2, Quaternion.identity, layermask);
        myTrigger = tempHold[0].gameObject.GetComponent<CombatTriggers>();
        triggPos = myTrigger.transform.position;
        CalculateDestinationRandom();
        triggSize = myTrigger.transform.localScale.x;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Recieved");
        if (vulnerable)
        {
            Debug.Log("Calculating");
            HP -= damage;
            if(HP > 0){ //Not at zero
                Debug.Log("Damaged");
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
        Debug.Log(myTrigger.CheckEnemies(gameObject));
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
        if(Vector3.Distance(transform.position, currTarget) < .3 && !combat)
        {
            //Debug.Log("Close Enough");
            CalculateDestinationRandom();
        } else if(Vector3.Distance(transform.position, triggPos) > triggSize)
        {
            Debug.Log("Too far");
            CalculateDestinationRandom();
        }
    }
    private void CalculateDestinationRandom()
    {
        Vector2 tempRand = Random.insideUnitSphere;
        currTarget = new Vector3(tempRand.x * Random.Range(.5f, triggSize * .66f), transform.position.y, tempRand.y * Random.Range(.5f, triggSize * .66f)) + triggPos;
        agent.destination = currTarget;
        //Debug.Log($"Calculated {currTarget} at {Time.timeSinceLevelLoad}");
    }
    public void StartCombat()
    {
        combat= true;
    }
    public void EndCombat()
    {
        combat= false;
        CalculateDestinationRandom();
    }

}

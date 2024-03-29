using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private AudioClip hitSFX, hitVariantSFX, deathSFX;
    [SerializeField] private LayerMask layermask;
    public void Start(){
        mainAudio = GetComponent<AudioSource>();
        Collider[] tempHold = Physics.OverlapBox(transform.position, transform.localScale/2, Quaternion.identity, layermask);
        myTrigger = tempHold[0].gameObject.GetComponent<CombatTriggers>();
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
    
}

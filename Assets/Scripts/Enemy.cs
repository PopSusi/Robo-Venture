using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Damageable
{
    //Damageable Variables
    public float HP{ get; set; }  
    public float damageDelay{ get; set; }
    public bool vulnerable{ get; set; }
    
    //Components
    private AudioSource mainAudio;
    [SerializeField]
    private AudioSource scndryAudio;

    [SerializeField] private AudioClip hitSFX, hitVariantSFX, deathSFX;
    public CombatTriggers myTrigger;
    [SerializeField] private LayerMask layermask;
    public void Start(){
        mainAudio = GetComponent<AudioSource>();
        Collider[] tempHold = Physics.OverlapBox(transform.position, transform.localScale/2, Quaternion.identity, layermask);
        myTrigger = tempHold[0].gameObject.GetComponent<CombatTriggers>();
        StartCoroutine("Fuckingdie");
    }
    public void TakeDamage(float damage)
    {
        if (vulnerable)
        {
            HP -= damage;
            if(!(HP <= 0)){ //Not at zero
                vulnerable = false;
                GetComponent<AudioSource>().Play();
                StartCoroutine("DamageDelay");
                HitNoise();
            } else {
                Die();
            }
        }
    }
    public void Die(){
        Debug.Log(myTrigger.CheckEnemies(gameObject));
        scndryAudio.clip = deathSFX;
        scndryAudio.Play();
        StartCoroutine("Death Audio Delay");
    }
    private IEnumerator Fuckingdie(){
        yield return new WaitForSeconds(3f);
        Die();
    }
    public void HitNoise()
    {
        scndryAudio.clip = Random.Range(0, 2) == 0 ? hitSFX : hitVariantSFX;
        scndryAudio.Play();
    }
    public IEnumerator DeathAudioDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(deathSFX.length + 1f);
        yield return wait;
        Destroy(gameObject);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource combatSource;
    public AudioSource levelSource;
    public AudioClip levelMusic;
    public AudioClip combatMusic;
    public static MusicManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        RoboLevels GM = RoboLevels.instance;
        levelMusic = GM.BGM;
        levelSource = GetComponent<AudioSource>();
        levelSource.clip = levelMusic;
        levelSource.Play();
    }
    public IEnumerator TransitionToCombat(){
        //Debug.Log("Starting Transition to Combat");
        yield return new WaitForSeconds(3f);
        float timeToFade = .25f;
        float timeElapsed = 0;
        combatSource.Play();
        while(timeElapsed < timeToFade){
            combatSource.volume = Mathf.Lerp(0,.4f, timeElapsed / timeToFade);
            levelSource.volume = Mathf.Lerp(.15f,0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
        }
        levelSource.Stop();
        //Debug.Log("Out of Main");
    }
    public IEnumerator TransitionToMain(){
        Debug.Log("Starting Transition to Main");
        yield return new WaitForSeconds(3f);
        float timeToFade = .25f;
        float timeElapsed = 0;
        levelSource.Play();
        while(timeElapsed < timeToFade){
            combatSource.volume = Mathf.Lerp(.4f,0, timeElapsed / timeToFade);
            levelSource.volume = Mathf.Lerp(0,.15f, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
        }
        combatSource.Stop();
        Debug.Log("Out of Combat");
    }
    
}

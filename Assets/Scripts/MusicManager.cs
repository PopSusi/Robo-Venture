using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource combatSource, mainLevelSource;
    public AudioClip myMain, combatMusic;
    // Start is called before the first frame update
    void Start()
    {
        GameObject currLevel = GameObject.FindWithTag("LevelGM");
        myMain = currLevel.GetComponent<RoboLevels>().BGM;
        mainLevelSource.clip = myMain;
        mainLevelSource.Play();
    }
    public IEnumerator TransitionToCombat(){
        Debug.Log("Starting Transition to Combat");
        yield return new WaitForSeconds(3f);
        float timeToFade = .25f;
        float timeElapsed = 0;
        combatSource.Play();
        while(timeElapsed < timeToFade){
            combatSource.volume = Mathf.Lerp(0,1, timeElapsed / timeToFade);
            mainLevelSource.volume = Mathf.Lerp(1,0, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
        }
        mainLevelSource.Stop();
        Debug.Log("Out of Main");
    }
    public IEnumerator TransitionToMain(){
        Debug.Log("Starting Transition to Main");
        yield return new WaitForSeconds(3f);
        float timeToFade = .25f;
        float timeElapsed = 0;
        mainLevelSource.Play();
        while(timeElapsed < timeToFade){
            combatSource.volume = Mathf.Lerp(1,0, timeElapsed / timeToFade);
            mainLevelSource.volume = Mathf.Lerp(0,1, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
        }
        combatSource.Stop();
        Debug.Log("Out of Combat");
    }
}

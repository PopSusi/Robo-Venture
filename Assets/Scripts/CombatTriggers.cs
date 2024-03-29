using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTriggers : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private LayerMask layermask;
    private GameObject player;

    private void Awake(){
        StartCoroutine("LoadPause");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            player.GetComponent<MusicManager>().StartCoroutine("TransitionToCombat");
            for (int i = 0; i < Enemies.Count - 1; i++)
            {
                Enemies[i].GetComponent<Enemy>().StartCombat();
            }
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            if (Enemies.Count == 0)
            {
                player.GetComponent<MusicManager>().StartCoroutine("TransitionToMain");
            }
        }
    }
    public bool CheckEnemies(GameObject deadEnemy){
        for (int i = 0; i < Enemies.Count - 1; i++) 
        {
            if(GameObject.ReferenceEquals(deadEnemy, Enemies[i])){
                Enemies.RemoveAt(i);
            }
        }
        if(Enemies.Count == 0){
            StartCoroutine("CombatOver");
            return true;
        }
        return false;
    }
    private IEnumerator CombatOver(){
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < Enemies.Count - 1; i++)
        {
            Enemies[i].GetComponent<Enemy>().EndCombat();
        }
        player.GetComponent<MusicManager>().StartCoroutine("TransitionToMain");
    }
    private IEnumerator LoadPause(){
        yield return new WaitForSeconds(.5f);
        Collider[] enemiesIn = Physics.OverlapBox(transform.position, transform.localScale/2, Quaternion.identity, layermask);
        for (int i = 0; i < enemiesIn.Length; i++) 
        {
            Enemies.Add(enemiesIn[i].gameObject);
        }
       
    }
}

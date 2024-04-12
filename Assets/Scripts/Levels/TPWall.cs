using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPWall : LevelData
{
    public Levels destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FadeToBlack");
        }
    }
    IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(3f);
        LoadLevel();
    }
    private void LoadLevel()
    {
        Settings.PrevLevel = RoboLevels.instance.currLevel;
        string tempString = destination.ToString() + "Level";
        SceneManager.LoadScene(tempString);
    }
}

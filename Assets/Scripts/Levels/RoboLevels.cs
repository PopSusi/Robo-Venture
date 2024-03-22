using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboLevels : MonoBehaviour
{
    public enum Levels
    {
        Hub = 0,
        Dash,
        Grapple,
        Wall
    };
    public static Levels currLevel = Levels.Hub;
    public GameObject player;
    public GameObject GOobjUI;
    public int objIndex = 0;
    public string[] objText;
    protected void FindPlayer()
    {
        player = GameObject.Find("player");
    }
}
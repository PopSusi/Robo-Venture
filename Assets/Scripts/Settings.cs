using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static bool dash, wall, grapple;
    public static bool allModChips;
    public static bool AllModChips
    {
        get { return allModChips; }
        set
        {
            allModChips = value;
            dash = value;
            wall = value;
            grapple = value;
        }
    }
    public static bool infiniteHealth;
    public static bool InfiniteHealth
    {
        get { return infiniteHealth; }
        set
        {
            infiniteHealth = value;
        }
    }
    public static bool hardMode;
    public static bool HardMode
    {
        get { return hardMode; }
        set
        {
            hardMode = value;
        }
    }

    public static Settings instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static bool dash=false, wall = false, grapple = false;
    public static bool allModChips = false;
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
    public static bool infiniteHealth = false;
    public static bool InfiniteHealth
    {
        get { return infiniteHealth; }
        set
        {
            infiniteHealth = value;
        }
    }
    public static bool hardMode = false;
    public static bool HardMode
    {
        get { return hardMode; }
        set
        {
            hardMode = value;
        }
    }

    //public static Settings instance;
    // Start is called before the first frame update
    //void Awake()
    //{
    //    if(instance == null)
    //    {
    //        instance = this;
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
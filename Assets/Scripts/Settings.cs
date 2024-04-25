using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static bool dash;
    public static bool wall;
    public static bool grapple;
    public static bool Dash
    {
        get {
            return dash;
        }
        set
        { dash = value; Debug.Log(value);

        }
    }
    public static bool Wall
    {
        get
        {
            return wall;
        }
        set
        {
            wall = value; Debug.Log(value);

        }
    }
    public static bool Grapple
    {
        get
        {
            return grapple;
        }
        set
        {
            grapple = value; Debug.Log(value);

        }
    }
    public static bool allModChips = false;
    public static bool AllModChips 
    {
        get { return allModChips; }
        set
        {
            allModChips = value;
            Dash = value;
            Wall = value;
            Grapple = value;
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
    public static RoboLevels.Levels prevLevel = RoboLevels.Levels.Hub;
    public static RoboLevels.Levels PrevLevel
    {
        get { return prevLevel; }
        set { prevLevel = value; }
    }
    public static HashSet<GameObject> Collected = new HashSet<GameObject>();
    
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallAbility : Ability
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        input.actions["Punch"].performed += OnPunch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnPunch(InputAction.CallbackContext context)
    { 
        if(context.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
        {
            Debug.Log(context.interaction.ToString());
        }
    }
}

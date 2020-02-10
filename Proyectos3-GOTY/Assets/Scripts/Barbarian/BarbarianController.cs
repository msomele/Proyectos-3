using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianController : PlayerController 
 //Con heredar de PlayerController y en: Start, Update y FixedUpdate poner base.tatata() ya se mueve con el defaultSet.
{
    
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();   
    }
    void FixedUpdate() 
    {
        base.FixedUpdate();
    }
}

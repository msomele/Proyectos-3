using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianController : PlayerController 
 //Con heredar de PlayerController y en: Start, Update y FixedUpdate poner base.tatata() ya se mueve con el defaultSet.
{

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();   
    }
    new void FixedUpdate() 
    {
        base.FixedUpdate();
    }
    



}

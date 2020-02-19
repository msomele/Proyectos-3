using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackType { heavy = 2, light = 1, lighter = 0 }
public class BarbarianCombos : MonoBehaviour
{

    [Header("Inputs test")]
    public KeyCode heavyKey;
    public KeyCode lightKey;
    public KeyCode lighterKey;

    [Header("Attacks")]
    public Attack heavyAttack;
    public Attack lightAttack;
    public Attack lighterAttack;
    public List<Combo> combos;
    public float comboLeeway = 0;

    [Header("Components")]
    public BarbarianController inputs; 
    public Animator anim;
    public ComboInput lastInput = null;
    public Attack currentAttack = null;
    List<int> currentCombos = new List<int>();
    [HideInInspector] public float timer = 0;
    float leeWay = 0;
    bool skip = false; 

    void Start()
    {
        anim = GetComponent<Animator>();

    }
    void PrimeCombos() //Once the event is invoked, send a call over to this script 2 play x or y attack
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.onInputted.AddListener(() =>
            {
                //call attack function with the combo´s attack
                skip = true;
                AttackF(c.comboAttack);
                ResetCombos();
            });
        }
    }


    void Update()
    {
        if(currentAttack != null)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                currentAttack = null;
          
            return;
        }

        if (currentCombos.Count > 0)
        {
            leeWay += Time.deltaTime;
            if (leeWay >= comboLeeway)
            {
                if (lastInput != null)
                {
                    AttackF(GetAttackFromType(lastInput.type));
                    lastInput = null;
                }
                ResetCombos();
            }
        }
        else
            leeWay = 0; 

        ComboInput input = null;
        if (inputs.controls.Gameplay.LightAttack.triggered) //if key X pressed
            input = new ComboInput(AttackType.light);
        if (inputs.controls.Gameplay.HeavyAttack.triggered)  //if key Y pressed
            input = new ComboInput(AttackType.heavy);
        if (inputs.controls.Gameplay.LighterAttack.triggered)  //if key Z pressed
            input = new ComboInput(AttackType.lighter);

        if (input == null) return;

        lastInput = input;
        List<int> remove = new List<int>();
        for (int i = 0; i < currentCombos.Count; i++)
        { 
            Combo c = combos[currentCombos[i]];
            if (c.ContinueCombo(input))
            {
                leeWay = 0; 
            }
            else
                remove.Add(i);
        }
        if(skip)
        {
            skip = false;
            return; 
        }

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentCombos.Contains(i)) continue;
            if (combos[i].ContinueCombo(input))
            {
                currentCombos.Add(i);
                leeWay = 0; 
            }
            
        }

        foreach(int i in remove)
            currentCombos.RemoveAt(i);

        if(currentCombos.Count <= 0)
        {
            AttackF(GetAttackFromType(input.type));
        }
    }

    void ResetCombos()
    {
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            c.ResetCombo();
        }
        currentCombos.Clear();
        leeWay = 0;
    }

    void AttackF(Attack att)
    {
        currentAttack = att;
        timer = att.length;
        Debug.Log(att.name);
        anim.Play(att.name, -1, 0);
    }
    
    Attack GetAttackFromType(AttackType t)
    {
        if (t == AttackType.heavy)
            return heavyAttack;
        if (t == AttackType.heavy)
            return lightAttack;
        return null;
    }


    


    [System.Serializable]
    public class Attack
    {
        public string name;
        public float length;
    }
    [System.Serializable]
    public class ComboInput
    {
        public AttackType type;
        public ComboInput(AttackType t)
        {
            type = t; 
        }
        public bool IsSameAs(ComboInput test)
        {
            return (type == test.type);// movement input? ==>> add && comboInput.Movement == inputs[currentInput].movement
        }
    }
    [System.Serializable]
    public class Combo
    {
        public string name;
        public List<ComboInput> inputs;
        public Attack comboAttack;
        public UnityEvent onInputted;
        int currentInput = 0;

        public bool ContinueCombo(ComboInput comboInput)
        {
            if(inputs[currentInput].IsSameAs(comboInput)) 
            {
                currentInput++;
                if(currentInput >= inputs.Count) //Finished input and do the attack action
                {
                    onInputted.Invoke();
                    currentInput = 0; 
                }
                return true;
            }
            else
            {
                currentInput = 0;
                return false; 
            }
        }

        public ComboInput CurrentComboInput()
        {
            if (currentInput >= inputs.Count)
                return null;
            return inputs[currentInput];
        }

        public void ResetCombo()
        {
            currentInput = 0; 
        }
    }
}

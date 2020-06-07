using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFunction : MonoBehaviour
{
    public BarbarianController player;
    public GameObject hammerSmashCollider;
    void Start()
    {
        player = gameObject.GetComponentInParent<BarbarianController>();
        hammerSmashCollider = player.Ability1Collider;
    }
    public void ActivateCollider()
    {
        hammerSmashCollider.GetComponent<MeshCollider>().enabled = true;
    }
    public void DeActivateCollider()
    {
        hammerSmashCollider.GetComponent<MeshCollider>().enabled = false;
        player.StartCoroutine(player.NowCanMove(player.barbarianAnimator));
    }
    public void IamIdle()
    {
        if(player != null)
        player.isIdle = true;
    }
    public void NoIdle()
    {
        if (player != null)
            player.isIdle = false;
    }
    public void IamAtt1()
    {
        if (player != null)
            player.isAtt1 = true;
    }
    public void NoAtt1()
    {
        if (player != null)
        {

            player.isAtt1 = false;
            if (!player.isAtt2)
                player.StartCoroutine(player.NowCanMove(player.barbarianAnimator));

        }
    }
    public void IamAtt2()
    {
        if (player != null)
            player.isAtt2 = true;
    }
    public void NoAtt2()
    {
        if (player != null)
        {
            player.isAtt2 = false;
            player.StartCoroutine(player.NowCanMove(player.barbarianAnimator));

        }
    }
    public void NoAb1()
    {
        if(player!=null)
        {
            player.StartCoroutine(player.NowCanMove(player.barbarianAnimator));
        }
    }



    public void TriggerAA()
    {
        player.TriggerDmgAAAction();
    }
    public void TriggerHab1()
    {
        player.TriggerDmgAb1Action();
    }






}

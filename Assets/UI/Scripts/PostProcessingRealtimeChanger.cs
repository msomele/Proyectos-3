using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
public class PostProcessingRealtimeChanger : MonoBehaviour
{
    private Volume v;
    DepthOfField dof;
    public float min_dof, max_dof, current_dof;
    public float timeStartedLerping;
    public float LerpTime;
    public bool shouldLerp, shouldLerpV2;
    public float givenDof;
    public bool auxcutre;
    // Start is called before the first frame update
    void Start()
    {
        v = GetComponent<Volume>();
        VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));
        if (!volumeProfile.TryGet(out dof)) throw new System.NullReferenceException(nameof(dof));
        current_dof = max_dof;
        //ChangeDOF(12f);
        if(auxcutre)
            Startlerping();
    }
    /*
    void ChangeDOF(float duration)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            current_dof = Mathf.Lerp(max_dof, min_dof, progress);
            dof.focusDistance.Override(current_dof);
        }
    }
    */
    private void Startlerping()
    {
        timeStartedLerping = Time.time;
        shouldLerp = true;
        shouldLerpV2 = false; 
    }

    private void Update()
    {
        if (shouldLerp) { 
            current_dof = CustomFloatLerp(max_dof, min_dof, timeStartedLerping, LerpTime);
            dof.focusDistance.Override(current_dof);
        }
        if (current_dof <= min_dof)
        {
            shouldLerp = false;
        }
        
        if(shouldLerpV2)
        {
            dof.focusDistance.Override(givenDof);
        }
        if (current_dof <= givenDof)
        {
            shouldLerpV2 = false;
        }
    }

    float CustomFloatLerp(float start, float end, float timeStartedLerping, float lerptime)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerptime;
        float result = Mathf.Lerp(start, end, percentageComplete);
        return result;
    }


    public void ChangeFov(float fov)
    {
        dof.focusDistance.Override(fov);
    }


}

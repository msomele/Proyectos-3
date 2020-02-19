using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{

    [Header("Objects information")]
    [SerializeField][Tooltip("All the gameobjects type <Player> in scene")]
    private List<GameObject> players = new List<GameObject>();

    [Header("Camera Settings")]
    [Tooltip("Camera offset from the center focus")]
    public Vector3 offset;
    [Range(0.01f, 1.0f)][Tooltip("0 means no smooth in camera follow movement. 1 full smooth")]
    public float SmoothFactor = 0.468f;
    [SerializeField][Tooltip("Point where camera is focusing")]
    private Vector3 midpoint;
  
   [Header("Zoom Settings")]
    [Tooltip("Maximum withing the limits for the zoom")]
    public float maxZoom = 40f;
    [Tooltip("Minimum withing the limits for the zoom")]
    public float minZoom = 10f;
    [Tooltip("Farest positioning in zoom limits")]
    public float zoomLimit = 50f;

    private Vector3 vel;

    public void AddPlayer(GameObject playerObj) => players.Add(playerObj);
    
   
    private void LateUpdate()
    {
        if (players.Count == 0)
            return;
        Move();
        Zoom();
    }


    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimit); 
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, newZoom, Time.deltaTime); 
    }


    float GetGreatestDistance()
    {
        var bounds = new Bounds(players[0].transform.position, Vector3.zero);
        foreach (GameObject player in players)
        {
            bounds.Encapsulate(player.transform.position);
        }
        return bounds.size.x;
    }


    void Move()
    {
        Vector3 middlePoint = GetMiddlePoint();
        Vector3 newPos = middlePoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref vel, SmoothFactor);
    }


    Vector3 GetMiddlePoint()
    {
        if (players.Count == 1)
            return players[0].transform.position;

        var bounds = new Bounds(players[0].transform.position, Vector3.zero);
        foreach (GameObject player in players)
        {
            bounds.Encapsulate(player.transform.position);
        }

        return bounds.center; 
    }


}
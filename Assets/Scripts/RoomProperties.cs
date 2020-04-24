﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomProperties : MonoBehaviour
{
    GameManager gm;    
    public GameObject virtualCamera;
    public SpriteRenderer blackOutCanvas;
    private Color transparent = new Color(1f, 1f, 1f, 0f);
    private Color opaque = new Color(1f, 1f, 1f, 1f);
    private Color startingColor;    
        
    void Awake()
    {        
        Assignments();
    }

    private void Assignments()
    {
        gm = FindObjectOfType<GameManager>();
        blackOutCanvas = GetComponent<SpriteRenderer>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>().gameObject;               
    }

    private void Start()
    {
        blackOutCanvas.color = opaque;
        virtualCamera.SetActive(false);        
    }

    public void LeaveRoom(GameObject lastRoom)
    {
        gm.lastRoom = lastRoom;
        virtualCamera.SetActive(false);        
        startingColor = blackOutCanvas.color;        
        StartCoroutine(FadeBlackOutCanvas(startingColor, opaque, gm.fadeSpeed));
    }

    public void EnterRoom(GameObject roomEntered)
    {        
        gm.RoomChange(roomEntered);        
        virtualCamera.SetActive(true);        
        startingColor = blackOutCanvas.color;        
        StartCoroutine(FadeBlackOutCanvas(startingColor, transparent, gm.fadeSpeed));
    }

    IEnumerator FadeBlackOutCanvas(Color startingColor, Color fadeToColor, float fadeSpeed)
    {
        float currentTime = 0f;
        while (blackOutCanvas.color != fadeToColor)
        {   
            currentTime += Time.deltaTime;
            blackOutCanvas.color = Color.Lerp(startingColor, fadeToColor, (currentTime / fadeSpeed));
            yield return null;
        }

        StopCoroutine(FadeBlackOutCanvas(startingColor, fadeToColor, fadeSpeed));
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomProperties : MonoBehaviour
{
    public GameObject virtualCamera;
    public SpriteRenderer blackOutCanvas;
    private Color transparent = new Color(255f, 255f, 255f, 0f);
    private Color opaque = new Color(255f, 255f, 255f, 1f);

    // Start is called before the first frame update
    void Awake()
    {   
        // Set reference to each room's camera, and then disable to save resources.
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>().gameObject;
        virtualCamera.SetActive(false);

        blackOutCanvas = GetComponent<SpriteRenderer>();
        blackOutCanvas.color = opaque;
    }

    public void LeaveRoom()
    {
        virtualCamera.SetActive(false);
        blackOutCanvas.color = opaque;
    }

    public void EnterRoom()
    {
        virtualCamera.SetActive(true);
        blackOutCanvas.color = transparent;
    }

}

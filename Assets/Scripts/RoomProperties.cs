using System.Collections;
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
    public List<GameObject> sceneTransitions;

    // Start is called before the first frame update
    void Awake()
    {        
        Assignments();
    }

    private void Assignments()
    {
        gm = FindObjectOfType<GameManager>();
        blackOutCanvas = GetComponent<SpriteRenderer>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>().gameObject;

        // Looks through each child in the room. There's probably a more cost effective way of performing this.
        Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < allChildren.Length; i++)
        {
            if (allChildren[i].gameObject.tag == "SceneTransition")
            {
                sceneTransitions.Add(allChildren[i].gameObject);
            }
        }
    }

    private void Start()
    {
        blackOutCanvas.color = opaque;
        virtualCamera.SetActive(false);
        TurnOffSceneTransitions();
    }

    private void TurnOffSceneTransitions()
    {
        for (int i = 0; i < sceneTransitions.Count; i++)
        {
            sceneTransitions[i].SetActive(false);
        }
    }

    private void TurnOnSceneTransitions()
    {
        for (int i = 0; i < sceneTransitions.Count; i++)
        {
            sceneTransitions[i].SetActive(true);
        }
    }

    public void LeaveRoom(GameObject lastRoom)
    {
        print("LeaveRoom has been called in " + transform.gameObject.name);
        virtualCamera.SetActive(false);        
        startingColor = blackOutCanvas.color;
        TurnOffSceneTransitions();
        StartCoroutine(FadeBlackOutCanvas(startingColor, opaque, gm.fadeSpeed));
        gm.lastRoom = lastRoom;
    }

    public void EnterRoom(GameObject currentRoom)
    {
        print("EnterRoom has been called in " + transform.gameObject.name);
        virtualCamera.SetActive(true);        
        startingColor = blackOutCanvas.color;
        Invoke("TurnOnSceneTransitions", gm.fadeSpeed);
        StartCoroutine(FadeBlackOutCanvas(startingColor, transparent, gm.fadeSpeed));
        gm.currentRoom = currentRoom;
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

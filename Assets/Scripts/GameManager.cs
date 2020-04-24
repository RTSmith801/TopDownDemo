using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public GameObject currentRoom;
    public GameObject lastRoom;
    public float fadeSpeed = 1f;
    public GameObject player;
    public List<GameObject> enemies;
    public List<GameObject> sceneTransitions;

    void Awake()
    {
        Assignments();
    }

    private void Assignments()
    {        
        IdentifyAllEnemies();
        IdentifyAllSceneTransitions();
        player = GameObject.FindGameObjectWithTag("Player");
        currentRoom = player.transform.parent.gameObject;
    }

    private void IdentifyAllSceneTransitions()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("SceneTransition");
        for (int i = 0; i < objects.Length; i++)
        {
            sceneTransitions.Add(objects[i].gameObject);
        }
    }

    private void IdentifyAllEnemies()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objects.Length; i++)
        {            
            enemies.Add(objects[i].gameObject);            
        }
    }

    void Start()
    {
        RoomChange(currentRoom);
        TurnOffSceneTransitions();        
        currentRoom.GetComponent<RoomProperties>().EnterRoom(currentRoom);
    }

    public void RoomChange(GameObject currentRoom)
    {
        player.transform.SetParent(null);
        player.transform.SetParent(currentRoom.transform);

        if (lastRoom != null)
        {
            TurnOffSceneTransitions();
        }

        Invoke("TurnOnSceneTransitions", fadeSpeed);
    }   

    public void TurnOffSceneTransitions()
    {
        for (int i = 0; i < sceneTransitions.Count; i++)
        {
            if (sceneTransitions[i].transform.parent == lastRoom)
            {
                sceneTransitions[i].SetActive(false);
            }
        }
    }

    public void TurnOnSceneTransitions()
    {
        for (int i = 0; i < sceneTransitions.Count; i++)
        {
            if (sceneTransitions[i].transform.parent == currentRoom)
            {
                sceneTransitions[i].SetActive(true);
            }
        }
    }
    
}

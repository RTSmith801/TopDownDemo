using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public GameObject currentRoom;
    public GameObject lastRoom;
    public float fadeSpeed = .7f;
    public List<GameObject> enemies;
    public List<GameObject> sceneTransitions;
        
    public GameObject player;   

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
        //RoomChange(currentRoom);
        TurnOffSceneTransitions();
        //currentRoom.GetComponent<RoomProperties>().EnterRoom(currentRoom);        
    }

    public void RoomChange(GameObject roomEntered)
    {
        currentRoom = roomEntered;
        player.transform.SetParent(null);
        player.transform.SetParent(currentRoom.transform);

        if (lastRoom != null && lastRoom != currentRoom)
        {
            TurnOffSceneTransitions();
        }
        DisableEnemies();
        EnableEnemies();
        Invoke("TurnOnSceneTransitions", fadeSpeed);
    }

    public void DisableEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].transform.parent.gameObject != currentRoom)
            {
                enemies[i].gameObject.SetActive(false);
            }
        }
    }

    public void EnableEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].transform.parent.gameObject == currentRoom)
            {
                enemies[i].gameObject.SetActive(true);
            }
        }
    }

    public void TurnOffSceneTransitions()
    {
        for (int i = 0; i < sceneTransitions.Count; i++)
        {
            if (sceneTransitions[i].transform.parent.gameObject != currentRoom)
            {
                sceneTransitions[i].SetActive(false);
            }
        }
    }

    public void TurnOnSceneTransitions()
    {
        for (int i = 0; i < sceneTransitions.Count; i++)
        {
            if (sceneTransitions[i].transform.parent.gameObject == currentRoom)
            {
                sceneTransitions[i].SetActive(true);
            }
        }
    }
    
}

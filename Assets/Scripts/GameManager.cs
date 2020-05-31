using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Scene scene;
    public AudioManager am;
    public GameObject currentRoom;
    public GameObject lastRoom;
    public float fadeSpeed = .7f;
    public List<GameObject> enemies;
    public List<GameObject> sceneTransitions;
        
    public GameObject player;

    // XBox Controller Settings
    PlayerControls XBoxControllerInput;

    // UI Settings
    [Header ("UI Settings")]
    //public int health;
    //public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Use Awake(), OnEnable(), and OnDisable() to identify controller input
    private void Awake()
    {
        Assignments();

        XBoxControllerInput = new PlayerControls();

        XBoxControllerInput.Gameplay.Start.performed += ctx => ReloadScene();        
    }

    private void OnEnable()
    {
        XBoxControllerInput.Gameplay.Enable();
    }

    private void OnDisable()
    {
        XBoxControllerInput.Gameplay.Disable();
    }

    private void Update()
    {
        AltControls();        
    }

    void AltControls()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReloadScene();
        }
    }

    public void UIHealthUpdate(int health, int numOfHearts)
    {
        for (int i = 0; i < hearts.Length; i++)
        {            
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            // Hides heart containers not in use.
            if (i < numOfHearts)            
                hearts[i].enabled = true;            
            else
                hearts[i].enabled = false;
        }
    }


    private void Assignments()
    {
        am = FindObjectOfType<AudioManager>();
        IdentifyAllEnemies();
        IdentifyAllSceneTransitions();
        player = GameObject.FindGameObjectWithTag("Player");
        currentRoom = player.transform.parent.transform.gameObject;
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
        scene = SceneManager.GetActiveScene();
        // If Scene Transitions exist, will disable and set active on enter room.
        TurnOffSceneTransitions();
    }

    public void RoomChange(GameObject roomEntered)
    {
        currentRoom = roomEntered;
        player.transform.parent.transform.SetParent(null);
        player.transform.parent.transform.SetParent(currentRoom.transform);

        if (lastRoom != null && lastRoom != currentRoom)
        {
            TurnOffSceneTransitions();
        }
        EnableEnemies();
        Invoke("DisableEnemies", fadeSpeed);
        Invoke("TurnOnSceneTransitions", fadeSpeed);
    }

    public void DisableEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i].gameObject);
            }


            else if (enemies[i].transform.parent.gameObject != currentRoom)
            {
                enemies[i].gameObject.SetActive(false);
            }
        }
    }

    public void EnableEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i].gameObject);
            }

            else if (enemies[i].transform.parent.gameObject == currentRoom)
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

    public void PlayerDeath()
    {
        print("Player Death called in Game Manager");
        Invoke("ReloadScene", 5f);
    }
    
    void ReloadScene()
    {
        SceneManager.LoadScene(scene.buildIndex);
    }
}

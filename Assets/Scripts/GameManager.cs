using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public GameObject currentRoom;
    public float fadeSpeed = 2f;    
    
    // Start is called before the first frame update
    void Start()
    {
        currentRoom.GetComponent<RoomProperties>().EnterRoom();
    }
}

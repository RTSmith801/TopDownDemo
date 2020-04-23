using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    // Used to move the player from scene/room to another.
    public Vector3 playerChange;
    public GameObject currentRoom;
    public GameObject newRoom;   
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position += playerChange;
            currentRoom.GetComponent<RoomProperties>().LeaveRoom();
            newRoom.GetComponent<RoomProperties>().EnterRoom();
        }
    }


}

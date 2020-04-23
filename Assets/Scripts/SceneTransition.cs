using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    // Used to move the player from scene/room to another.
    //public Vector3 playerChange;
    public GameObject currentRoom;
    public GameObject nextRoom;

    private void Start()
    {
        currentRoom = this.transform.parent.gameObject;
        if(currentRoom == null)
        {
            print("An unparented scene transition exists");
        }
        if (nextRoom == null)
        {
            print("An undefined scene transition exists in " + this.transform.parent.gameObject.name);            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {   
            currentRoom.GetComponent<RoomProperties>().LeaveRoom(currentRoom);
            nextRoom.GetComponent<RoomProperties>().EnterRoom(nextRoom);
        }
    }
}

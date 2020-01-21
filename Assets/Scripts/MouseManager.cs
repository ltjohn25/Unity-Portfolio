using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    // What objects are clickable
    // Swap cursors out per object
    // Start is called before the first frame update
    public LayerMask clickablelayer;
    public Texture2D pointer; //normalpointer
    public Texture2D target; //Cursor for clickable objects
    public Texture2D doorway; //cursor for doorwways
    public Texture2D combat;//cursor for combat

    public EventVector3 OnClickEnvironment;
    void Update()
    {

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50, clickablelayer.value))
        {
            bool door = false;
            bool item = false;
            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else if(hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(combat, new Vector2(10, 10), CursorMode.Auto);
                item = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(doorway.position);
                    Debug.Log("DOOR");
                }
                else if(item)
                {
                    Transform itemPos = hit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(itemPos.position);
                    Debug.Log("ITEM");
                }
                else
                {
                    OnClickEnvironment.Invoke(hit.point);
                }
            }

        }

        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

    
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
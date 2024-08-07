using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
private string groundTag = "Ground";
private bool enterGround;
private bool exitGround;
private bool onGround;

public bool IsGround(){
    return !exitGround;
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.tag == groundTag)
    {
        enterGround = true;
        exitGround = false;
        // Debug.Log("何かが判定に入りました");
    }
}
 
private void OnTriggerStay2D(Collider2D collision)
{
     if (collision.tag == groundTag)
     {
        onGround = true;
        // Debug.Log("何かが判定に入り続けています");
     }
}
     
private void OnTriggerExit2D(Collider2D collision)
{
     if (collision.tag == groundTag)
     {
        onGround = false;
        enterGround = false;
        exitGround = true;
        // Debug.Log("何かが判定をでました");
     }
}
}
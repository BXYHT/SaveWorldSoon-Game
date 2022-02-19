using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTrigger : MonoBehaviour
{
    public string message = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fungus.Flowchart.BroadcastFungusMessage(message);
    }
}

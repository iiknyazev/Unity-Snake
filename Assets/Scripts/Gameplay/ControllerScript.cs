using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col)
    {
        var obstacle = col.GetComponent<IEndGame>();
        if (obstacle != null)
        {
            obstacle.EndGameFoo();
            Destroy(transform.parent.gameObject);
        }
    }
}

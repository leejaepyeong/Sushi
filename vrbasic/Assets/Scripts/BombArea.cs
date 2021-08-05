using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Goal")
        {
            GameManager.instance.LoseLife();
        }
    }
}

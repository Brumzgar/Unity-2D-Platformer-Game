using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnScript : MonoBehaviour
{
    public GameObject tutorialToTurnOn;
    public Transform player;
    public float distanceToPlayer;

    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < 12)
            tutorialToTurnOn.SetActive(true);
    }
}

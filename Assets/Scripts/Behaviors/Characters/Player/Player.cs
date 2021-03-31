using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAnimation playerAnimation;
    public PlayerInteraction playerInteraction;

    public void Reset()
    {
        this.transform.eulerAngles = Vector3.zero;
        playerMovement.Reset();
    }

    protected virtual void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }
}

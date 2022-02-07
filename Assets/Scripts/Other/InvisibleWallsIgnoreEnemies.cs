using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallsIgnoreEnemies : MonoBehaviour
{
    public void Update()
    {
        // Ignore Platforms
        Physics2D.IgnoreLayerCollision(12, 11);
    }
}

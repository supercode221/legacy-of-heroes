﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestroyer : MonoBehaviour
{
    public float lifetime = 5.0f;

    // The amount of time this gameobject has already existed in play mode
    private float timeAlive = 0.0f;

    public bool destroyChildrenOnDeath = true;

    void Update()
    {
        // Every frame, increment the amount of time that this gameobject has been alive,
        // or if it has exceeded it's maximum lifetime, destroy it
        if (timeAlive > lifetime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timeAlive += Time.deltaTime;
        }
    }

    // Flag which tells whether the application is shutting down (helps avoid errors)
    public static bool quitting = false;

    private void OnApplicationQuit()
    {
        quitting = true;
        DestroyImmediate(this.gameObject);
    }

    private void OnDestroy()
    {
        if (destroyChildrenOnDeath && !quitting && Application.isPlaying)
        {
            int childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject childObject = transform.GetChild(i).gameObject;
                if (childObject != null)
                {
                    Destroy(childObject);
                }
            }
        }
        transform.DetachChildren();
    }
}

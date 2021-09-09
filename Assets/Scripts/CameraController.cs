using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float DUMPING = 3;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    private Transform player;
    private int lastX;

    void SetLastX(int X)
    {
        this.lastX = X;
    }

    int GetLastX()
    {
        return this.lastX;
    }

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), (offset.y) + 0.4f);
        FindPlayer(isLeft);
    }

    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetLastX(Mathf.RoundToInt(player.position.x));
        if (playerIsLeft)
        {
            transform.position = new Vector3(player.position.x - offset.x, player.position.y -offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }
    }

    void Update()
    {
        if (player)
        {
            int currentX = Mathf.RoundToInt(player.position.x);
            if (currentX > GetLastX())
            {
                isLeft = false;
            }
            else
            {
                if (currentX > GetLastX())
                {
                    isLeft = true;
                }
            }
        }

        SetLastX(Mathf.RoundToInt(player.position.x));
        Vector3 target;
        if (isLeft)
        {
            target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
        }
        else
        {
            target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        }

        Vector3 currentPosition = Vector3.Lerp(transform.position, target, DUMPING * Time.deltaTime);
        transform.position = currentPosition;
    }
}


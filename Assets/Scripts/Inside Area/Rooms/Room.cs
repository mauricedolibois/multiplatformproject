using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Room : MonoBehaviour
{
    public String roomName;
    public int roomId;
    [SerializeField] private Transform lowerLeft;
    [SerializeField] private Transform upperRight;

    public Transform LowerLeft => lowerLeft;
    public Transform UpperRight => upperRight;

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour {

    public static MainCanvasManager instane;

    [SerializeField]
    private LobbyCanvas _lobbyCanvas;
    public LobbyCanvas LobbyCanvas
    {
        get { return _lobbyCanvas; }
    }

    [SerializeField]
    private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get { return _currentRoomCanvas; }
    }

    private void Awake()
    {
        instane = this;
    }
}

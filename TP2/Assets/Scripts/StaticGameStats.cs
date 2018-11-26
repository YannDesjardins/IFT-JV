
﻿using System.Collections.Generic;
using UnityEngine;

public static class StaticGameStats{

	private static int enemyMax = 10;
	private static int enemyCount = 10;
    private static int difficulty = 1;
    private static float accuracy = 0.09f;
    private static bool controller = false;
    private static bool directionnal = false;
    private static bool joystick = true;
    private static Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>()
    {
        {"ForwardButton", KeyCode.W},
        {"BackwardButton", KeyCode.S },
        {"LeftButton", KeyCode.A },
        {"RightButton", KeyCode.D },
        {"ShootButton", KeyCode.Joystick1Button7 },


    };

    public static int EnemyMax
	{
		get 
		{
			return enemyMax;
		}
		set 
		{
			enemyMax = value;
		}
	}

	public static int EnemyCount 
	{
		get 
		{
			return enemyCount;
		}
		set 
		{
			enemyCount = value;
		}
	}

    public static Dictionary<string, KeyCode> Controls
    {
        get
        {
            return controls;
        }
        set
        {
            controls = value;
        }
	}

    public static int Difficulty
    {
        get
        {
            return difficulty;
        }

        set
        {
            difficulty = value;
        }
    }

    public static float Accuracy
    {
        get
        {
            return accuracy;
        }

        set
        {
            accuracy = value;
        }
    }

    public static bool Directionnal
    {
        get
        {
            return directionnal;
        }

        set
        {
            directionnal = value;
        }
    }

    public static bool Joystick
    {
        get
        {
            return joystick;
        }

        set
        {
            joystick = value;
        }
    }

    public static bool UsingController
    {
        get
        {
            return controller;
        }

        set
        {
            controller = value;
        }
    }
}

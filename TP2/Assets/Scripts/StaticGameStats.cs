
﻿using System.Collections.Generic;
using UnityEngine;

public static class StaticGameStats{

	private static Vector3 avatarHeadScale = new Vector3 (1.0f, 1.0f, 1.0f);
	private static Vector3 avatarBodyScale = new Vector3 (1.0f, 1.0f, 1.0f);
	private static int avatarColor1 = 0;
	private static int avatarColor2 = 0;
	private static int avatarColor3 = 0;
	private static bool avatarSantaHat = false;

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

	public static bool AvatarSantaHat
	{
		get 
		{
			return avatarSantaHat;
		}
		set 
		{
			avatarSantaHat = value;
		}
	}

	public static Vector3 AvatarBodyScale
	{
		get 
		{
			return avatarBodyScale;
		}
		set 
		{
			avatarBodyScale = value;
		}
	}

	public static Vector3 AvatarHeadScale
	{
		get 
		{
			return avatarHeadScale;
		}
		set 
		{
			avatarHeadScale = value;
		}
	}

	public static int AvatarColor1
	{
		get 
		{
			return avatarColor1;
		}
		set 
		{
			avatarColor1 = value;
		}
	}

	public static int AvatarColor2
	{
		get 
		{
			return avatarColor2;
		}
		set 
		{
			avatarColor2 = value;
		}
	}

	public static int AvatarColor3
	{
		get 
		{
			return avatarColor3;
		}
		set 
		{
			avatarColor3 = value;
		}
	}

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

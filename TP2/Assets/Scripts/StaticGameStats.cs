
﻿using System.Collections.Generic;
using UnityEngine;

public static class StaticGameStats{

	private static int enemyMax = 10;
	private static int enemyCount = 10;
    private static int difficulty = 1;
    private static Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>()
    {
        {"ForwardButton", KeyCode.W},
        {"BackwardButton", KeyCode.S },
        {"LeftButton", KeyCode.A },
        {"RightButton", KeyCode.D }
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
}

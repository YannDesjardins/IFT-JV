public static class StaticGameStats{

	private static int timeScale = 1;
	private static int enemyMax = 10;
	private static int enemyCount = 10;

	public static int TimeScale
	{
		get 
		{
			return timeScale;
		}
		set 
		{
			timeScale = value;
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
}

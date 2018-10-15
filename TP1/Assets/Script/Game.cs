
using UnityEngine;

public class Game : MonoBehaviour {

    bool started;
    int turn;
    int[] score;

    public ProjectileLauncher player1;
    public ProjectileLauncher player2;
    public int scoreToWin = 10;


	// Use this for initialization
	void Start () {
        started = false;
        turn = 0;
        score = new int[2];
        player1.onProjectileDestruction += ExecuteTurn;
        player2.onProjectileDestruction += ExecuteTurn;
        player1.onWallCollision += ModifyScore;
        player2.onWallCollision += ModifyScore;
    }
	
	// Update is called once per frame
	void Update () {
        if (!started){
            ExecuteTurn();
            started = true;
        }
	}

    void ExecuteTurn()
    {
        Debug.Log("Score is :" + score[0] + "-" + score[1]);
        if (VerifiyIfGameOver())
        {
            Application.Quit();
        }
        if (turn == 0)
        {
            player1.createProjectile();
            turn = 1;
        }
        else
        {
            player2.createProjectile();
            turn = 0;
        }
    }

    void ModifyScore()
    {
        score[turn]+=1;
    }

    bool VerifiyIfGameOver()
    {
        bool gameOver = false;
        for(int i = 0; i < score.Length; i++)
        {
            gameOver |= score[i] > scoreToWin;
        }
        return gameOver;
    }
}

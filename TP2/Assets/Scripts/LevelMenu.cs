using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public InputField InputColumns, InputRow, InputSeed;
    // Start is called before the first frame update
    void Start()
    {
        InputColumns.text = StaticGameStats.Columns.ToString();
        InputRow.text = StaticGameStats.Rows.ToString();
        InputSeed.text = StaticGameStats.Seed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public void EndEdit()
    //{
    //    StaticGameStats.Columns = int.Parse(InputColumns.text);
    //    StaticGameStats.Rows = int.Parse(InputRow.text);
    //    StaticGameStats.Seed = int.Parse(InputSeed.text);
    //}

    public void SetColumns()
    {
        StaticGameStats.Columns = int.Parse(InputColumns.text);
    }

    public void SetRow()
    {
        StaticGameStats.Rows = int.Parse(InputRow.text);
    }

    public void SetSeed()
    {
        StaticGameStats.Seed = int.Parse(InputSeed.text);
    }
}

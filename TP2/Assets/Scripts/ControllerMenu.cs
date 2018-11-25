using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMenu : MonoBehaviour {

    public Text ActionButton;
    private Dictionary<string, KeyCode> controls = StaticGameStats.Controls;
    private System.Array values = System.Enum.GetValues(typeof(KeyCode));
    private string selectedKey;
    // Use this for initialization
    void Start () {
        values = System.Enum.GetValues(typeof(KeyCode));
    }
	
	// Update is called once per frame
	void Update () {
        foreach (KeyCode code in values)
        {
            if (Input.GetKeyDown(code)) { print(System.Enum.GetName(typeof(KeyCode), code)); }
        }
        //Debug.Log(Input.GetAxis("4th Axis"));
        if (selectedKey != null)
        {
            foreach (KeyCode code in values)
            {
                if (Input.GetKeyDown(code))
                {
                    print(System.Enum.GetName(typeof(KeyCode), code));
                    GameObject.Find(selectedKey).GetComponentInChildren<Text>().text = System.Enum.GetName(typeof(KeyCode), code);
                    
                }
            }
            selectedKey = null;
        }
        
            Debug.Log(Input.GetAxis("HorizontalLeft"));
        
    }

    private void OnGUI()
    {
        //if (selectedKey != null)
        //{
        //    foreach (KeyCode code in values)
        //    {
        //        if (Input.GetKeyDown(code)) {
        //            print(System.Enum.GetName(typeof(KeyCode), code));
        //            GameObject.Find(selectedKey).GetComponentInChildren<Text>().text = System.Enum.GetName(typeof(KeyCode), code).ToString();
        //            selectedKey = null;
        //        }
        //    }
            
        //}
    }

    public void SelectKey(GameObject key)
    {
        selectedKey = key.name;
    }

    public void SetDirectional(GameObject key)
    {
        StaticGameStats.Directionnal = true;
        StaticGameStats.Joystick = false;
    }

    public void SetJoystick(GameObject key)
    {
        StaticGameStats.Joystick = true;
        StaticGameStats.Directionnal = false;
    }
}

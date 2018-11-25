using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// inspire de https://inscopestudios.com/portfolio-items/keybinds/
// et de https://www.youtube.com/watch?v=iSxifRKQKAA
public class ControlMenu : MonoBehaviour {


    public Text ForwardButton, BackwardButton, LeftButton, RightButton;
    private Dictionary<string, KeyCode> controls = StaticGameStats.Controls;
    private string selectedKey;
    // Use this for initialization
    void Start () {
        ForwardButton.text = controls["ForwardButton"].ToString();
        BackwardButton.text = controls["BackwardButton"].ToString();
        LeftButton.text = controls["LeftButton"].ToString();
        RightButton.text = controls["RightButton"].ToString();
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnGUI()
    {
        if (selectedKey != null)
        {
            Event current = Event.current;
            if (current.isKey)
            {
                controls[selectedKey] = current.keyCode;
                GameObject.Find(selectedKey).GetComponentInChildren<Text>().text = current.keyCode.ToString();
                selectedKey = null;
            }
        }
    }

    public void SelectKey( GameObject key)
    {
        selectedKey = key.name;
    }
}

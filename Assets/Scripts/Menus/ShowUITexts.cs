using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ShowControls : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI controlsText;
    [SerializeField] private TextMeshProUGUI areaText;
    void Start()
    {
        controlsText.text = 
                            "Attack: L-Mouse\n" +
                            "Stand-Up: Shift\n"+
                            "Inventory: I\n" +
                            "Pause: Q\n" ;
        areaText.text = "Outside Area";
    }

    // Update is called once per frame
    void Update()
    {
        //check if current scene is the Outside scene
        if (SceneManager.GetActiveScene().name == "Outside")
        {
            controlsText.text = 
                                "Attack: L-Mouse\n" +
                                "Stand-Up: Shift\n" +
                                "Inventory: I\n" +
                                "Pause: Q\n" ;
            areaText.text = "Outside Area";
        }
        else if (SceneManager.GetActiveScene().name == "Game Over Menu"){
            controlsText.text = "";
            areaText.text = "";
        }
        else
        {
            controlsText.text = "Attack: L-Mouse\n" +
                                "Inventory: I\n" +
                                "Pause: Q\n" ;
        }
    }
}

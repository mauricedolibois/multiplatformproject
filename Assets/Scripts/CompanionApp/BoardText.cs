using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardText : MonoBehaviour
{

    public int roomID;
    [SerializeField] private GameObject boardText;
    // Start is called before the first frame update
    void Start()
    {
        boardText.GetComponent<Text>().text = "111mHz";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

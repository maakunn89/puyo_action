using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextManager : MonoBehaviour
{
    void Start(){
        if ( GameVariableStatic.winner == "both") {
            gameObject.GetComponent<Text>().text = ("Draw!");
        } else {
            gameObject.GetComponent<Text>().text = ("The Winner is " + GameVariableStatic.winner + "!");
        }
    }
}
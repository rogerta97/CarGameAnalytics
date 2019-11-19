using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    public Text personIDText; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void GoGame()
    {
        SessionController.Instance.roundTime = 0;
        SessionController.Instance.sessionTime = 0;
        SessionController.Instance.welcomeUISeen = true;

        SessionController.Instance.personID = personIDText.text;
        SessionController.Instance.sessionID = Random.RandomRange(0, 9999999); 
    }
}

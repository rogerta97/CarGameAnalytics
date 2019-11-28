using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PathGenerator pathGenerator;
    public Text roundText; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlusButtonClicked()
    {
        if (pathGenerator.roundShowing < 3)
        {
            pathGenerator.roundShowing++;
            pathGenerator.GeneratePath();
            roundText.text = pathGenerator.roundShowing.ToString(); 
        }
    }

    public void OnMinusButtonClicked()
    {
        if(pathGenerator.roundShowing > 0)
        {
            pathGenerator.roundShowing--;
            pathGenerator.GeneratePath();
            roundText.text = pathGenerator.roundShowing.ToString();
        }
    }
}

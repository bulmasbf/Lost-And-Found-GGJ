using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssigner : MonoBehaviour
{
    private static PlayerAssigner _instance;
    public static PlayerAssigner Instance { get { return _instance; } }

    public int Player1ID = 0;
    public int Player2ID = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    public void AssignPlayer1(int playerIDInput)
    {
        if(Player1ID == 0)
        {
            Player1ID = playerIDInput;
            UIHandler.Instance.TogglePlayerAssignedSprite(1);
        }
    }
    public void AssignPlayer2(int playerIDInput)
    {
        if (Player2ID == 0)
        {
            Player2ID = playerIDInput;
            UIHandler.Instance.TogglePlayerAssignedSprite(2);
        }
    }
}

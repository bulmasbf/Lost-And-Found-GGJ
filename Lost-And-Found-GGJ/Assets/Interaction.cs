using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public enum state {Forward, Back, Left, Right};
    public state InteractableState;

    public TextMeshProUGUI instructionText;

    void Start()
    {
        if (gameObject.name == "Back")
        {
            InteractableState = state.Back;
        }

        if (gameObject.name == "Forward")
        {
            InteractableState = state.Forward;
        }

        if (gameObject.name == "Left")
        {
            InteractableState = state.Left;
        }

        if (gameObject.name == "Right")
        {
            InteractableState = state.Right;
        }
    }

    public void Interact()
    {
        switch (InteractableState)
        {
            case state.Forward:
                instructionText.text = "Forward";
                break;

            case state.Back:
                instructionText.text = "Back";
                break;

            case state.Left:
                instructionText.text = "Left";
                break;

            case state.Right:
                instructionText.text = "Right";
                break;
        }
    }
}

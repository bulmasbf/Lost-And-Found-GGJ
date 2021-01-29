using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    public enum state {Forward, Back, Left, Right, PressurePlateOn, PressurePlateOff };
    public state InteractableState;

    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI pressurePlateText;

    void Start()
    {
        if (gameObject.CompareTag("Back"))
        {
            InteractableState = state.Back;
        }

        if (gameObject.CompareTag("Forward"))
        {
            InteractableState = state.Forward;
        }

        if (gameObject.CompareTag("Left"))
        {
            InteractableState = state.Left;
        }

        if (gameObject.CompareTag("Right"))
        {
            InteractableState = state.Right;
        }

        if(gameObject.CompareTag("PressurePlate"))
        {
            InteractableState = state.PressurePlateOff;
            Interact();
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

            case state.PressurePlateOn:
                pressurePlateText.text = "PP On";
                Debug.Log("Preassure Plate On");
                break;

            case state.PressurePlateOff:
                pressurePlateText.text = "PP Off";
                Debug.Log("Preassure Plate Off");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && gameObject.CompareTag("PressurePlate"))
        {
            InteractableState = state.PressurePlateOn;
            Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("PressurePlate"))
        {
            InteractableState = state.PressurePlateOff;
            Interact();
        }
    }
}

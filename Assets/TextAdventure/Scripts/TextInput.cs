using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInput : MonoBehaviour
{
    public TMP_InputField inputField;
    private TextGameController controller;

    void Awake()
    {
        controller = GetComponent<TextGameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }
    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput);

        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);

        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if (inputAction.keyWord == separatedInputWords[0])
            {
                inputAction.RespondToInput(controller, separatedInputWords);
            }
        }
        
        InputComplete();
    }

    void InputComplete()
    {
        controller.DisplayLoggedText();
        //Reactivate field after hitting enter
        inputField.ActivateInputField();
        inputField.text = null;
    }
}

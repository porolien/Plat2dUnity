using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class Rebind : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpAction = null;
    [SerializeField] private InputActionReference mouvementAction = null;
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private TMP_Text jumpDisplayNameText = null;
    [SerializeField] private TMP_Text upDisplayNameText = null;
    [SerializeField] private TMP_Text downDisplayNameText = null;
    [SerializeField] private TMP_Text rightDisplayNameText = null;
    [SerializeField] private TMP_Text leftDisplayNameText = null;
    [SerializeField] private GameObject startRebindObject = null;
    [SerializeField] private GameObject waitingForInputObject = null;

    public Button JumpButton = null;
    public Button UpButton = null;
    public Button DownButton = null;
    public Button LeftButton = null;
    public Button RightButton = null;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private const string RebindsKey = "rebinds";
    // Start is called before the first frame update
    void Start()
    {
        // En utilisant un tuto de DapperDino
        string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);
       
        if (string.IsNullOrEmpty(rebinds)) 
        {
            return; 
        }
        playerMovement.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds);

        int bindingIndex = jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0]);

        jumpDisplayNameText.text = InputControlPath.ToHumanReadableString(
            jumpAction.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void Save()
    {
        string rebinds = playerMovement.PlayerInput.actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString(RebindsKey, rebinds);
    }
    public void StartRebinding(string ButtonRebind)
    {
        Debug.Log(mouvementAction.action.controls[0]);
        Debug.Log(mouvementAction.action.controls[1]);
        Debug.Log(mouvementAction.action.controls[2]);
        Debug.Log(mouvementAction.action.controls[3]);
        int controlsFromActions = 0;
        InputActionReference InputActionForRebind = null;
        TMP_Text bindingDisplayNameText = null;
       // InputActionForRebind.action = new InputAction(expectedControlType: "Vector2");

        switch (ButtonRebind)
        {
            case "jump":
                InputActionForRebind = jumpAction;
                bindingDisplayNameText = jumpDisplayNameText;
                break;
            case "up":
                InputActionForRebind = mouvementAction;
                bindingDisplayNameText = upDisplayNameText;
                break;
            case "down":
                InputActionForRebind = mouvementAction;
                controlsFromActions = 1;
                bindingDisplayNameText = downDisplayNameText;
                break;
            case "right":
                InputActionForRebind = mouvementAction;
                controlsFromActions = 3;
                bindingDisplayNameText = rightDisplayNameText;
                break;
            case "left":
                InputActionForRebind = mouvementAction;
                controlsFromActions = 2;
                bindingDisplayNameText = leftDisplayNameText;
                break;
        }
       startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);
        playerMovement.PlayerInput.SwitchCurrentActionMap("menu");
        if(InputActionForRebind == mouvementAction) 
        {
            rebindingOperation = InputActionForRebind.action.PerformInteractiveRebinding(controlsFromActions + 1)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(InputActionForRebind, controlsFromActions, bindingDisplayNameText))
            .Start();
        }
        else
        {
            rebindingOperation = InputActionForRebind.action.PerformInteractiveRebinding(controlsFromActions)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(InputActionForRebind, controlsFromActions, bindingDisplayNameText))
            .Start();
        }
        
    }
    private void RebindComplete(InputActionReference ActionForRebind, int TheControlsFromActions, TMP_Text bindingDisplayNameText)
    {
        
        int bindingIndex = ActionForRebind.action.GetBindingIndexForControl(ActionForRebind.action.controls[TheControlsFromActions]);
        Debug.Log(bindingIndex);
        bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            ActionForRebind.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);

        playerMovement.PlayerInput.SwitchCurrentActionMap("PlayerActions");
    }

    
}

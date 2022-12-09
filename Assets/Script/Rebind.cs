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
    [SerializeField] private InputActionReference attackAction = null;
    [SerializeField] private InputActionReference changeTimeAction = null;
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private TMP_Text jumpDisplayNameText = null;
    [SerializeField] private TMP_Text upDisplayNameText = null;
    [SerializeField] private TMP_Text downDisplayNameText = null;
    [SerializeField] private TMP_Text rightDisplayNameText = null;
    [SerializeField] private TMP_Text leftDisplayNameText = null;
    [SerializeField] private TMP_Text attackDisplayNameText = null;
    [SerializeField] private TMP_Text changeTimeDisplayNameText = null;
    [SerializeField] private GameObject startRebindObject = null;
    [SerializeField] private GameObject waitingForInputObject = null;

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


        jumpDisplayNameText.text = QwertyToAzerty(InputControlPath.ToHumanReadableString(
            jumpAction.action.bindings[jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0])].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
        attackDisplayNameText.text = QwertyToAzerty(InputControlPath.ToHumanReadableString(
            attackAction.action.bindings[attackAction.action.GetBindingIndexForControl(attackAction.action.controls[0])].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
        changeTimeDisplayNameText.text = QwertyToAzerty(InputControlPath.ToHumanReadableString(
            changeTimeAction.action.bindings[changeTimeAction.action.GetBindingIndexForControl(changeTimeAction.action.controls[0])].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
        upDisplayNameText.text = QwertyToAzerty(InputControlPath.ToHumanReadableString(
            mouvementAction.action.bindings[mouvementAction.action.GetBindingIndexForControl(mouvementAction.action.controls[0])].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
        downDisplayNameText.text = QwertyToAzerty(InputControlPath.ToHumanReadableString(
            mouvementAction.action.bindings[mouvementAction.action.GetBindingIndexForControl(mouvementAction.action.controls[1])].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
        rightDisplayNameText.text = QwertyToAzerty(InputControlPath.ToHumanReadableString(
            mouvementAction.action.bindings[mouvementAction.action.GetBindingIndexForControl(mouvementAction.action.controls[2])].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
        leftDisplayNameText.text = QwertyToAzerty(InputControlPath.ToHumanReadableString(
            mouvementAction.action.bindings[mouvementAction.action.GetBindingIndexForControl(mouvementAction.action.controls[3])].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
    }

    public void Save()
    {
        string rebinds = playerMovement.PlayerInput.actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString(RebindsKey, rebinds);
    }
    public void StartRebinding(string ButtonRebind)
    {
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
            case "attack":
                InputActionForRebind = attackAction;
                bindingDisplayNameText = attackDisplayNameText;
                break;
            case "changeTime":
                InputActionForRebind = changeTimeAction;
                bindingDisplayNameText = changeTimeDisplayNameText;
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

        string QwertyCaracter = InputControlPath.ToHumanReadableString(
            ActionForRebind.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        bindingDisplayNameText.text = QwertyToAzerty(QwertyCaracter);
        rebindingOperation.Dispose();

        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);

        playerMovement.PlayerInput.SwitchCurrentActionMap("PlayerActions");
    }

    private string QwertyToAzerty(string QwertyCaracterToAzerty)
    {
        
        switch (QwertyCaracterToAzerty)
        {
            case "Q":
                QwertyCaracterToAzerty = "A";
                break;
            case "W":
                QwertyCaracterToAzerty = "Z";
                break;
            case "A":
                QwertyCaracterToAzerty = "Q";
                break;
            case ";":
                QwertyCaracterToAzerty = "M";
                break;
            case "Z":
                QwertyCaracterToAzerty = "W";
                break;
            case "M":
                QwertyCaracterToAzerty = ",";
                break;
        }
        return QwertyCaracterToAzerty;
    }
    
}

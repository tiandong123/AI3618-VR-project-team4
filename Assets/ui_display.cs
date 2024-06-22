using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
public class UIToggleController : MonoBehaviour
{
    public GameObject uiPanel; // 引用 UI 界面的 GameObject
    public GameObject aimPanel;
    private bool isUIActive = false;
    private bool isaimActive = true;
    Myinputactions actions;

    void Awake()
    {
        // 实例化Action包装类
        actions = new Myinputactions();
        // 直接通过api使用jump动作，绑定回调
        actions.basic.uidisplay.performed += Ondisplay;
        uiPanel.SetActive(isUIActive);
        DisableUIInteraction();
    }
    public bool _isUIActive(){
        return isUIActive;
    }
    private void Ondisplay(InputAction.CallbackContext context)
    {
        Debug.Log("uidisplay or undisplay");
        ToggleUI();
    }
    private void ToggleUI()
    {
        isUIActive = !isUIActive;
        isaimActive = !isaimActive;
        uiPanel.SetActive(isUIActive);
        aimPanel.SetActive(isaimActive);
        // 根据 isUIActive 状态设置 UI 的交互性
        if (uiPanel.activeSelf)
        {
            // 启用 UI 元素的交互性
            EnableUIInteraction();
        }
        else
        {
            // 禁用 UI 元素的交互性
            DisableUIInteraction();
        }
    }

    private void EnableUIInteraction()
    {
        // 遍历 UI 元素并启用其交互性
        foreach (var uiElement in uiPanel.GetComponentsInChildren<Selectable>())
        {
            uiElement.interactable = true;
        }
    }

    private void DisableUIInteraction()
    {
        // 遍历 UI 元素并禁用其交互性
        foreach (var uiElement in uiPanel.GetComponentsInChildren<Selectable>())
        {
            uiElement.interactable = false;
        }
    }
    void OnEnable()
    {
        actions.basic.Enable();
    }
    void OnDisable()
    {
        actions.basic.Disable();
    }
}
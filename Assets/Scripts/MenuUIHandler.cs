using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        MainManager.Instance.TeamColor = color;
    }

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;
        ColorPicker.SelectColor(MainManager.Instance.TeamColor); // Pre-select color from main manager if there is one
    }

    public void StartNew() {
        SceneManager.LoadScene(1);
    }

    public void Exit() {
        MainManager.Instance.SaveColor(); // Save last selected colour
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode(); // Exit play mode if compiling in the editor
#else
        Application.Quit(); // Exit game normally
#endif
    }

    public void SaveColorClicked() {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked() {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}

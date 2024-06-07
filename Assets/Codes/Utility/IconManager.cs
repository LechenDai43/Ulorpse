using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class IconManager : MonoBehaviour
{
    public Button GuordButton, BodyButton;
    public GameObject ExilirListPanelPrefab, BodyPanelPrefab;
    public GameObject MainPanel;
    // Start is called before the first frame update
    public void ClickGourdButton() {
        MainPanel.SetActive(true);
        GameObject newItem = Instantiate(ExilirListPanelPrefab, MainPanel.transform);
        newItem.GetComponent<ExilirPanelManager>().IconManager = this;
        GuordButton.interactable = false;
        BodyButton.interactable = false;
    }

    public void ClickBodyButton() {
        MainPanel.SetActive(true);
        GameObject newItem = Instantiate(BodyPanelPrefab, MainPanel.transform);
        newItem.GetComponent<ExilirPanelManager>().IconManager = this;
        GuordButton.interactable = false;
        BodyButton.interactable = false;
    }

    public void OpenedPanelIsClosing() {
        GuordButton.interactable = true;
        BodyButton.interactable = true;

    }
}

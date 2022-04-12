using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    
    [Header("Buttons")]
    [SerializeField] private Button friendsMenuButton;
    [SerializeField] private Button closeFriendsMenuButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Button lobbySettingsButton;
    [SerializeField] private Button closeLobbySettingsButton;
    
    [Header("Panels")]
    [SerializeField] private GameObject friendsMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject lobbySettingsPanel;
    
    [NonSerialized]
    public MenuManager Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Set listeners for buttons
        friendsMenuButton.onClick.AddListener(OpenFriendsMenu);
        closeFriendsMenuButton.onClick.AddListener(CloseFriendsMenu);
        
        settingsButton.onClick.AddListener(OpenSettings);
        closeSettingsButton.onClick.AddListener(CloseSettings);
        
        lobbySettingsButton.onClick.AddListener(OpenLobbySettings);
        closeLobbySettingsButton.onClick.AddListener(CloseLobbySettings);
        
        
        // Hide additional menus
        friendsMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        lobbySettingsPanel.SetActive(false);
    }

    public void OpenFriendsMenu()
    {
        friendsMenuPanel.SetActive(true);
    }

    public void CloseFriendsMenu()
    {
        friendsMenuPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenLobbySettings()
    {
        lobbySettingsPanel.SetActive(true);
    }
    
    public void CloseLobbySettings()
    {
        lobbySettingsPanel.SetActive(false);
    }
}

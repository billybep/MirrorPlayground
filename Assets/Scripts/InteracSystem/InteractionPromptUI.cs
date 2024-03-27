using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private TMP_Text _promptText;
    

    void Start()
    {
        _mainCam = Camera.main;

        if (_mainCam != null)
        {
            Debug.Log("Main camera berhasil didapatkan: " + _mainCam.name);
        }
        else
        {
            Debug.LogError("Main camera tidak ditemukan dalam scene!");
        }
    }

    void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform
            .LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public bool IsDisplayed = false;

    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        _uiPanel.SetActive(false);
        IsDisplayed = false;
    }
}

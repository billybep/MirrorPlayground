using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    [SerializeField] private PlayerMovement playerMovementScript;

    [SerializeField] private GameObject _chatGPTUI;

    private readonly Collider[] _collider = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    void Start()
    {
        PlayerMovement playerMovementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        _numFound = Physics
            .OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _collider, _interactableMask);


        if (_numFound > 0)
        {
            _interactable = _collider[0].GetComponent<IInteractable>();
            if (_interactable != null)
            {
                if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(_interactable.InteractionPrompt);

                if (Keyboard.current.fKey.wasPressedThisFrame)
                {
                    _interactable.Interact(this);
                    LockPlayerMovement();
                }
            }
        }
        else
        {
            if(_interactable != null) _interactable = null;

            if(_interactionPromptUI.IsDisplayed)
            {
                _interactionPromptUI.Close();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _chatGPTUI = GameObject.Find("UI_ChatGPT");

            if (_chatGPTUI != null ) _chatGPTUI.SetActive(false);
            else Debug.LogError("ChatGPT UI not Found!");
            UnlockPlayerMovement();
        }

    }

    private void OnDrawGizmos()
    {
        Debug.Log("DrawGizmos");
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

    public void LockPlayerMovement()
    {
        Debug.Log("LockPlayerMovement");
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }
        else
        {
            Debug.LogError("PlayerMovement not found");
        }
    }

    public void UnlockPlayerMovement()
    {
        Debug.Log("UnlockPlayerMovement");
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
        else
        {
            Debug.LogError("PlayerMovement not found");
        }
    }
}


using UnityEngine;
using Mirror;
using UnityEngine.UI;
using StarterAssets;
using TMPro;

public class PlayerScript : NetworkBehaviour
{
    public TextMesh playerNameText;
    public TMP_InputField playerInputName;
    public GameObject floatingInfo;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;
    private PlayerMovement playerMovementScript;
    public GameObject playerNameUI;

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName;
    }

    public string GetName()
    {
        return playerName;
    }

    public override void OnStartLocalPlayer()
    {
        floatingInfo.transform.localPosition = new Vector3(0, 2.3f, 0.1f);
        floatingInfo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        string name = " ";
        CmdSetupPlayer(name);

        // Menemukan dan mengunci script PlayerMovement
        playerMovementScript = GetComponent<PlayerMovement>();
        playerMovementScript.enabled = false;

        // Menampilkan UI untuk input nama pemain hanya pada pemain lokal
        if (isLocalPlayer)
        {
            playerNameUI.SetActive(true);
        }

    }

    // Fungsi untuk menetapkan nama pemain setelah diinput dari UI
    public void SetPlayerName()
    {

        CmdSetupPlayer(playerInputName.text);

        // Menyembunyikan UI input nama pemain
        playerNameUI.SetActive(false);

        // Membuka kembali script PlayerMovement
        playerMovementScript.enabled = true;
    }

    [Command]
    public void CmdSetupPlayer(string _name)
    {
        playerName = _name;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            floatingInfo.transform.LookAt(Camera.main.transform);
            // LockPlayerNameTextRotationY();
            return;
        }
    }

    void LockPlayerNameTextRotationY()
    {
         // Mengunci rotasi pada sumbu Y agar tetap menghadap kamera
        Quaternion originalRotation = floatingInfo.transform.rotation;
        floatingInfo.transform.LookAt(Camera.main.transform);
        floatingInfo.transform.rotation = Quaternion.Euler(originalRotation.eulerAngles.x, floatingInfo.transform.rotation.eulerAngles.y, originalRotation.eulerAngles.z);
    }
}

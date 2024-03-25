
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

    public GameObject sb;

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
        floatingInfo.transform.localPosition = new Vector3(0, 2.1f, 0.1f);
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

        // Panggil fungsi untuk memperbarui scoreboard di semua klien
        UpdateScoreboardForAllClients();

        // GameObject scriptScoreBoard = GameObject.Find("ScoreBoard");
        // if (scriptScoreBoard != null)
        // {
        //     ScoreBoard scoreBoardComponent = scriptScoreBoard.GetComponent<ScoreBoard>();
        //     if (scoreBoardComponent != null)
        //     {
        //         // Cari GameObject "Score" di dalam GameObject "ScoreBoard"
        //         Transform scoreTransform = scoreBoardComponent.transform.Find("Canvas/Score");

        //         // Periksa apakah GameObject "Score" ditemukan
        //         if (scoreTransform != null)
        //         {
        //             // Aktifkan GameObject "Score"
        //             scoreTransform.gameObject.SetActive(true);
        //         }
        //         else
        //         {
        //             Debug.LogError("GameObject Score tidak ditemukan di dalam ScoreBoard");
        //         }

        //     }
        //     else
        //     {
        //         Debug.Log("Tidak ditemukan komponen ScoreBoard pada GameObject");
        //     }
        // }
        // else
        // {
        //     Debug.Log("Tidak ditemukan GameObject ScoreBoard");
        // }
    }

    [Command]
    public void CmdSetupPlayer(string _name)
    {
        playerName = _name;
    }

    // Memanggil fungsi UpdateScoreboard pada semua klien
    [Command]
    void CmdUpdateScoreboard()
    {
        RpcUpdateScoreboard();
    }

    // Memanggil fungsi UpdateScoreboard pada semua klien
    [ClientRpc]
    void RpcUpdateScoreboard()
    {
        // Mencari skrip ScoreBoard dan memanggil metode UpdateScoreboard
        GameObject scriptScoreBoard = GameObject.Find("ScoreBoard");
        if (scriptScoreBoard != null)
        {
            ScoreBoard scoreBoardComponent = scriptScoreBoard.GetComponent<ScoreBoard>();
            
            if (scoreBoardComponent != null)
            {
                Debug.Log("UPdate screboard rpc");
                scoreBoardComponent.UpdateScoreboard();

                // // Cari GameObject "Score" di dalam GameObject "ScoreBoard"
                // Transform scoreTransform = scoreBoardComponent.transform.Find("Canvas/Score");

                // // Periksa apakah GameObject "Score" ditemukan
                // if (scoreTransform != null)
                // {
                //     // Aktifkan GameObject "Score"
                //     scoreTransform.gameObject.SetActive(true);
                // }
                // else
                // {
                //     Debug.LogError("GameObject Score tidak ditemukan di dalam ScoreBoard");
                // }

                // scoreBoardComponent.UpdateScoreboard();
            }
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            floatingInfo.transform.localPosition = new Vector3(0, 2f, 0.1f);
            floatingInfo.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            floatingInfo.transform.LookAt(Camera.main.transform);
            floatingInfo.transform.Rotate(0, 180, 0);
            return;
        }
        else
        {
            // floatingInfo.transform.LookAt(Camera.main.transform);
            floatingInfo.transform.LookAt(Camera.main.transform);
            floatingInfo.transform.Rotate(0, 180, 0);
            playerNameText.color = Color.blue;
            return;
        }
    }

    // Panggil fungsi ini untuk memperbarui scoreboard di semua klien
    void UpdateScoreboardForAllClients()
    {
        if (isServer)
        {
            // Siapkan pesan kustom Anda
            // CustomMessage message = new CustomMessage();
            // message.someData = "RPC UPDATE SCOREBOARD";

            // Kirim pesan ke semua klien yang terhubung
            // NetworkServer.SendToAll(message);
        }
        else if (isClient)
        {
            CmdUpdateScoreboard();
        }
    }

    // Dipanggil saat pemain keluar dari permainan
    public override void OnStopClient()
    {
        base.OnStopClient();
        Debug.Log("Player Out Update ScoreBoard");
        CmdUpdateScoreboard();
    }
}

// Pesan kustom Anda harus turunan dari MessageBase
// public class CustomMessage : MessageBase
// {
//     public string someData;
// }
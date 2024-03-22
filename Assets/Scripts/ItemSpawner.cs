using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class ItemSpawner : NetworkBehaviour
{

    public GameObject itemPrefab;
    public int numberOfItems = 20;
    [SerializeField] private List<GameObject> spawnedObjects = new List<GameObject>();

    public override void OnStartServer()
    {
        Debug.Log("Spawn Item Random");

        for (int i = 0; i < numberOfItems; i ++)
        {
            // GameObject spawnItem = Instantiate(itemPrefab, RandomPosition(), Quaternion.identity);
            // NetworkServer.Spawn(spawnItem);

            // Debug.Log("Item " + i+1);
            SpawnItems();
        }
    }

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (!isServer)
            {
                // Request information about all existing objects when client starts
                CmdRequestSpawnedObjects();
            }
        }

        [Command]
        private void CmdRequestSpawnedObjects()
        {
            foreach (GameObject obj in spawnedObjects)
            {
                // Inform the late-joining client about each existing object
                TargetSpawnObject(connectionToClient, obj);
            }
        }

        [TargetRpc]
        private void TargetSpawnObject(NetworkConnection target, GameObject obj)
        {
            // Spawn existing objects on the late-joining client
            NetworkServer.Spawn(obj, target);
        }

        private void SpawnItems()
        {
            GameObject obj = Instantiate(itemPrefab, RandomPosition(), Quaternion.identity);
            NetworkServer.Spawn(obj);
            spawnedObjects.Add(obj);
        }


        Vector3 RandomPosition()
        {
            float xPos = Random.Range(-10f, 20f);
            float zPos = Random.Range(2f, 30f);
            float yPos = 2f;
            return new Vector3(xPos, yPos, zPos);
    }
}

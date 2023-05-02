using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public int totalCarrotsInLevel;
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.carrotsCollected >= totalCarrotsInLevel)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}



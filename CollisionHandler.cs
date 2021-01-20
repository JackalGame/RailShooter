using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Player Death")]
    [Tooltip("In seconds")][SerializeField] float levelReloadTime = 2;
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject[] jets;
    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        FindObjectOfType<LevelLoader>().ReloadLevel(levelReloadTime); 
        deathFX.SetActive(true);
        meshRenderer.enabled = false;
        TurnOffJets();
    }

    private void TurnOffJets()
    {
        foreach(GameObject jet in jets)
        {
            jet.SetActive(false);
        }
    }
}
 
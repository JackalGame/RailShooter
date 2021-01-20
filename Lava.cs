using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] int activationDistance = 500;
    [SerializeField] GameObject[] particlesToBeTriggered;
    PlayerController playerShip;
    

    void Start()
    {
        playerShip = FindObjectOfType<PlayerController>(); 
    }


    void Update()
    {
        float distance = Vector3.Distance(playerShip.transform.position, transform.position);
        
        if(distance < activationDistance)
        {
            ActivateLava(true);
        }
        else
        {
            ActivateLava(false);
        }

    }

    void ActivateLava(bool isActive)
    {
        foreach(GameObject par in particlesToBeTriggered)
        {
            par.SetActive(isActive);
        }
    }
}

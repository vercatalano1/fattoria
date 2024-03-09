using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameCrossOver : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Text PlayernameTextHolder;
    bool Updatedname = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateName();
     }

    private void UpdateName()
    {
        if(gameManager ==null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
        }
        if (gameManager != null && Updatedname==false)
        {
            PlayernameTextHolder.text = gameManager.playerName;
            print("Hello");
            Updatedname = true;
        }
    }
}

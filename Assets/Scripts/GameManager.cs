using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private Renderer playerRenderer;  //added
    [SerializeField] private TextMeshProUGUI gameOverTextMesh;
    
    public GameObject player;
    private readonly List<Renderer> _pickupRenderers = new List<Renderer>();
    bool _isGameOver;
    
    //Singleton
    public static GameManager Instance { get; private set; }  //property
    //static bc it belongs to class
    //get can be accessed from outside the class, but not set

    private int _score = 0;
    private int _winscore = 0;
    private GameObject _player;
    private GameObject[] _pickups;

    void Awake()  //runs before start
    {
        //an instance has already been assigned
        if (Instance != null)  //if NOT empty
        {
            Destroy(this);   //destroy current instance in favor of pre-existing one
            return; //exit code
        }
        
        Instance = this;  //instance is equal to original instance
        
        //my little pony, my little pony,aaaahahahahahahaha my little pony, i used to wonder what friendship could be! (My little pony) And to you all shared its magic with me!
        //Great adventure, tons of fun! a beautiful heart faithful and strong, sharing kindness, its an easy feat!
        //
    }

    public void IncrementScore(Renderer pickupRenderer)
    {
        if (_isGameOver)
            return;
        
        _score++;
        uiController.UpdateScoreDisplay(_score);
        _pickupRenderers.Remove(pickupRenderer);
        
        if (_score >= _winscore)
        {
            _isGameOver = true;
            uiController.DisplayYouWin();
            Invoke(nameof(ReturnToMainMenu), 3f);
            return;
        }
        
        CheckLoseState();
        
    }
    
    private void Start()
    {
       InitialisePickupRenderers();
       _winscore = _pickupRenderers.Count;
    }

    private void InitialisePickupRenderers()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

        for (int i = 0; i < pickups.Length; i++)
        {
            _pickupRenderers.Add(pickups[i].GetComponentInChildren<Renderer>());
        }
    }

    private void CheckLoseState()
    {
        if (_isGameOver)
        {
            return;
        }
        
        foreach (Renderer pickupRenderer in _pickupRenderers)
        {
            if (pickupRenderer.material.color != playerRenderer.material.color)
                return;
        }
        
        _isGameOver = true;
        uiController.DisplayGameOver();
        Invoke(nameof(ReturnToMainMenu), 3f);
    }

    private void ReturnToMainMenu()
    {
        //wait 3 secs
        //return to main menu
        SceneManager.LoadScene("MainMenu");
    }
    
}

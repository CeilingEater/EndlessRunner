using System;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    //boss spawn
    public GameObject bossPrefab;
    public Transform spawnPoint;
    public GameObject bossEnd;

    //shockwavs
    public GameObject shockwavePrefab;
    public Transform shockwaveSpawnPoint;
    public float delayBeforeFirstAttack = 2f;
    public float delayBetweenShockwaves = 5f;
    public float shockwaveSpeed = 25f;
    public int totalShockwaves = 3;

    //move up down
    public float floatHeight = 5f;
    public float floatSpeed = 2f;
    public float levelDuration = 50f;

    private GameObject instantiatedBoss;
    private float levelTimer = 0f;
    private bool bossSpawned = false;

    private float attackDelayTimer = 0f;
    private float shockwaveDelayTimer = 0f;
    private int shockwavesSent = 0;
    private bool attackStarted = false;
    private Vector3 bossStartPos;
    
    private List<GameObject> activeShockwaves = new List<GameObject>();

    void Update()
    {
        //timeElapsed += Time.deltaTime;
        levelTimer += Time.deltaTime;

        if (!bossSpawned && levelTimer >= levelDuration / 2f)
        {
            SpawnBoss();
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.PlayBossMusic();
            }
        }


        if (instantiatedBoss != null)
        {
            HandleBossFloat();

            if (!attackStarted)
            {
                attackDelayTimer += Time.deltaTime;
                if (attackDelayTimer >= delayBeforeFirstAttack)
                {
                    attackStarted = true;
                    attackDelayTimer = 0f;
                }
            }
            else
            {
                
                shockwaveDelayTimer += Time.deltaTime;

                if (shockwavesSent < totalShockwaves && shockwaveDelayTimer >= delayBetweenShockwaves)
                {
                    SpawnShockwave();
                    shockwaveDelayTimer = 0f;
                    shockwavesSent++;
                }

                if (shockwavesSent >= totalShockwaves)
                {
                    Destroy(instantiatedBoss, 2f);
                    instantiatedBoss = null;
                }
            }
            
        }
    }

    private void FixedUpdate()
    {
        MoveShockwaves();
    }

    private void SpawnBoss()
    {
        bossSpawned = true;
        instantiatedBoss = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        instantiatedBoss.transform.Rotate(0, 90, 0);
        bossStartPos = instantiatedBoss.transform.position;
        GameEvents.RaiseBossSpawned();
    }

    private void HandleBossFloat()
    {
        if (instantiatedBoss == null) return;
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        Vector3 floatPos = new Vector3(bossStartPos.x, bossStartPos.y + yOffset, bossStartPos.z);
        instantiatedBoss.transform.position = floatPos;
    }

    private void SpawnShockwave()
    {
        GameObject wave = Instantiate(shockwavePrefab, shockwaveSpawnPoint.position, Quaternion.identity);
        activeShockwaves.Add(wave);
        Destroy(wave, 10f);
    }

    private void MoveShockwaves()
    {
        for (int i = activeShockwaves.Count - 1; i >= 0; i--)
        {
            GameObject wave = activeShockwaves[i];
            
            if (wave != null)
            {
                wave.transform.position += Vector3.right * shockwaveSpeed * Time.fixedDeltaTime;

                if (wave.transform.position.x < -100f)
                {
                    Destroy(wave);
                    activeShockwaves.RemoveAt(i);
                }
            }
            else
            {
                activeShockwaves.RemoveAt(i);
            }
        }
    }
    
}

using UnityEngine;

public class Boss2 : MonoBehaviour
{
    [SerializeField] private float delayBeforeFirstAttack = 2f;
    
    public GameObject shockwavePrefab;
    public Transform shockwaveSpawnPoint;
    public float delayBetweenShockwaves = 1.5f;
    public int totalShockwaves = 3;
    public float shockwaveSpeed = 10f;

    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;

    private int shockwavesSent = 0;
    
    private float timer = 0f;
    private Vector3 startPos;
    private bool isActive = false;

    void Start()
    {
        startPos = transform.position;
        isActive = true;
        GameEvents.RaiseBossSpawned();
    }

    void Update()
    {
        if (!isActive) return;

        // jump
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, startPos.y + yOffset, startPos.z);

        
        timer += Time.deltaTime;

        if (shockwavesSent < totalShockwaves && timer >= delayBetweenShockwaves)
        {
            SendShockwave();
            timer = 0f;
            shockwavesSent++;
        }

        
        if (shockwavesSent >= totalShockwaves)
        {
            Destroy(gameObject, 2f); 
            isActive = false;
        }
    }

    private void SendShockwave()
    {
        GameObject wave = Instantiate(shockwavePrefab, shockwaveSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = wave.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = Vector3.right * shockwaveSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit shockwave");
        }
    }
}

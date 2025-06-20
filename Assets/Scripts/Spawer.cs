using UnityEngine;

public class spawer : MonoBehaviour
{
    private Vector3 originalPosition;
    public GameObject coinPrefab; 
    public float coinSpawnOffset = 1f; 

    private bool hasBeenHit = false;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Untagged") && !hasBeenHit)
        {
            
            SpawnCoin();

        }
    }

    void SpawnCoin()
    {
        if (coinPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + coinSpawnOffset, transform.position.z);

            GameObject spawnedCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);    
        }
       
    }
    
}
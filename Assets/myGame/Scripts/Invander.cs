using UnityEngine;

public class Invander : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
      public System.Action killed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            // Debug.Log("Invader hit by projectile!");
            this.killed?.Invoke();
            // Destroy(other.gameObject);
            Destroy(this.gameObject);


            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX("KillAudio");
            }
        }

    }
}

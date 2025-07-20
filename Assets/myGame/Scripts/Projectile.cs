using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 25;
    public Vector3 direction = Vector3.up;
    public System.Action  onDestroy;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         this.transform.position += this.transform.right * this.speed * Time.deltaTime;
         
    }

    private void  OnTriggerEnter2D(Collider2D other) {
        this.onDestroy?.Invoke();
        Destroy(this.gameObject);
    }
}

using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
       float MoveX = Input.GetAxis("Horizontal"); 
        float MoveY = Input.GetAxis("Vertical"); 
        transform.position += new Vector3(MoveX, MoveY,0) * moveSpeed * Time.deltaTime;
    }
}

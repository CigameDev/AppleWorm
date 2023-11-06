using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadWorm : MonoBehaviour
{
    private Worm worm;

    [SerializeField] private GameObject segmentWormPrefab;

    private void Awake()
    {
        worm = GetComponentInParent<Worm>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(!CanMove(Vector2.right))
            {
                return;
            }    
            worm.Move();
            this.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!CanMove(Vector2.left))
            {
                return;
            }
            worm.Move();
            this.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!CanMove(Vector2.up))
            {
                return;
            }
            worm.Move();
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!CanMove(Vector2.down))
            {
                return;
            }
            worm.Move();
            this.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);

            var segment = Instantiate(segmentWormPrefab,worm.gameObject.transform);
            segment.transform.position = transform.position;
            transform.position = new Vector3(transform.position.x +1, transform.position.y, transform.position.z);
            worm.AddSegmentWorm(segment);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Debug.Log("GameOver");
        }
    }
    private bool CanMove(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,1f);
        if(hit.collider != null)
        {
            Debug.Log("Khong the di chuyen "+hit.collider.gameObject.name);
            return false;
        }    
        return true;
    }    
}

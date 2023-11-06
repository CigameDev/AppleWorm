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
            this.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            worm.Move();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            worm.Move();
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            worm.Move();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            worm.Move();
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
}

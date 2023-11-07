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
            if(CanEat(Vector2.right))
            {
                Eat(Vector2.right);
                return;
            }
            if (!CanMove(Vector2.right))
            {
                return;
            }    
            worm.Move();
            this.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CanEat(Vector2.left))
            {
                Eat(Vector2.left);
                return;
            }

            if (!CanMove(Vector2.left))
            {
                return;
            }
            worm.Move();
            this.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CanEat(Vector2.up))
            {
                Eat(Vector2.up);
                return;
            }

            if (!CanMove(Vector2.up))
            {
                return;
            }
            worm.Move();
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CanEat(Vector2.down))
            {
                Eat(Vector2.down);
                return;
            }

            if (!CanMove(Vector2.down))
            {
                return;
            }
            worm.Move();
            this.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
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

        Vector2 temp = new Vector2(transform.position.x +direction.x, transform.position.y +direction.y);
        Collider2D col = Physics2D.OverlapCircle(temp, 0.2f);
        if (col == null) return true;
        return false;
    } 
    private bool CanEat(Vector2 direction)
    {
        Vector2 temp = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        Collider2D col = Physics2D.OverlapCircle(temp, 0.2f);
        if (col != null && col.gameObject.CompareTag("Apple")) return true;
        return false;
    }    

    private string GetTag(Vector2 direction)
    {
        Collider2D col = GetCollider2D(direction);
        if(col != null) return col.gameObject.tag;
        return "";
    }    
    private Collider2D GetCollider2D(Vector2 direction)
    {
        Vector2 temp = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
        Collider2D col = Physics2D.OverlapCircle(temp, 0.2f);
        if (col != null)
        {
            return col;
        }
        return null;
    }
    private void Eat(Vector2 direction)
    {
        Collider2D col = GetCollider2D(direction);
        string tagName = GetTag(direction);
        if(tagName != "Apple" )
        {
            return;
        }    
        Destroy(col.gameObject);
        
        var segment = Instantiate(segmentWormPrefab, worm.gameObject.transform);
        segment.transform.position = transform.position;
        transform.position = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z);
        worm.AddSegmentWorm(segment);
    }

    private bool CheckLose()
    {
        if (CanMove(Vector2.right)) return false;
        if (CanMove(Vector2.left)) return false;
        if (CanMove(Vector2.down)) return false;
        if (CanMove(Vector2.up)) return false;

        return true;
    }    
}

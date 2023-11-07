using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] private List<GameObject> segmentWorms;

    public void AddSegmentWorm(GameObject segment)
    {
        segmentWorms.Insert(1, segment);
    }    
    public void Move()
    {
        for(int i = segmentWorms.Count -1;i >= 1;i --)
        {
            segmentWorms[i].transform.position = segmentWorms[i-1].transform.position;
        }
    }

    public void WormFall(bool isFall)
    {
        for(int i =0;i < segmentWorms.Count;i++)
        {
            Rigidbody2D rb = segmentWorms[i].GetComponent<Rigidbody2D>();
            rb.isKinematic = isFall;
        }
    }    
}

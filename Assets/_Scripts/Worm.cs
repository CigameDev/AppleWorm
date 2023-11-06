using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [SerializeField] private List<GameObject> segmentWorms;

    public void AddSegmentWorm(GameObject segment)
    {
        segmentWorms.Add(segment);
    }    
    public void Move()
    {
        for(int i = segmentWorms.Count -1;i >= 1;i --)
        {
            segmentWorms[i].transform.position = segmentWorms[i-1].transform.position;
        }
    }
}

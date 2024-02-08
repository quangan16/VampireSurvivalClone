using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] public ChunkID chunkID;
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            ChunkController currentChunk = MapManager.Instance.currentChunk;
            if (TryGetComponent(out ChunkController chunkCtl) 
                && chunkCtl.chunkID != currentChunk.chunkID)
            {
                MapManager.Instance.currentChunk = chunkCtl;
                MapManager.Instance.ShiftColumn(chunkID);
                
            }
        }
        
       
    }
}

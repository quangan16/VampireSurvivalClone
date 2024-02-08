using System;
using UnityEditor;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private Row[] chunkRows;
    [SerializeField] public ChunkController currentChunk;

    // [ContextMenu("lol")]
    public void ShiftColumn(ChunkID id)
    {
        
        switch (id)
        {
            case ChunkID.UP_CENTER:
            {
                foreach (var item in chunkRows[2].ChunkItems)
                {
                    item.transform.position += Vector3.up * 96;
                }
                
                Row rowTemp = chunkRows[2];
                chunkRows[2] = chunkRows[1];
                chunkRows[1] = chunkRows[0];
                chunkRows[0] = rowTemp;
               
               
                break;
            }

            case ChunkID.DOWN_CENTER:
            {
                foreach (var item in chunkRows[0].ChunkItems)
                {
                    item.transform.position -= Vector3.up * 96;
                }
                Row rowTemp = chunkRows[0];
                chunkRows[0] = chunkRows[1];
                chunkRows[1] = chunkRows[2];
                chunkRows[2] = rowTemp;
               
                break;
            }
               
        case ChunkID.CENTER_LEFT:
        {
            foreach (var row in chunkRows)
            {
                ChunkController temp;
                row.ChunkItems[2].transform.position -= Vector3.right * 96;
                temp = row.ChunkItems[2];
                row.ChunkItems[2] = row.ChunkItems[1];
                row.ChunkItems[1] = row.ChunkItems[0];
                row.ChunkItems[0] = temp;
                
            }

            break;
        }
        
        case ChunkID.CENTER_RIGHT:
        {
            foreach (var row in chunkRows)
            {
                row.ChunkItems[0].transform.position += Vector3.right * 96;
                ChunkController temp;
                temp = row.ChunkItems[0];
                row.ChunkItems[0] = row.ChunkItems[1];
                row.ChunkItems[1] = row.ChunkItems[2];
                row.ChunkItems[2] = temp;
                
            }
            break;
        }
            
        }

        EvaluateChunkIDHorizontal();
    }

    
    public void EvaluateChunkIDHorizontal()
    {
        int id = 0;
        foreach (var rows in chunkRows)
        {
            foreach (var rowItem in rows.ChunkItems)
            {
                
                rowItem.chunkID = (ChunkID)id++;
            }
        }
        
    }

 

    
}


[Serializable]
public class Row
{
    [SerializeField] public ChunkController[] ChunkItems;
}

public enum ChunkID
{
    UP_LEFT,
    UP_CENTER,
    UP_RIGHT,
    CENTER_LEFT,
    CENTER,
    CENTER_RIGHT,
    DOWN_LEFT,
    DOWN_CENTER,
    DOWN_RIGHT
}

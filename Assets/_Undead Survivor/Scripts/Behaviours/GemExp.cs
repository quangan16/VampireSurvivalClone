using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemExp : MonoBehaviour
{
    [SerializeField] public int experience;
    [SerializeField] private float gravity;
    
    

    public void OnEmerge(float targetPosY)
    {
        StartCoroutine(Popup(targetPosY));
    }

    public void OnAchieve(Transform target)
    {
        StartCoroutine(FlyToTarget(target));
    }
    
    public IEnumerator Popup(float targetPosY)
    {
        
        float randomAngle = Random.Range(75.0f * Mathf.Deg2Rad, 105.0f * Mathf.Deg2Rad);
        float directionX = 2 * Mathf.Cos(randomAngle);
        float directionY = 4 * Mathf.Sin(randomAngle);
       
        while (transform.position.y >= targetPosY)
        {
            directionY -= gravity * Time.deltaTime;
            Vector3 randomDirection = new Vector3(directionX, directionY);
            transform.position += randomDirection *  Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FlyToTarget(Transform target)
    {
        float time = 0;
        float flyTime = 0.7f;
        Vector2 startPos = transform.position;
        while (time < flyTime || Vector2.Distance(transform.position, target.position) > 0.1f)
        {
            time += Time.deltaTime;
            transform.position =
                Vector2.Lerp(startPos, target.position, 1 - ((1 - time) * 1 - (time)) / flyTime);
            yield return null;
        }

        target.GetComponent<PlayerController>().AddExp(experience);
        UIManager.Instance.UpdateExpBar();
        Destroy(gameObject);
        yield break;
    }
    
}

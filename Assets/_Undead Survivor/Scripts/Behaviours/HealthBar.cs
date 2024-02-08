using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform bar;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        bar.localScale = Vector3.one;
    }

    public void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void UpdateDisplay(float currentHealth)
    {
        bar.localScale = new Vector3(currentHealth, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgText : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1.5f);
        transform.rotation = Camera.main.transform.rotation;
        transform.Translate(Vector3.back);
    }
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}

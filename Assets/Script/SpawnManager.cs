using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public GameObject[] enemys;
    bool isspawn = false;

    // Update is called once per frame
    void Update()
    {
        if(!isspawn)
        StartCoroutine(spawnEnemy());
    }
    IEnumerator spawnEnemy()
    {
        isspawn = true;
        int r = Random.Range(0, enemys.Length);
        Instantiate(enemys[r], point1.position,Quaternion.identity);
        yield return new WaitForSeconds(5);
        r = Random.Range(0, enemys.Length);
        Instantiate(enemys[r], point2.position, Quaternion.identity);
        yield return new WaitForSeconds(15);
        isspawn = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    GameObject[] Objects;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawntroops", 3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawntroops()
    {
        Vector3 pos = this.transform.position;
        pos.y += 0.1f;
        Instantiate(Objects[Random.Range(0,2)], pos, transform.rotation);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delay : MonoBehaviour
{
    public GameObject apl_4;
    public GameObject apl_1_parabs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1);
        apl_4.SetActive(false);
        apl_1_parabs.SetActive(true);
  
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delay_parabens : MonoBehaviour
{
    public GameObject apl_1_parabs;
    public GameObject apl_1_consec;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2);
        apl_1_parabs.SetActive(false);
        apl_1_consec.SetActive(true);
    }

}

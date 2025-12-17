using UnityEngine;
using System.Collections;

public class TerminalScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(func());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator func()
    {
        int i = 0;

        while (true)
        {
            if (i % 2 == 0)
            {
                Debug.Log("o<o");
            }
            else
            {
                Debug.Log("o-o");
            }
            i++;
            yield return new WaitForSeconds(2);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject allCanvasParent;
    [SerializeField] List<GameObject> allCanvases = new List<GameObject>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        foreach(Transform currCanvas in allCanvasParent.transform)
        {
            allCanvases.Add(currCanvas.gameObject);
        }
    }
    
    public void EnableCanvas(GameObject canvas)
    {
        foreach (GameObject currCanvas in allCanvases)
        {
            currCanvas.SetActive(false);
        }

        canvas.SetActive(true);
    }
}

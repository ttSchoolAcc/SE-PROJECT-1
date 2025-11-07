using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject allCanvasParent;
    [SerializeField] List<GameObject> allCanvases = new List<GameObject>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        foreach(GameObject currCanvas in allCanvasParent.transform)
        {
            allCanvases.Add(currCanvas);
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

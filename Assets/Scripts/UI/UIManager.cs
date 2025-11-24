using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine.Timeline;
//using System.Numerics;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject moveTickPrefab;
    [SerializeField] Transform moveTickParent;

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

    public void ClearMarkers()
    {
        while(moveTickParent.childCount > 0)
        {
            DecrementMarkerCount(); //BE CAREFUL, destroy is not immediate, will inf loop if not handled
        }
    }

    public void DecrementMarkerCount()
    {
        if (moveTickParent.childCount > 0)
        {
            GameObject markToDestroy = moveTickParent.GetChild(moveTickParent.childCount - 1).gameObject;
            markToDestroy.transform.SetParent(null); //This "instantly removes" the child so the loop for clearmarkers works()
            Destroy(markToDestroy);
        }
    }
    
    public void IncrementMarkerCount()
    {
        Instantiate(moveTickPrefab, moveTickParent);
    }
}

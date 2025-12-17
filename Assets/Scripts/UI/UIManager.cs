using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine.Timeline;
//using System.Numerics;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject moveTickPrefab;
    [SerializeField] Transform moveTickParent;

    [SerializeField] GameObject allCanvasParent;
    [SerializeField] List<GameObject> allCanvases = new List<GameObject>();

    [SerializeField] Image background;
    [SerializeField] float fadeDuration = 0.5f; // Adjust for faster/slower fades

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

    //public void EnableCanvas(GameObject canvas)
    //{
    //    foreach (GameObject currCanvas in allCanvases)
    //    {
    //        currCanvas.SetActive(false);
    //    }

    //    canvas.SetActive(true);
    //}

    public void EnableCanvas(GameObject canvas)
    {
        foreach (GameObject currCanvas in allCanvases)
            currCanvas.SetActive(false);

        canvas.SetActive(true);

        // Determine target color
        Color32 targetColor;

        switch (canvas.name)
        {
            case "Login":
                targetColor = new Color32(26, 43, 76, 255); // Dark Blue
                break;

            case "Bulletin Board":
                targetColor = new Color32(58, 79, 65, 255); // Deep Olive
                break;

            case "Chat":
                targetColor = new Color32(74, 63, 85, 255); // Desaturated Purple
                break;

            case "Account":
                targetColor = new Color32(46, 46, 51, 255); // Graphite Gray
                break;

            default:
                targetColor = new Color32(75, 75, 75, 255); // Neutral Gray
                break;
        }

        // Start smooth transition
        StopAllCoroutines();
        StartCoroutine(FadeBackground(targetColor));
    }

    private IEnumerator FadeBackground(Color32 targetColor)
    {
        Color startColor = background.color;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            background.color = Color.Lerp(startColor, targetColor, t / fadeDuration);
            yield return null;
        }

        background.color = targetColor; // ensure exact final color
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

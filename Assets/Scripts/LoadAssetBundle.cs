using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundle : MonoBehaviour
{
    [SerializeField] private string url = "";
    [SerializeField] private string assetName = "";
    void Start()
    {
        WWW web = new WWW(url);
        StartCoroutine(WebRequest(web));
    }

    private IEnumerator WebRequest(WWW w3)
    {
        yield return w3;
        while (!w3.isDone)
        {
            yield return null;
        }

        AssetBundle bundle = w3.assetBundle;
        if(w3.error == null)
        {
            GameObject gameObj = (GameObject)bundle.LoadAsset(assetName);
            Instantiate(gameObj);
        }
        else
        {
            Debug.Log(w3.error);
        }
    }
}

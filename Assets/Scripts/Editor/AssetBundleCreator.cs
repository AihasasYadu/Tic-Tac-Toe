using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleCreator : MonoBehaviour
{
    [MenuItem("Assets/Build Assets")]

    public static void BuildAssetBundle()
    {
        string assetBundleDirectory = "Assets/StreamingAssets";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}

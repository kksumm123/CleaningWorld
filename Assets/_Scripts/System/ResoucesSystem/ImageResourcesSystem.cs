using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ImageResources
{
    private static readonly string Label = "Image";

    private Dictionary<GarbageType, Sprite> _cacehdImages = new();

    public async Task Initialize()
    {
        var loadResourceLocationHandles = Addressables.LoadResourceLocationsAsync(Label, typeof(Sprite));
        await loadResourceLocationHandles.Task;

        var loadAssetHandles = new List<AsyncOperationHandle>();
        foreach (var item in loadResourceLocationHandles.Result)
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(item.PrimaryKey);
            handle.Completed += x =>
            {
                var newId = (GarbageType)Enum.Parse(typeof(GarbageType), item.PrimaryKey);
                _cacehdImages[newId] = x.Result;
            };
            loadAssetHandles.Add(handle);
        }
        var groupHandle = Addressables.ResourceManager.CreateGenericGroupOperation(loadAssetHandles);
        if (!groupHandle.IsDone) await groupHandle.Task;

        Addressables.Release(loadResourceLocationHandles);
    }

    public Sprite GetImage(GarbageType garbageType)
    {
        if (!_cacehdImages.ContainsKey(garbageType)) return null;
        return _cacehdImages[garbageType];
    }
}
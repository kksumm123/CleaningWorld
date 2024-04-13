using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class FactorySystem<PrefabType, ID> where PrefabType : RecycleObject
{
    protected abstract string Label { get; }

    private Transform _factoryManager;
    private ObjectPoolSystem _defaultOp;

    protected PrefabType tempObject;
    protected Dictionary<ID, ObjectPoolSystem> objectPools = new();
    protected Dictionary<ID, PrefabType> prefabContainer = new();
    private ObjectPoolSystem _tempOp;

    public async Task Initialize(Transform factoryManager)
    {
        _factoryManager = factoryManager;
        await OnInitialize();
    }

    protected virtual async Task OnInitialize()
    {
        var loadResourceLocationHandles = Addressables.LoadResourceLocationsAsync(Label, typeof(GameObject));
        await loadResourceLocationHandles.Task;

        var loadAssetHandles = new List<AsyncOperationHandle>();
        foreach (var item in loadResourceLocationHandles.Result)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(item.PrimaryKey);
            handle.Completed += x => OnLoadAsset(item.PrimaryKey, x.Result.GetComponent<PrefabType>());
            loadAssetHandles.Add(handle);
        }
        var groupHandle = Addressables.ResourceManager.CreateGenericGroupOperation(loadAssetHandles);
        if (!groupHandle.IsDone) await groupHandle.Task;

        var isEmptyPrefabContainer = prefabContainer.Count == 0;
        if (isEmptyPrefabContainer)
        {
            WoonyMethods.AlertError($"{Label} 그룹 하위에 자원이 존재해야합니다.");
        }
        else
        {
            _defaultOp = objectPools.ElementAt(0).Value;
        }
        Addressables.Release(loadResourceLocationHandles);
    }

    protected abstract void OnLoadAsset(string id, PrefabType prefab);

    protected void InitializeObjectPool(ID id, int defaultSize = 1)
    {
        objectPools[id] = new ObjectPoolSystem(prefabContainer[id], defaultSize, _factoryManager);
    }

    private ObjectPoolSystem GetObjectPool(ID id)
    {
        if (objectPools.TryGetValue(id, out _tempOp))
        {
            return _tempOp;
        }

        WoonyMethods.AlertError($"{this}: 해당 id를 가진 자원은 존재하지 않습니다. id = {id}");
        return null;
    }

    protected PrefabType GetObject(ID iD)
    {
        _tempOp = GetObjectPool(iD);
        if (_tempOp == null)
        {
            return _defaultOp.Get() as PrefabType;
        }
        return _tempOp.Get() as PrefabType;
    }
}

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;

[CreateAssetMenu(
    fileName = "GameAssetReferences",
    menuName = "ScriptableObjects/GameAssetReferences",
    order = 1
)]
public class GameAssetReferences : ScriptableObject
{
    static GameAssetReferences instance;
    static GameAssetReferences Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Addressables
                    .LoadAssetAsync<GameAssetReferences>("Assets/Common/GameAssetReferences.asset")
                    .WaitForCompletion();
            }
            return instance;
        }
    }

    // Assets
    // TODO: use enum and list to remove duplicate code?
    // TODO: required
    [SerializeField]
    AssetReferenceGameObject contextMenu;
    public static AssetReferenceGameObject ContextMenu
    {
        get { return Instance.contextMenu; }
    }

    [SerializeField]
    AssetReferenceGameObject textButton;
    public static AssetReferenceGameObject TextButton
    {
        get { return Instance.textButton; }
    }

    /// <summary>
    /// Just an extension for `Resources.Load`
    /// </summary>
    public static T Load<T>(string path)
        where T : Object
    {
        var t = Resources.Load<T>(path);
        Assert.IsNotNull(t, $"Asset \"{path}\" not found");
        return t;
    }
}

public static class GameAssetReferencesExtensions
{
    public static GameObject InstantiateSync(
        this AssetReferenceGameObject obj,
        Transform parent = null
    )
    {
        return Addressables.InstantiateAsync(obj, parent).WaitForCompletion();
    }
}

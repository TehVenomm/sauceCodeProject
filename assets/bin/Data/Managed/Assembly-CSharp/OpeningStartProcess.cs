using System.Collections;
using UnityEngine;

public class OpeningStartProcess : MonoBehaviour
{
	public OpeningStartProcess()
		: this()
	{
	}

	private IEnumerator Start()
	{
		yield return ResourceSizeInfo.Init();
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: false);
		MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(isShow: false);
		yield return ResourceSizeInfo.OpenConfirmDialog(ResourceSizeInfo.GetOpeningAssetSizeMB(isTutorial: true));
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: true);
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[2]
		{
			"MainCamera",
			"InputManager"
		});
		LoadObject lo_field_map_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapTable");
		LoadObject lo_portal_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapPortalTable");
		LoadObject lo_enemy_pop_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, "FieldMapEnemyPopTable");
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FieldManager>();
		Singleton<FieldMapTable>.Create();
		string csv3 = DataTableManager.Decrypt((lo_field_map_table.loadedObject as TextAsset).get_text());
		Singleton<FieldMapTable>.I.CreateFieldMapTable(csv3);
		csv3 = DataTableManager.Decrypt((lo_portal_table.loadedObject as TextAsset).get_text());
		Singleton<FieldMapTable>.I.CreatePortalTable(csv3);
		csv3 = DataTableManager.Decrypt((lo_enemy_pop_table.loadedObject as TextAsset).get_text());
		Singleton<FieldMapTable>.I.CreateEnemyPopTable(csv3);
		if (Camera.get_main() != null)
		{
			Object.DestroyImmediate(Camera.get_main().get_gameObject());
		}
		ResourceUtility.Realizes(lo_common_prefabs.loadedObjects[0].obj, MonoBehaviourSingleton<AppMain>.I._transform);
		ResourceUtility.Realizes(lo_common_prefabs.loadedObjects[1].obj, MonoBehaviourSingleton<AppMain>.I._transform);
		MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.get_main());
		MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, isEnable: true);
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<WorldMapManager>();
		MonoBehaviourSingleton<AppMain>.I.get_gameObject().AddComponent<FilterManager>();
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: false);
		MonoBehaviourSingleton<AppMain>.I.startScene = string.Empty;
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "Opening");
	}
}

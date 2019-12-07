using System.Collections;
using UnityEngine;

public class AppCloseProcess : MonoBehaviour
{
	private IEnumerator Start()
	{
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, is_stop: true);
		ResourceManager.internalMode = false;
		yield return ResourceSizeInfo.Init();
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_common_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemCommon", new string[2]
		{
			"MainCamera",
			"InputManager"
		});
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			MonoBehaviourSingleton<SoundManager>.I.LoadParmanentAudioClip();
		}
		while (load_queue.IsLoading())
		{
			yield return null;
		}
		GameSceneGlobalSettings.SetOrientation(ingame: false);
		MonoBehaviourSingleton<GameSceneManager>.I.Initialize();
		if (Camera.main != null)
		{
			Object.DestroyImmediate(Camera.main.gameObject);
		}
		ResourceObject[] loadedObjects = lo_common_prefabs.loadedObjects;
		for (int i = 0; i < loadedObjects.Length; i++)
		{
			ResourceUtility.Realizes(loadedObjects[i].obj, MonoBehaviourSingleton<AppMain>.I._transform);
		}
		MonoBehaviourSingleton<AppMain>.I.SetMainCamera(Camera.main);
		MonoBehaviourSingleton<AudioListenerManager>.I.SetFlag(AudioListenerManager.STATUS_FLAGS.CAMERA_MAIN_ACTIVE, isEnable: true);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.INITIALIZE, is_disable: false);
		MonoBehaviourSingleton<AppMain>.I.startScene = string.Empty;
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "AppClose");
		MonoBehaviourSingleton<AppMain>.I.OnLoadFinished();
		PredownloadManager.Stop(PredownloadManager.STOP_FLAG.LOADING_PROCESS, is_stop: false);
		Object.Destroy(this);
	}
}

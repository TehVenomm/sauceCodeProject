using System.Collections;
using UnityEngine;

public class PlayerLoadTest : MonoBehaviour
{
	public string body;

	public string face;

	public string head;

	private Object loadObject;

	private bool loadError;

	private IEnumerator Start()
	{
		yield return 0;
		yield return StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_BDY, "BDY00_000"));
		GameObject body = (GameObject)Object.Instantiate(loadObject);
		yield return StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_HEAD, "HED00_000"));
		GameObject head = (GameObject)Object.Instantiate(loadObject);
		yield return StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_FACE, "PLF00_000"));
		GameObject face = (GameObject)Object.Instantiate(loadObject);
		yield return StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_WEAPON, "WEP00_001"));
		GameObject weapon = (GameObject)Object.Instantiate(loadObject);
		yield return StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_ANIM, "PLC00_AnimCtrl"));
		RuntimeAnimatorController runtimeAnimatorController = loadObject as RuntimeAnimatorController;
		Transform parent = Utility.Find(body.transform, "Head");
		Transform parent2 = Utility.Find(body.transform, "R_Wep");
		Transform parent3 = Utility.Find(body.transform, "L_Wep");
		Utility.Attach(parent, head.transform);
		Utility.Attach(parent, face.transform);
		Renderer[] componentsInChildren = weapon.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			MeshRenderer meshRenderer = (MeshRenderer)componentsInChildren[i];
			if (meshRenderer.name.EndsWith("_L"))
			{
				Utility.Attach(parent3, meshRenderer.transform.parent);
			}
			else
			{
				Utility.Attach(parent2, meshRenderer.transform.parent);
			}
		}
		Object.DestroyImmediate(weapon);
		body.GetComponentInChildren<Animator>().runtimeAnimatorController = runtimeAnimatorController;
		Debug.Log("End:" + runtimeAnimatorController);
	}

	private IEnumerator LoadObject(RESOURCE_CATEGORY category, string resource_name)
	{
		loadObject = null;
		loadError = false;
		MonoBehaviourSingleton<ResourceManager>.I.Load(this, category, resource_name, OnLoadComplate, OnLoadError);
		while (loadObject == null && !loadError)
		{
			yield return 0;
		}
	}

	private void OnLoadComplate(ResourceManager.LoadRequest request, ResourceObject[] objs)
	{
		Debug.Log("OnLoadComplate");
		loadObject = objs[0].obj;
	}

	private void OnLoadError(ResourceManager.LoadRequest request, ResourceManager.ERROR_CODE code)
	{
		Debug.Log("OnLoadError");
		loadError = true;
	}

	private void Update()
	{
	}
}

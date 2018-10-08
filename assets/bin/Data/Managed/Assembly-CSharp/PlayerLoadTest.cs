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
		yield return (object)0;
		yield return (object)StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_BDY, "BDY00_000"));
		GameObject body = (GameObject)Object.Instantiate(loadObject);
		yield return (object)StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_HEAD, "HED00_000"));
		GameObject head = (GameObject)Object.Instantiate(loadObject);
		yield return (object)StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_FACE, "PLF00_000"));
		GameObject face = (GameObject)Object.Instantiate(loadObject);
		yield return (object)StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_WEAPON, "WEP00_001"));
		GameObject weapon = (GameObject)Object.Instantiate(loadObject);
		yield return (object)StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_ANIM, "PLC00_AnimCtrl"));
		RuntimeAnimatorController anim = loadObject as RuntimeAnimatorController;
		Transform head_node = Utility.Find(body.transform, "Head");
		Transform r_wep_node = Utility.Find(body.transform, "R_Wep");
		Transform l_wep_node = Utility.Find(body.transform, "L_Wep");
		Utility.Attach(head_node, head.transform);
		Utility.Attach(head_node, face.transform);
		Renderer[] renderers = weapon.GetComponentsInChildren<Renderer>();
		Renderer[] array = renderers;
		for (int i = 0; i < array.Length; i++)
		{
			MeshRenderer renderer = (MeshRenderer)array[i];
			if (renderer.name.EndsWith("_L"))
			{
				Utility.Attach(l_wep_node, renderer.transform.parent);
			}
			else
			{
				Utility.Attach(r_wep_node, renderer.transform.parent);
			}
		}
		Object.DestroyImmediate(weapon);
		body.GetComponentInChildren<Animator>().runtimeAnimatorController = anim;
		Debug.Log("End:" + anim);
	}

	private IEnumerator LoadObject(RESOURCE_CATEGORY category, string resource_name)
	{
		loadObject = null;
		loadError = false;
		MonoBehaviourSingleton<ResourceManager>.I.Load(this, category, resource_name, OnLoadComplate, OnLoadError, false, null);
		while (loadObject == (Object)null && !loadError)
		{
			yield return (object)0;
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

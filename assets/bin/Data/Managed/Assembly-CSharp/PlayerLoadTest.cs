using System.Collections;
using UnityEngine;

public class PlayerLoadTest : MonoBehaviour
{
	public string body;

	public string face;

	public string head;

	private Object loadObject;

	private bool loadError;

	public PlayerLoadTest()
		: this()
	{
	}

	private IEnumerator Start()
	{
		yield return 0;
		yield return this.StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_BDY, "BDY00_000"));
		GameObject body = Object.Instantiate(loadObject);
		yield return this.StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_HEAD, "HED00_000"));
		GameObject head = Object.Instantiate(loadObject);
		yield return this.StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_FACE, "PLF00_000"));
		GameObject face = Object.Instantiate(loadObject);
		yield return this.StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_WEAPON, "WEP00_001"));
		GameObject weapon = Object.Instantiate(loadObject);
		yield return this.StartCoroutine(LoadObject(RESOURCE_CATEGORY.PLAYER_ANIM, "PLC00_AnimCtrl"));
		RuntimeAnimatorController anim = loadObject as RuntimeAnimatorController;
		Transform head_node = Utility.Find(body.get_transform(), "Head");
		Transform r_wep_node = Utility.Find(body.get_transform(), "R_Wep");
		Transform l_wep_node = Utility.Find(body.get_transform(), "L_Wep");
		Utility.Attach(head_node, head.get_transform());
		Utility.Attach(head_node, face.get_transform());
		Renderer[] renderers = weapon.GetComponentsInChildren<Renderer>();
		Renderer[] array = renderers;
		foreach (MeshRenderer val in array)
		{
			if (val.get_name().EndsWith("_L"))
			{
				Utility.Attach(l_wep_node, val.get_transform().get_parent());
			}
			else
			{
				Utility.Attach(r_wep_node, val.get_transform().get_parent());
			}
		}
		Object.DestroyImmediate(weapon);
		body.GetComponentInChildren<Animator>().set_runtimeAnimatorController(anim);
		Debug.Log((object)("End:" + anim));
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
		Debug.Log((object)"OnLoadComplate");
		loadObject = objs[0].obj;
	}

	private void OnLoadError(ResourceManager.LoadRequest request, ResourceManager.ERROR_CODE code)
	{
		Debug.Log((object)"OnLoadError");
		loadError = true;
	}

	private void Update()
	{
	}
}

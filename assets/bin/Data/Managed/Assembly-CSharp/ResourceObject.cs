using rhyme;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject
{
	private class Pool_ResourceObject : rymTPool<ResourceObject>
	{
	}

	private int _refCount;

	public PackageObject package;

	public RESOURCE_CATEGORY category;

	public Object obj;

	public string name;

	private Object[] willReleaseObjs;

	private static List<Object> willReleaseList = new List<Object>(2);

	public int refCount
	{
		get
		{
			return _refCount;
		}
		set
		{
			_refCount = value;
		}
	}

	public ResourceObject()
	{
		_refCount = 0;
		package = null;
		obj = null;
		name = null;
		willReleaseObjs = null;
	}

	public static void ClearPoolObjects()
	{
		rymTPool<ResourceObject>.Clear();
	}

	public static ResourceObject Get(RESOURCE_CATEGORY category, string name, Object obj)
	{
		ResourceObject resourceObject = rymTPool<ResourceObject>.Get();
		resourceObject._refCount = 0;
		resourceObject.category = category;
		resourceObject.obj = obj;
		resourceObject.name = name;
		switch (resourceObject.category)
		{
		case RESOURCE_CATEGORY.EFFECT_TEX:
		case RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX:
		case RESOURCE_CATEGORY.SOUND_VOICE:
			willReleaseList.Add(resourceObject.obj);
			break;
		case RESOURCE_CATEGORY.PLAYER_ARM:
		case RESOURCE_CATEGORY.PLAYER_BDY:
		case RESOURCE_CATEGORY.PLAYER_FACE:
		case RESOURCE_CATEGORY.PLAYER_HEAD:
		case RESOURCE_CATEGORY.PLAYER_LEG:
		case RESOURCE_CATEGORY.PLAYER_WEAPON:
		{
			GameObject val = resourceObject.obj as GameObject;
			if (!(val != null))
			{
				break;
			}
			Renderer componentInChildren = val.GetComponentInChildren<Renderer>();
			willReleaseList.Add(componentInChildren.get_sharedMaterial().get_mainTexture());
			if (componentInChildren is MeshRenderer)
			{
				MeshFilter component = componentInChildren.GetComponent<MeshFilter>();
				if (component != null)
				{
					willReleaseList.Add(component.get_sharedMesh());
				}
			}
			else if (componentInChildren is SkinnedMeshRenderer)
			{
				SkinnedMeshRenderer val2 = componentInChildren as SkinnedMeshRenderer;
				willReleaseList.Add(val2.get_sharedMesh());
			}
			break;
		}
		}
		if (willReleaseList.Count > 0)
		{
			resourceObject.willReleaseObjs = willReleaseList.ToArray();
			willReleaseList.Clear();
		}
		return resourceObject;
	}

	public static void Release(ref ResourceObject resobj)
	{
		if (resobj.willReleaseObjs != null && ResourceCache.CanUseCustomUnloder())
		{
			for (int i = 0; i < resobj.willReleaseObjs.Length; i++)
			{
				Object.DestroyImmediate(resobj.willReleaseObjs[i], true);
				resobj.willReleaseObjs[i] = null;
			}
		}
		resobj.Reset();
		rymTPool<ResourceObject>.Release(ref resobj);
	}

	public void Reset()
	{
		_refCount = 0;
		package = null;
		obj = null;
		name = null;
		willReleaseObjs = null;
	}
}

using rhyme;
using UnityEngine;

public class PackageObject
{
	private class Pool_PackageObject : rymTPool<PackageObject>
	{
	}

	public int refCount;

	public string name;

	public object obj;

	public BetterList<PackageObject> linkPackages;

	public UIAtlas hostAtlas;

	public bool unloadAllLoadedObjects;

	public PackageObject()
	{
		refCount = 0;
		linkPackages = new BetterList<PackageObject>();
		obj = null;
	}

	public static void ClearPoolObjects()
	{
		rymTPool<PackageObject>.Clear();
	}

	public static PackageObject Get()
	{
		return rymTPool<PackageObject>.Get();
	}

	public static PackageObject Get(string name, object obj)
	{
		PackageObject packageObject = rymTPool<PackageObject>.Get();
		packageObject.name = name;
		packageObject.obj = obj;
		packageObject.refCount = 0;
		return packageObject;
	}

	public static void Release(ref PackageObject obj)
	{
		AssetBundle val = obj.obj as AssetBundle;
		if (val != null)
		{
			val.Unload(false);
		}
		obj.Reset();
		rymTPool<PackageObject>.Release(ref obj);
	}

	public void Reset()
	{
		refCount = 0;
		name = null;
		obj = null;
		linkPackages.Clear();
	}
}

using rhyme;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EffectStock : MonoBehaviour
{
	public bool stocking;

	private Vector3 defaultPosition;

	private Quaternion defaultRotation;

	private Vector3 defaultScale;

	private int defaultLayer;

	private List<Component> defaultComponents = new List<Component>();

	private rymFX fx;

	private EffectCtrl ctrl;

	public bool IsLoop()
	{
		if (fx != null)
		{
			return fx.IsLoop();
		}
		if (ctrl != null)
		{
			return ctrl.loop;
		}
		return false;
	}

	private void Awake()
	{
		Transform transform = base.transform;
		defaultPosition = transform.localPosition;
		defaultRotation = transform.localRotation;
		defaultScale = transform.localScale;
		defaultLayer = base.gameObject.layer;
		GetComponentsInChildren(includeInactive: true, defaultComponents);
		fx = GetComponent<rymFX>();
		ctrl = GetComponent<EffectCtrl>();
	}

	public void Stock()
	{
		if (stocking)
		{
			return;
		}
		GetComponentsInChildren(includeInactive: true, Temporary.componentList);
		int num = 0;
		int count = defaultComponents.Count;
		int i = 0;
		for (int count2 = Temporary.componentList.Count; i < count2; i++)
		{
			Component component = Temporary.componentList[i];
			if (!(component != null))
			{
				continue;
			}
			int j;
			for (j = num; j < count; j++)
			{
				if (defaultComponents[j] == component)
				{
					if (j == num)
					{
						num++;
					}
					break;
				}
			}
			if (j == count)
			{
				if (component is Transform)
				{
					Object.DestroyImmediate(component.gameObject);
				}
				else
				{
					Object.DestroyImmediate(component);
				}
			}
			Temporary.componentList[i] = null;
		}
		Temporary.componentList.Clear();
		if (fx != null && rymFXManager.DestroyFxDelegate != null)
		{
			rymFXManager.DestroyFxDelegate(fx);
		}
		stocking = true;
	}

	public void Recycle(Transform parent, int layer = -1)
	{
		Transform transform = base.transform;
		transform.parent = null;
		base.gameObject.SetActive(value: true);
		if (layer == -1)
		{
			layer = defaultLayer;
		}
		Utility.SetLayerWithChildren(transform, layer);
		int i = 0;
		for (int count = defaultComponents.Count; i < count; i++)
		{
			Component component = defaultComponents[i];
			if (component is Trail)
			{
				Trail obj = component as Trail;
				obj.enabled = true;
				obj.Reset();
				obj.emit = true;
			}
			else if (component is Renderer)
			{
				(component as Renderer).enabled = true;
			}
			else if (component is Animator)
			{
				Animator obj2 = component as Animator;
				obj2.enabled = true;
				RuntimeAnimatorController runtimeAnimatorController = obj2.runtimeAnimatorController;
				obj2.runtimeAnimatorController = null;
				obj2.runtimeAnimatorController = runtimeAnimatorController;
			}
		}
		transform.localPosition = defaultPosition;
		transform.localRotation = defaultRotation;
		transform.localScale = defaultScale;
		Utility.Attach(parent, transform);
		if (fx != null)
		{
			fx.LoopEnd = false;
			rymFXManager.GetTextureFunc getTextureDelegate = rymFXManager.GetTextureDelegate;
			rymFXManager.GetTextureDelegate = GetTexture;
			fx.ResetImmediate();
			rymFXManager.GetTextureDelegate = getTextureDelegate;
		}
		if (ctrl != null)
		{
			ctrl.Reset();
		}
		stocking = false;
	}

	private Texture GetTexture(string name)
	{
		ResourceLink component = fx.GetComponent<ResourceLink>();
		if (component != null)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(name);
			if (!string.IsNullOrEmpty(fileNameWithoutExtension))
			{
				return component.Get<Texture>(fileNameWithoutExtension);
			}
		}
		return null;
	}
}

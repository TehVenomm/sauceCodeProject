using rhyme;
using System;
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

	public EffectStock()
		: this()
	{
	}

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
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = this.get_transform();
		defaultPosition = transform.get_localPosition();
		defaultRotation = transform.get_localRotation();
		defaultScale = transform.get_localScale();
		defaultLayer = this.get_gameObject().get_layer();
		this.GetComponentsInChildren<Component>(true, defaultComponents);
		fx = this.GetComponent<rymFX>();
		ctrl = this.GetComponent<EffectCtrl>();
	}

	public void Stock()
	{
		if (stocking)
		{
			return;
		}
		this.GetComponentsInChildren<Component>(true, Temporary.componentList);
		int num = 0;
		int count = defaultComponents.Count;
		int i = 0;
		for (int count2 = Temporary.componentList.Count; i < count2; i++)
		{
			Component val = Temporary.componentList[i];
			if (!(val != null))
			{
				continue;
			}
			int j;
			for (j = num; j < count; j++)
			{
				if (defaultComponents[j] == val)
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
				if (val is Transform)
				{
					Object.DestroyImmediate(val.get_gameObject());
				}
				else
				{
					Object.DestroyImmediate(val);
				}
			}
			Temporary.componentList[i] = null;
		}
		Temporary.componentList.Clear();
		if (fx != null && rymFXManager.DestroyFxDelegate != null)
		{
			rymFXManager.DestroyFxDelegate.Invoke(fx);
		}
		stocking = true;
	}

	public unsafe void Recycle(Transform parent, int layer = -1)
	{
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		Transform transform = this.get_transform();
		transform.set_parent(null);
		this.get_gameObject().SetActive(true);
		if (layer == -1)
		{
			layer = defaultLayer;
		}
		Utility.SetLayerWithChildren(transform, layer);
		int i = 0;
		for (int count = defaultComponents.Count; i < count; i++)
		{
			Component val = defaultComponents[i];
			if (val is Trail)
			{
				Trail trail = val as Trail;
				trail.set_enabled(true);
				trail.Reset();
				trail.emit = true;
			}
			else if (val is Renderer)
			{
				(val as Renderer).set_enabled(true);
			}
			else if (val is Animator)
			{
				Animator val2 = val as Animator;
				val2.set_enabled(true);
				RuntimeAnimatorController runtimeAnimatorController = val2.get_runtimeAnimatorController();
				val2.set_runtimeAnimatorController(null);
				val2.set_runtimeAnimatorController(runtimeAnimatorController);
			}
		}
		transform.set_localPosition(defaultPosition);
		transform.set_localRotation(defaultRotation);
		transform.set_localScale(defaultScale);
		Utility.Attach(parent, transform);
		if (fx != null)
		{
			fx.LoopEnd = false;
			GetTextureFunc getTextureDelegate = rymFXManager.GetTextureDelegate;
			rymFXManager.GetTextureDelegate = new GetTextureFunc((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
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

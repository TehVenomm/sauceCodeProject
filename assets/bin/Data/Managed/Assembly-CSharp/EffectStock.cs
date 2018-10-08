using rhyme;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EffectStock
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Transform val = this.get_transform();
		defaultPosition = val.get_localPosition();
		defaultRotation = val.get_localRotation();
		defaultScale = val.get_localScale();
		defaultLayer = this.get_gameObject().get_layer();
		this.GetComponentsInChildren<Component>(true, defaultComponents);
		fx = this.GetComponent<rymFX>();
		ctrl = this.GetComponent<EffectCtrl>();
	}

	public void Stock()
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		if (!stocking)
		{
			this.GetComponentsInChildren<Component>(true, Temporary.componentList);
			int num = 0;
			int count = defaultComponents.Count;
			int i = 0;
			for (int count2 = Temporary.componentList.Count; i < count2; i++)
			{
				Component val = Temporary.componentList[i];
				if (val != null)
				{
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
			}
			Temporary.componentList.Clear();
			if (fx != null && (int)rymFXManager.DestroyFxDelegate != 0)
			{
				rymFXManager.DestroyFxDelegate.Invoke(fx);
			}
			stocking = true;
		}
	}

	public unsafe void Recycle(Transform parent, int layer = -1)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		Transform val = this.get_transform();
		val.set_parent(null);
		this.get_gameObject().SetActive(true);
		if (layer == -1)
		{
			layer = defaultLayer;
		}
		Utility.SetLayerWithChildren(val, layer);
		int i = 0;
		for (int count = defaultComponents.Count; i < count; i++)
		{
			Component val2 = defaultComponents[i];
			if (val2 is Trail)
			{
				Trail trail = val2 as Trail;
				trail.set_enabled(true);
				trail.Reset();
				trail.emit = true;
			}
			else if (val2 is Renderer)
			{
				(val2 as Renderer).set_enabled(true);
			}
			else if (val2 is Animator)
			{
				Animator val3 = val2 as Animator;
				val3.set_enabled(true);
				RuntimeAnimatorController val4 = val3.get_runtimeAnimatorController();
				val3.set_runtimeAnimatorController(null);
				val3.set_runtimeAnimatorController(val4);
			}
		}
		val.set_localPosition(defaultPosition);
		val.set_localRotation(defaultRotation);
		val.set_localScale(defaultScale);
		Utility.Attach(parent, val);
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

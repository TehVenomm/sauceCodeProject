using System;
using UnityEngine;

public class UIParticle : UIWidget
{
	private Renderer[] _renderers;

	private int _lastQueue;

	private ParticleSystemRenderer _particleRenderer;

	private bool _createMaterial;

	public override Material material
	{
		get
		{
			if (_particleRenderer == null)
			{
				_particleRenderer = this.GetComponentInChildren<ParticleSystemRenderer>(true);
			}
			return _particleRenderer.get_sharedMaterial();
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no material setter");
		}
	}

	protected override void OnStart()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		base.OnStart();
		_renderers = this.GetComponentsInChildren<Renderer>(true);
		if (Application.get_isPlaying() && !_createMaterial)
		{
			_createMaterial = true;
			Renderer[] renderers = _renderers;
			foreach (Renderer val in renderers)
			{
				int num = val.get_materials().Length;
				Material[] array = (Material[])new Material[num];
				for (int j = 0; j < num; j++)
				{
					Material val2 = array[j] = new Material(val.get_materials()[j]);
				}
				val.set_materials(array);
			}
		}
		_lastQueue = -1;
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (drawCall == null)
		{
			return;
		}
		int renderQueue = drawCall.renderQueue;
		if (_lastQueue == renderQueue)
		{
			return;
		}
		_lastQueue = renderQueue;
		Renderer[] renderers = _renderers;
		foreach (Renderer val in renderers)
		{
			Material[] sharedMaterials = val.get_sharedMaterials();
			foreach (Material val2 in sharedMaterials)
			{
				val2.set_renderQueue(_lastQueue);
			}
			val.set_sortingOrder(drawCall.sortingOrder);
		}
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			verts.Add(Vector3.get_zero());
		}
		uvs.Add(Vector2.get_zero());
		cols.Add(new Color32((byte)1, (byte)1, (byte)1, (byte)1));
	}
}

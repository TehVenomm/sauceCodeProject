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
				_particleRenderer = GetComponentInChildren<ParticleSystemRenderer>(includeInactive: true);
			}
			return _particleRenderer.sharedMaterial;
		}
		set
		{
			throw new NotImplementedException(GetType() + " has no material setter");
		}
	}

	protected override void OnStart()
	{
		base.OnStart();
		_renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
		if (Application.isPlaying && !_createMaterial)
		{
			_createMaterial = true;
			Renderer[] renderers = _renderers;
			foreach (Renderer renderer in renderers)
			{
				int num = renderer.materials.Length;
				Material[] array = new Material[num];
				for (int j = 0; j < num; j++)
				{
					Material material = array[j] = new Material(renderer.materials[j]);
				}
				renderer.materials = array;
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
		foreach (Renderer renderer in renderers)
		{
			Material[] sharedMaterials = renderer.sharedMaterials;
			for (int j = 0; j < sharedMaterials.Length; j++)
			{
				sharedMaterials[j].renderQueue = _lastQueue;
			}
			renderer.sortingOrder = drawCall.sortingOrder;
		}
	}

	public override void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		for (int i = 0; i < 4; i++)
		{
			verts.Add(Vector3.zero);
		}
		uvs.Add(Vector2.zero);
		cols.Add(new Color32(1, 1, 1, 1));
	}
}

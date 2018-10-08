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
			if ((UnityEngine.Object)_particleRenderer == (UnityEngine.Object)null)
			{
				_particleRenderer = GetComponentInChildren<ParticleSystemRenderer>(true);
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
		_renderers = GetComponentsInChildren<Renderer>(true);
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
		if (!((UnityEngine.Object)drawCall == (UnityEngine.Object)null))
		{
			int renderQueue = drawCall.renderQueue;
			if (_lastQueue != renderQueue)
			{
				_lastQueue = renderQueue;
				Renderer[] renderers = _renderers;
				foreach (Renderer renderer in renderers)
				{
					Material[] sharedMaterials = renderer.sharedMaterials;
					foreach (Material material in sharedMaterials)
					{
						material.renderQueue = _lastQueue;
					}
					renderer.sortingOrder = drawCall.sortingOrder;
				}
			}
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

using System.Collections.Generic;
using UnityEngine;

public class EffectSizeCtrl : MonoBehaviour
{
	private class ParticleInfo
	{
		public ParticleSystemRenderer psr;

		public float firstLength;
	}

	[SerializeField]
	private bool isScale;

	[SerializeField]
	private float endScale;

	[SerializeField]
	private ParticleSystem[] particles;

	private List<ParticleInfo> particleInfo = new List<ParticleInfo>();

	private bool isWorking;

	private float execSec;

	private float targetSec;

	private float firstScale = 1f;

	private Vector3 _scale = new Vector3(1f, 1f, 1f);

	private Transform _transform;

	private void Awake()
	{
		_transform = base.transform;
	}

	private void Destroy()
	{
		particleInfo.Clear();
		_transform = null;
	}

	private void Update()
	{
		if (isWorking)
		{
			execSec += Time.deltaTime;
			float num = 0f;
			if (execSec >= targetSec)
			{
				execSec = targetSec;
				num = 1f;
				isWorking = false;
			}
			else
			{
				num = execSec / targetSec;
			}
			float num2 = 1f - (1f - endScale) * num;
			if (isScale)
			{
				float num3 = firstScale * num2;
				_scale.Set(num3, num3, num3);
				_transform.localScale = _scale;
			}
			int i = 0;
			for (int count = this.particleInfo.Count; i < count; i++)
			{
				ParticleInfo particleInfo = this.particleInfo[i];
				particleInfo.psr.lengthScale = particleInfo.firstLength * num2;
			}
		}
	}

	public void Work(float sec)
	{
		if (!isWorking && sec != 0f)
		{
			this.particleInfo.Clear();
			int i = 0;
			for (int num = particles.Length; i < num; i++)
			{
				ParticleSystemRenderer component = particles[i].GetComponent<ParticleSystemRenderer>();
				if (!object.ReferenceEquals(component, null))
				{
					ParticleInfo particleInfo = new ParticleInfo();
					particleInfo.psr = component;
					particleInfo.firstLength = component.lengthScale;
					this.particleInfo.Add(particleInfo);
				}
			}
			Vector3 localScale = _transform.localScale;
			firstScale = localScale.x;
			execSec = 0f;
			targetSec = sec;
			isWorking = true;
		}
	}
}

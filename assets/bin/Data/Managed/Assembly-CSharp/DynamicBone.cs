using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dynamic Bone/Dynamic Bone")]
public class DynamicBone
{
	public enum FreezeAxis
	{
		None,
		X,
		Y,
		Z
	}

	private class Particle
	{
		public Transform m_Transform;

		public int m_ParentIndex = -1;

		public float m_Damping;

		public float m_Elasticity;

		public float m_Stiffness;

		public float m_Inert;

		public float m_Radius;

		public float m_BoneLength;

		public Vector3 m_Position = Vector3.get_zero();

		public Vector3 m_PrevPosition = Vector3.get_zero();

		public Vector3 m_EndOffset = Vector3.get_zero();

		public Vector3 m_InitLocalPosition = Vector3.get_zero();

		public Quaternion m_InitLocalRotation = Quaternion.get_identity();
	}

	public Transform m_Root;

	public float m_UpdateRate = 60f;

	[Range(0f, 1f)]
	public float m_Damping = 0.1f;

	public AnimationCurve m_DampingDistrib;

	[Range(0f, 1f)]
	public float m_Elasticity = 0.1f;

	public AnimationCurve m_ElasticityDistrib;

	[Range(0f, 1f)]
	public float m_Stiffness = 0.1f;

	public AnimationCurve m_StiffnessDistrib;

	[Range(0f, 1f)]
	public float m_Inert;

	public AnimationCurve m_InertDistrib;

	public float m_Radius;

	public AnimationCurve m_RadiusDistrib;

	public float m_EndLength;

	public Vector3 m_EndOffset = Vector3.get_zero();

	public Vector3 m_Gravity = Vector3.get_zero();

	public Vector3 m_Force = Vector3.get_zero();

	public List<DynamicBoneCollider> m_Colliders;

	public List<Transform> m_Exclusions;

	public FreezeAxis m_FreezeAxis;

	public bool m_DistantDisable;

	public Transform m_ReferenceObject;

	public float m_DistanceToObject = 20f;

	private Vector3 m_LocalGravity = Vector3.get_zero();

	private Vector3 m_ObjectMove = Vector3.get_zero();

	private Vector3 m_ObjectPrevPosition = Vector3.get_zero();

	private float m_BoneTotalLength;

	private float m_ObjectScale = 1f;

	private float m_Time;

	private float m_Weight = 1f;

	private bool m_DistantDisabled;

	private List<Particle> m_Particles = new List<Particle>();

	public DynamicBone()
		: this()
	{
	}//IL_002d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0032: Unknown result type (might be due to invalid IL or missing references)
	//IL_0038: Unknown result type (might be due to invalid IL or missing references)
	//IL_003d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0043: Unknown result type (might be due to invalid IL or missing references)
	//IL_0048: Unknown result type (might be due to invalid IL or missing references)
	//IL_0059: Unknown result type (might be due to invalid IL or missing references)
	//IL_005e: Unknown result type (might be due to invalid IL or missing references)
	//IL_0064: Unknown result type (might be due to invalid IL or missing references)
	//IL_0069: Unknown result type (might be due to invalid IL or missing references)
	//IL_006f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0074: Unknown result type (might be due to invalid IL or missing references)


	private void Start()
	{
		SetupParticles();
	}

	private void Update()
	{
		if (m_Weight > 0f && (!m_DistantDisable || !m_DistantDisabled))
		{
			InitTransforms();
		}
	}

	private void LateUpdate()
	{
		if (m_DistantDisable)
		{
			CheckDistance();
		}
		if (m_Weight > 0f && (!m_DistantDisable || !m_DistantDisabled))
		{
			UpdateDynamicBones(Time.get_deltaTime());
		}
	}

	private void CheckDistance()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		Transform val = m_ReferenceObject;
		if (val == null && Camera.get_main() != null)
		{
			val = Camera.get_main().get_transform();
		}
		if (val != null)
		{
			Vector3 val2 = val.get_position() - this.get_transform().get_position();
			float sqrMagnitude = val2.get_sqrMagnitude();
			bool flag = sqrMagnitude > m_DistanceToObject * m_DistanceToObject;
			if (flag != m_DistantDisabled)
			{
				if (!flag)
				{
					ResetParticlesPosition();
				}
				m_DistantDisabled = flag;
			}
		}
	}

	private void OnEnable()
	{
		ResetParticlesPosition();
	}

	private void OnDisable()
	{
		InitTransforms();
	}

	private void OnValidate()
	{
		m_UpdateRate = Mathf.Max(m_UpdateRate, 0f);
		m_Damping = Mathf.Clamp01(m_Damping);
		m_Elasticity = Mathf.Clamp01(m_Elasticity);
		m_Stiffness = Mathf.Clamp01(m_Stiffness);
		m_Inert = Mathf.Clamp01(m_Inert);
		m_Radius = Mathf.Max(m_Radius, 0f);
		if (Application.get_isEditor() && Application.get_isPlaying())
		{
			InitTransforms();
			SetupParticles();
		}
	}

	private void OnDrawGizmosSelected()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		if (this.get_enabled() && !(m_Root == null))
		{
			if (Application.get_isEditor() && !Application.get_isPlaying() && this.get_transform().get_hasChanged())
			{
				InitTransforms();
				SetupParticles();
			}
			Gizmos.set_color(Color.get_white());
			for (int i = 0; i < m_Particles.Count; i++)
			{
				Particle particle = m_Particles[i];
				if (particle.m_ParentIndex >= 0)
				{
					Particle particle2 = m_Particles[particle.m_ParentIndex];
					Gizmos.DrawLine(particle.m_Position, particle2.m_Position);
				}
				if (particle.m_Radius > 0f)
				{
					Gizmos.DrawWireSphere(particle.m_Position, particle.m_Radius * m_ObjectScale);
				}
			}
		}
	}

	public void SetWeight(float w)
	{
		if (m_Weight != w)
		{
			if (w == 0f)
			{
				InitTransforms();
			}
			else if (m_Weight == 0f)
			{
				ResetParticlesPosition();
			}
			m_Weight = w;
		}
	}

	public float GetWeight()
	{
		return m_Weight;
	}

	private void UpdateDynamicBones(float t)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_Root == null))
		{
			Vector3 lossyScale = this.get_transform().get_lossyScale();
			m_ObjectScale = Mathf.Abs(lossyScale.x);
			m_ObjectMove = this.get_transform().get_position() - m_ObjectPrevPosition;
			m_ObjectPrevPosition = this.get_transform().get_position();
			int num = 1;
			if (m_UpdateRate > 0f)
			{
				float num2 = 1f / m_UpdateRate;
				m_Time += t;
				num = 0;
				while (m_Time >= num2)
				{
					m_Time -= num2;
					if (++num >= 3)
					{
						m_Time = 0f;
						break;
					}
				}
			}
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					UpdateParticles1();
					UpdateParticles2();
					m_ObjectMove = Vector3.get_zero();
				}
			}
			else
			{
				SkipUpdateParticles();
			}
			ApplyParticlesToTransforms();
		}
	}

	private void SetupParticles()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		m_Particles.Clear();
		if (!(m_Root == null))
		{
			m_LocalGravity = m_Root.InverseTransformDirection(m_Gravity);
			Vector3 lossyScale = this.get_transform().get_lossyScale();
			m_ObjectScale = lossyScale.x;
			m_ObjectPrevPosition = this.get_transform().get_position();
			m_ObjectMove = Vector3.get_zero();
			m_BoneTotalLength = 0f;
			AppendParticles(m_Root, -1, 0f);
			for (int i = 0; i < m_Particles.Count; i++)
			{
				Particle particle = m_Particles[i];
				particle.m_Damping = m_Damping;
				particle.m_Elasticity = m_Elasticity;
				particle.m_Stiffness = m_Stiffness;
				particle.m_Inert = m_Inert;
				particle.m_Radius = m_Radius;
				if (m_BoneTotalLength > 0f)
				{
					float num = particle.m_BoneLength / m_BoneTotalLength;
					if (m_DampingDistrib != null && m_DampingDistrib.get_keys().Length > 0)
					{
						particle.m_Damping *= m_DampingDistrib.Evaluate(num);
					}
					if (m_ElasticityDistrib != null && m_ElasticityDistrib.get_keys().Length > 0)
					{
						particle.m_Elasticity *= m_ElasticityDistrib.Evaluate(num);
					}
					if (m_StiffnessDistrib != null && m_StiffnessDistrib.get_keys().Length > 0)
					{
						particle.m_Stiffness *= m_StiffnessDistrib.Evaluate(num);
					}
					if (m_InertDistrib != null && m_InertDistrib.get_keys().Length > 0)
					{
						particle.m_Inert *= m_InertDistrib.Evaluate(num);
					}
					if (m_RadiusDistrib != null && m_RadiusDistrib.get_keys().Length > 0)
					{
						particle.m_Radius *= m_RadiusDistrib.Evaluate(num);
					}
				}
				particle.m_Damping = Mathf.Clamp01(particle.m_Damping);
				particle.m_Elasticity = Mathf.Clamp01(particle.m_Elasticity);
				particle.m_Stiffness = Mathf.Clamp01(particle.m_Stiffness);
				particle.m_Inert = Mathf.Clamp01(particle.m_Inert);
				particle.m_Radius = Mathf.Max(particle.m_Radius, 0f);
			}
		}
	}

	private void AppendParticles(Transform b, int parentIndex, float boneLength)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		Particle particle = new Particle();
		particle.m_Transform = b;
		particle.m_ParentIndex = parentIndex;
		Vector3 val;
		if (b != null)
		{
			val = (particle.m_Position = (particle.m_PrevPosition = b.get_position()));
			particle.m_InitLocalPosition = b.get_localPosition();
			particle.m_InitLocalRotation = b.get_localRotation();
		}
		else
		{
			Transform transform = m_Particles[parentIndex].m_Transform;
			if (m_EndLength > 0f)
			{
				Transform val2 = transform.get_parent();
				if (val2 != null)
				{
					particle.m_EndOffset = transform.InverseTransformPoint(transform.get_position() * 2f - val2.get_position()) * m_EndLength;
				}
				else
				{
					particle.m_EndOffset = new Vector3(m_EndLength, 0f, 0f);
				}
			}
			else
			{
				particle.m_EndOffset = transform.InverseTransformPoint(this.get_transform().TransformDirection(m_EndOffset) + transform.get_position());
			}
			val = (particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset)));
		}
		if (parentIndex >= 0)
		{
			float num = boneLength;
			val = m_Particles[parentIndex].m_Transform.get_position() - particle.m_Position;
			boneLength = num + val.get_magnitude();
			particle.m_BoneLength = boneLength;
			m_BoneTotalLength = Mathf.Max(m_BoneTotalLength, boneLength);
		}
		int count = m_Particles.Count;
		m_Particles.Add(particle);
		if (b != null)
		{
			for (int i = 0; i < b.get_childCount(); i++)
			{
				bool flag = false;
				if (m_Exclusions != null)
				{
					for (int j = 0; j < m_Exclusions.Count; j++)
					{
						Transform val3 = m_Exclusions[j];
						if (val3 == b.GetChild(i))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					AppendParticles(b.GetChild(i), count, boneLength);
				}
			}
			if (b.get_childCount() == 0 && (m_EndLength > 0f || m_EndOffset != Vector3.get_zero()))
			{
				AppendParticles(null, count, boneLength);
			}
		}
	}

	private void InitTransforms()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			if (particle.m_Transform != null)
			{
				particle.m_Transform.set_localPosition(particle.m_InitLocalPosition);
				particle.m_Transform.set_localRotation(particle.m_InitLocalRotation);
			}
		}
	}

	private void ResetParticlesPosition()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			if (particle.m_Transform != null)
			{
				particle.m_Position = (particle.m_PrevPosition = particle.m_Transform.get_position());
			}
			else
			{
				Transform transform = m_Particles[particle.m_ParentIndex].m_Transform;
				particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
			}
		}
		m_ObjectPrevPosition = this.get_transform().get_position();
	}

	private void UpdateParticles1()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		Vector3 gravity = m_Gravity;
		Vector3 normalized = m_Gravity.get_normalized();
		Vector3 val = m_Root.TransformDirection(m_LocalGravity);
		Vector3 val2 = normalized * Mathf.Max(Vector3.Dot(val, normalized), 0f);
		gravity -= val2;
		gravity = (gravity + m_Force) * m_ObjectScale;
		for (int i = 0; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				Vector3 val3 = particle.m_Position - particle.m_PrevPosition;
				Vector3 val4 = m_ObjectMove * particle.m_Inert;
				particle.m_PrevPosition = particle.m_Position + val4;
				Particle particle2 = particle;
				particle2.m_Position += val3 * (1f - particle.m_Damping) + gravity + val4;
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.get_position();
			}
		}
	}

	private void UpdateParticles2()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		Plane val = default(Plane);
		for (int i = 1; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			Particle particle2 = m_Particles[particle.m_ParentIndex];
			float magnitude;
			if (particle.m_Transform != null)
			{
				Vector3 val2 = particle2.m_Transform.get_position() - particle.m_Transform.get_position();
				magnitude = val2.get_magnitude();
			}
			else
			{
				Matrix4x4 localToWorldMatrix = particle2.m_Transform.get_localToWorldMatrix();
				Vector3 val3 = localToWorldMatrix.MultiplyVector(particle.m_EndOffset);
				magnitude = val3.get_magnitude();
			}
			float num = Mathf.Lerp(1f, particle.m_Stiffness, m_Weight);
			if (num > 0f || particle.m_Elasticity > 0f)
			{
				Matrix4x4 localToWorldMatrix2 = particle2.m_Transform.get_localToWorldMatrix();
				localToWorldMatrix2.SetColumn(3, Vector4.op_Implicit(particle2.m_Position));
				Vector3 val4 = (!(particle.m_Transform != null)) ? localToWorldMatrix2.MultiplyPoint3x4(particle.m_EndOffset) : localToWorldMatrix2.MultiplyPoint3x4(particle.m_Transform.get_localPosition());
				Vector3 val5 = val4 - particle.m_Position;
				Particle particle3 = particle;
				particle3.m_Position += val5 * particle.m_Elasticity;
				if (num > 0f)
				{
					val5 = val4 - particle.m_Position;
					float magnitude2 = val5.get_magnitude();
					float num2 = magnitude * (1f - num) * 2f;
					if (magnitude2 > num2)
					{
						Particle particle4 = particle;
						particle4.m_Position += val5 * ((magnitude2 - num2) / magnitude2);
					}
				}
			}
			if (m_Colliders != null)
			{
				float particleRadius = particle.m_Radius * m_ObjectScale;
				for (int j = 0; j < m_Colliders.Count; j++)
				{
					DynamicBoneCollider dynamicBoneCollider = m_Colliders[j];
					if (dynamicBoneCollider != null && dynamicBoneCollider.get_enabled())
					{
						dynamicBoneCollider.Collide(ref particle.m_Position, particleRadius);
					}
				}
			}
			if (m_FreezeAxis != 0)
			{
				switch (m_FreezeAxis)
				{
				case FreezeAxis.X:
					val.SetNormalAndPosition(particle2.m_Transform.get_right(), particle2.m_Position);
					break;
				case FreezeAxis.Y:
					val.SetNormalAndPosition(particle2.m_Transform.get_up(), particle2.m_Position);
					break;
				case FreezeAxis.Z:
					val.SetNormalAndPosition(particle2.m_Transform.get_forward(), particle2.m_Position);
					break;
				}
				Particle particle5 = particle;
				particle5.m_Position -= val.get_normal() * val.GetDistanceToPoint(particle.m_Position);
			}
			Vector3 val6 = particle2.m_Position - particle.m_Position;
			float magnitude3 = val6.get_magnitude();
			if (magnitude3 > 0f)
			{
				Particle particle6 = particle;
				particle6.m_Position += val6 * ((magnitude3 - magnitude) / magnitude3);
			}
		}
	}

	private void SkipUpdateParticles()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				Particle particle2 = particle;
				particle2.m_PrevPosition += m_ObjectMove;
				Particle particle3 = particle;
				particle3.m_Position += m_ObjectMove;
				Particle particle4 = m_Particles[particle.m_ParentIndex];
				float magnitude;
				if (particle.m_Transform != null)
				{
					Vector3 val = particle4.m_Transform.get_position() - particle.m_Transform.get_position();
					magnitude = val.get_magnitude();
				}
				else
				{
					Matrix4x4 localToWorldMatrix = particle4.m_Transform.get_localToWorldMatrix();
					Vector3 val2 = localToWorldMatrix.MultiplyVector(particle.m_EndOffset);
					magnitude = val2.get_magnitude();
				}
				float num = Mathf.Lerp(1f, particle.m_Stiffness, m_Weight);
				if (num > 0f)
				{
					Matrix4x4 localToWorldMatrix2 = particle4.m_Transform.get_localToWorldMatrix();
					localToWorldMatrix2.SetColumn(3, Vector4.op_Implicit(particle4.m_Position));
					Vector3 val3 = (!(particle.m_Transform != null)) ? localToWorldMatrix2.MultiplyPoint3x4(particle.m_EndOffset) : localToWorldMatrix2.MultiplyPoint3x4(particle.m_Transform.get_localPosition());
					Vector3 val4 = val3 - particle.m_Position;
					float magnitude2 = val4.get_magnitude();
					float num2 = magnitude * (1f - num) * 2f;
					if (magnitude2 > num2)
					{
						Particle particle5 = particle;
						particle5.m_Position += val4 * ((magnitude2 - num2) / magnitude2);
					}
				}
				Vector3 val5 = particle4.m_Position - particle.m_Position;
				float magnitude3 = val5.get_magnitude();
				if (magnitude3 > 0f)
				{
					Particle particle6 = particle;
					particle6.m_Position += val5 * ((magnitude3 - magnitude) / magnitude3);
				}
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.get_position();
			}
		}
	}

	private void ApplyParticlesToTransforms()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 1; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			Particle particle2 = m_Particles[particle.m_ParentIndex];
			if (particle2.m_Transform.get_childCount() <= 1)
			{
				Vector3 val = (!(particle.m_Transform != null)) ? particle.m_EndOffset : particle.m_Transform.get_localPosition();
				Quaternion val2 = Quaternion.FromToRotation(particle2.m_Transform.TransformDirection(val), particle.m_Position - particle2.m_Position);
				particle2.m_Transform.set_rotation(val2 * particle2.m_Transform.get_rotation());
			}
			if (particle.m_Transform != null)
			{
				particle.m_Transform.set_position(particle.m_Position);
			}
		}
	}
}

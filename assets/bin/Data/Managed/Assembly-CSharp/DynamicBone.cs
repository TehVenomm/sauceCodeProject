using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Dynamic Bone/Dynamic Bone")]
public class DynamicBone : MonoBehaviour
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

		public Vector3 m_Position = Vector3.zero;

		public Vector3 m_PrevPosition = Vector3.zero;

		public Vector3 m_EndOffset = Vector3.zero;

		public Vector3 m_InitLocalPosition = Vector3.zero;

		public Quaternion m_InitLocalRotation = Quaternion.identity;
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

	public Vector3 m_EndOffset = Vector3.zero;

	public Vector3 m_Gravity = Vector3.zero;

	public Vector3 m_Force = Vector3.zero;

	public List<DynamicBoneCollider> m_Colliders;

	public List<Transform> m_Exclusions;

	public FreezeAxis m_FreezeAxis;

	public bool m_DistantDisable;

	public Transform m_ReferenceObject;

	public float m_DistanceToObject = 20f;

	private Vector3 m_LocalGravity = Vector3.zero;

	private Vector3 m_ObjectMove = Vector3.zero;

	private Vector3 m_ObjectPrevPosition = Vector3.zero;

	private float m_BoneTotalLength;

	private float m_ObjectScale = 1f;

	private float m_Time;

	private float m_Weight = 1f;

	private bool m_DistantDisabled;

	private List<Particle> m_Particles = new List<Particle>();

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
			UpdateDynamicBones(Time.deltaTime);
		}
	}

	private void CheckDistance()
	{
		Transform transform = m_ReferenceObject;
		if ((Object)transform == (Object)null && (Object)Camera.main != (Object)null)
		{
			transform = Camera.main.transform;
		}
		if ((Object)transform != (Object)null)
		{
			float sqrMagnitude = (transform.position - base.transform.position).sqrMagnitude;
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
		if (Application.isEditor && Application.isPlaying)
		{
			InitTransforms();
			SetupParticles();
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (base.enabled && !((Object)m_Root == (Object)null))
		{
			if (Application.isEditor && !Application.isPlaying && base.transform.hasChanged)
			{
				InitTransforms();
				SetupParticles();
			}
			Gizmos.color = Color.white;
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
		if (!((Object)m_Root == (Object)null))
		{
			Vector3 lossyScale = base.transform.lossyScale;
			m_ObjectScale = Mathf.Abs(lossyScale.x);
			m_ObjectMove = base.transform.position - m_ObjectPrevPosition;
			m_ObjectPrevPosition = base.transform.position;
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
					m_ObjectMove = Vector3.zero;
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
		m_Particles.Clear();
		if (!((Object)m_Root == (Object)null))
		{
			m_LocalGravity = m_Root.InverseTransformDirection(m_Gravity);
			Vector3 lossyScale = base.transform.lossyScale;
			m_ObjectScale = lossyScale.x;
			m_ObjectPrevPosition = base.transform.position;
			m_ObjectMove = Vector3.zero;
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
					float time = particle.m_BoneLength / m_BoneTotalLength;
					if (m_DampingDistrib != null && m_DampingDistrib.keys.Length > 0)
					{
						particle.m_Damping *= m_DampingDistrib.Evaluate(time);
					}
					if (m_ElasticityDistrib != null && m_ElasticityDistrib.keys.Length > 0)
					{
						particle.m_Elasticity *= m_ElasticityDistrib.Evaluate(time);
					}
					if (m_StiffnessDistrib != null && m_StiffnessDistrib.keys.Length > 0)
					{
						particle.m_Stiffness *= m_StiffnessDistrib.Evaluate(time);
					}
					if (m_InertDistrib != null && m_InertDistrib.keys.Length > 0)
					{
						particle.m_Inert *= m_InertDistrib.Evaluate(time);
					}
					if (m_RadiusDistrib != null && m_RadiusDistrib.keys.Length > 0)
					{
						particle.m_Radius *= m_RadiusDistrib.Evaluate(time);
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
		Particle particle = new Particle();
		particle.m_Transform = b;
		particle.m_ParentIndex = parentIndex;
		Vector3 vector;
		if ((Object)b != (Object)null)
		{
			vector = (particle.m_Position = (particle.m_PrevPosition = b.position));
			particle.m_InitLocalPosition = b.localPosition;
			particle.m_InitLocalRotation = b.localRotation;
		}
		else
		{
			Transform transform = m_Particles[parentIndex].m_Transform;
			if (m_EndLength > 0f)
			{
				Transform parent = transform.parent;
				if ((Object)parent != (Object)null)
				{
					particle.m_EndOffset = transform.InverseTransformPoint(transform.position * 2f - parent.position) * m_EndLength;
				}
				else
				{
					particle.m_EndOffset = new Vector3(m_EndLength, 0f, 0f);
				}
			}
			else
			{
				particle.m_EndOffset = transform.InverseTransformPoint(base.transform.TransformDirection(m_EndOffset) + transform.position);
			}
			vector = (particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset)));
		}
		if (parentIndex >= 0)
		{
			float num = boneLength;
			vector = m_Particles[parentIndex].m_Transform.position - particle.m_Position;
			boneLength = num + vector.magnitude;
			particle.m_BoneLength = boneLength;
			m_BoneTotalLength = Mathf.Max(m_BoneTotalLength, boneLength);
		}
		int count = m_Particles.Count;
		m_Particles.Add(particle);
		if ((Object)b != (Object)null)
		{
			for (int i = 0; i < b.childCount; i++)
			{
				bool flag = false;
				if (m_Exclusions != null)
				{
					for (int j = 0; j < m_Exclusions.Count; j++)
					{
						Transform x = m_Exclusions[j];
						if ((Object)x == (Object)b.GetChild(i))
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
			if (b.childCount == 0 && (m_EndLength > 0f || m_EndOffset != Vector3.zero))
			{
				AppendParticles(null, count, boneLength);
			}
		}
	}

	private void InitTransforms()
	{
		for (int i = 0; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			if ((Object)particle.m_Transform != (Object)null)
			{
				particle.m_Transform.localPosition = particle.m_InitLocalPosition;
				particle.m_Transform.localRotation = particle.m_InitLocalRotation;
			}
		}
	}

	private void ResetParticlesPosition()
	{
		for (int i = 0; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			if ((Object)particle.m_Transform != (Object)null)
			{
				particle.m_Position = (particle.m_PrevPosition = particle.m_Transform.position);
			}
			else
			{
				Transform transform = m_Particles[particle.m_ParentIndex].m_Transform;
				particle.m_Position = (particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset));
			}
		}
		m_ObjectPrevPosition = base.transform.position;
	}

	private void UpdateParticles1()
	{
		Vector3 gravity = m_Gravity;
		Vector3 normalized = m_Gravity.normalized;
		Vector3 lhs = m_Root.TransformDirection(m_LocalGravity);
		Vector3 b = normalized * Mathf.Max(Vector3.Dot(lhs, normalized), 0f);
		gravity -= b;
		gravity = (gravity + m_Force) * m_ObjectScale;
		for (int i = 0; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				Vector3 a = particle.m_Position - particle.m_PrevPosition;
				Vector3 b2 = m_ObjectMove * particle.m_Inert;
				particle.m_PrevPosition = particle.m_Position + b2;
				particle.m_Position += a * (1f - particle.m_Damping) + gravity + b2;
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	private void UpdateParticles2()
	{
		Plane plane = default(Plane);
		for (int i = 1; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			Particle particle2 = m_Particles[particle.m_ParentIndex];
			float num = (!((Object)particle.m_Transform != (Object)null)) ? particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle.m_EndOffset).magnitude : (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
			float num2 = Mathf.Lerp(1f, particle.m_Stiffness, m_Weight);
			if (num2 > 0f || particle.m_Elasticity > 0f)
			{
				Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
				localToWorldMatrix.SetColumn(3, particle2.m_Position);
				Vector3 a = (!((Object)particle.m_Transform != (Object)null)) ? localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset) : localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
				Vector3 a2 = a - particle.m_Position;
				particle.m_Position += a2 * particle.m_Elasticity;
				if (num2 > 0f)
				{
					a2 = a - particle.m_Position;
					float magnitude = a2.magnitude;
					float num3 = num * (1f - num2) * 2f;
					if (magnitude > num3)
					{
						particle.m_Position += a2 * ((magnitude - num3) / magnitude);
					}
				}
			}
			if (m_Colliders != null)
			{
				float particleRadius = particle.m_Radius * m_ObjectScale;
				for (int j = 0; j < m_Colliders.Count; j++)
				{
					DynamicBoneCollider dynamicBoneCollider = m_Colliders[j];
					if ((Object)dynamicBoneCollider != (Object)null && dynamicBoneCollider.enabled)
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
					plane.SetNormalAndPosition(particle2.m_Transform.right, particle2.m_Position);
					break;
				case FreezeAxis.Y:
					plane.SetNormalAndPosition(particle2.m_Transform.up, particle2.m_Position);
					break;
				case FreezeAxis.Z:
					plane.SetNormalAndPosition(particle2.m_Transform.forward, particle2.m_Position);
					break;
				}
				particle.m_Position -= plane.normal * plane.GetDistanceToPoint(particle.m_Position);
			}
			Vector3 a3 = particle2.m_Position - particle.m_Position;
			float magnitude2 = a3.magnitude;
			if (magnitude2 > 0f)
			{
				particle.m_Position += a3 * ((magnitude2 - num) / magnitude2);
			}
		}
	}

	private void SkipUpdateParticles()
	{
		for (int i = 0; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			if (particle.m_ParentIndex >= 0)
			{
				particle.m_PrevPosition += m_ObjectMove;
				particle.m_Position += m_ObjectMove;
				Particle particle2 = m_Particles[particle.m_ParentIndex];
				float num = (!((Object)particle.m_Transform != (Object)null)) ? particle2.m_Transform.localToWorldMatrix.MultiplyVector(particle.m_EndOffset).magnitude : (particle2.m_Transform.position - particle.m_Transform.position).magnitude;
				float num2 = Mathf.Lerp(1f, particle.m_Stiffness, m_Weight);
				if (num2 > 0f)
				{
					Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
					localToWorldMatrix.SetColumn(3, particle2.m_Position);
					Vector3 a = (!((Object)particle.m_Transform != (Object)null)) ? localToWorldMatrix.MultiplyPoint3x4(particle.m_EndOffset) : localToWorldMatrix.MultiplyPoint3x4(particle.m_Transform.localPosition);
					Vector3 a2 = a - particle.m_Position;
					float magnitude = a2.magnitude;
					float num3 = num * (1f - num2) * 2f;
					if (magnitude > num3)
					{
						particle.m_Position += a2 * ((magnitude - num3) / magnitude);
					}
				}
				Vector3 a3 = particle2.m_Position - particle.m_Position;
				float magnitude2 = a3.magnitude;
				if (magnitude2 > 0f)
				{
					particle.m_Position += a3 * ((magnitude2 - num) / magnitude2);
				}
			}
			else
			{
				particle.m_PrevPosition = particle.m_Position;
				particle.m_Position = particle.m_Transform.position;
			}
		}
	}

	private void ApplyParticlesToTransforms()
	{
		for (int i = 1; i < m_Particles.Count; i++)
		{
			Particle particle = m_Particles[i];
			Particle particle2 = m_Particles[particle.m_ParentIndex];
			if (particle2.m_Transform.childCount <= 1)
			{
				Vector3 direction = (!((Object)particle.m_Transform != (Object)null)) ? particle.m_EndOffset : particle.m_Transform.localPosition;
				Quaternion lhs = Quaternion.FromToRotation(particle2.m_Transform.TransformDirection(direction), particle.m_Position - particle2.m_Position);
				particle2.m_Transform.rotation = lhs * particle2.m_Transform.rotation;
			}
			if ((Object)particle.m_Transform != (Object)null)
			{
				particle.m_Transform.position = particle.m_Position;
			}
		}
	}
}

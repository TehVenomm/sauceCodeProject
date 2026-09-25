// Decompiled with JetBrains decompiler
// Type: DynamicBone
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[AddComponentMenu("Dynamic Bone/Dynamic Bone")]
public class DynamicBone : MonoBehaviour
{
  public Transform m_Root;
  public float m_UpdateRate = 60f;
  [Range(0.0f, 1f)]
  public float m_Damping = 0.1f;
  public AnimationCurve m_DampingDistrib;
  [Range(0.0f, 1f)]
  public float m_Elasticity = 0.1f;
  public AnimationCurve m_ElasticityDistrib;
  [Range(0.0f, 1f)]
  public float m_Stiffness = 0.1f;
  public AnimationCurve m_StiffnessDistrib;
  [Range(0.0f, 1f)]
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
  public DynamicBone.FreezeAxis m_FreezeAxis;
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
  private List<DynamicBone.Particle> m_Particles = new List<DynamicBone.Particle>();

  private void Start() => this.SetupParticles();

  private void Update()
  {
    if ((double) this.m_Weight <= 0.0 || this.m_DistantDisable && this.m_DistantDisabled)
      return;
    this.InitTransforms();
  }

  private void LateUpdate()
  {
    if (this.m_DistantDisable)
      this.CheckDistance();
    if ((double) this.m_Weight <= 0.0 || this.m_DistantDisable && this.m_DistantDisabled)
      return;
    this.UpdateDynamicBones(Time.deltaTime);
  }

  private void CheckDistance()
  {
    Transform transform = this.m_ReferenceObject;
    if (Object.op_Equality((Object) transform, (Object) null) && Object.op_Inequality((Object) Camera.main, (Object) null))
      transform = ((Component) Camera.main).transform;
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return;
    Vector3 vector3 = Vector3.op_Subtraction(transform.position, ((Component) this).transform.position);
    bool flag = (double) ((Vector3) ref vector3).sqrMagnitude > (double) this.m_DistanceToObject * (double) this.m_DistanceToObject;
    if (flag == this.m_DistantDisabled)
      return;
    if (!flag)
      this.ResetParticlesPosition();
    this.m_DistantDisabled = flag;
  }

  private void OnEnable() => this.ResetParticlesPosition();

  private void OnDisable() => this.InitTransforms();

  private void OnValidate()
  {
    this.m_UpdateRate = Mathf.Max(this.m_UpdateRate, 0.0f);
    this.m_Damping = Mathf.Clamp01(this.m_Damping);
    this.m_Elasticity = Mathf.Clamp01(this.m_Elasticity);
    this.m_Stiffness = Mathf.Clamp01(this.m_Stiffness);
    this.m_Inert = Mathf.Clamp01(this.m_Inert);
    this.m_Radius = Mathf.Max(this.m_Radius, 0.0f);
    if (!Application.isEditor || !Application.isPlaying)
      return;
    this.InitTransforms();
    this.SetupParticles();
  }

  private void OnDrawGizmosSelected()
  {
    if (!((Behaviour) this).enabled || Object.op_Equality((Object) this.m_Root, (Object) null))
      return;
    if (Application.isEditor && !Application.isPlaying && ((Component) this).transform.hasChanged)
    {
      this.InitTransforms();
      this.SetupParticles();
    }
    Gizmos.color = Color.white;
    for (int index = 0; index < this.m_Particles.Count; ++index)
    {
      DynamicBone.Particle particle1 = this.m_Particles[index];
      if (particle1.m_ParentIndex >= 0)
      {
        DynamicBone.Particle particle2 = this.m_Particles[particle1.m_ParentIndex];
        Gizmos.DrawLine(particle1.m_Position, particle2.m_Position);
      }
      if ((double) particle1.m_Radius > 0.0)
        Gizmos.DrawWireSphere(particle1.m_Position, particle1.m_Radius * this.m_ObjectScale);
    }
  }

  public void SetWeight(float w)
  {
    if ((double) this.m_Weight == (double) w)
      return;
    if ((double) w == 0.0)
      this.InitTransforms();
    else if ((double) this.m_Weight == 0.0)
      this.ResetParticlesPosition();
    this.m_Weight = w;
  }

  public float GetWeight() => this.m_Weight;

  private void UpdateDynamicBones(float t)
  {
    if (Object.op_Equality((Object) this.m_Root, (Object) null))
      return;
    this.m_ObjectScale = Mathf.Abs(((Component) this).transform.lossyScale.x);
    this.m_ObjectMove = Vector3.op_Subtraction(((Component) this).transform.position, this.m_ObjectPrevPosition);
    this.m_ObjectPrevPosition = ((Component) this).transform.position;
    int num1 = 1;
    if ((double) this.m_UpdateRate > 0.0)
    {
      float num2 = 1f / this.m_UpdateRate;
      this.m_Time += t;
      num1 = 0;
      while ((double) this.m_Time >= (double) num2)
      {
        this.m_Time -= num2;
        if (++num1 >= 3)
        {
          this.m_Time = 0.0f;
          break;
        }
      }
    }
    if (num1 > 0)
    {
      for (int index = 0; index < num1; ++index)
      {
        this.UpdateParticles1();
        this.UpdateParticles2();
        this.m_ObjectMove = Vector3.zero;
      }
    }
    else
      this.SkipUpdateParticles();
    this.ApplyParticlesToTransforms();
  }

  private void SetupParticles()
  {
    this.m_Particles.Clear();
    if (Object.op_Equality((Object) this.m_Root, (Object) null))
      return;
    this.m_LocalGravity = this.m_Root.InverseTransformDirection(this.m_Gravity);
    this.m_ObjectScale = ((Component) this).transform.lossyScale.x;
    this.m_ObjectPrevPosition = ((Component) this).transform.position;
    this.m_ObjectMove = Vector3.zero;
    this.m_BoneTotalLength = 0.0f;
    this.AppendParticles(this.m_Root, -1, 0.0f);
    for (int index = 0; index < this.m_Particles.Count; ++index)
    {
      DynamicBone.Particle particle = this.m_Particles[index];
      particle.m_Damping = this.m_Damping;
      particle.m_Elasticity = this.m_Elasticity;
      particle.m_Stiffness = this.m_Stiffness;
      particle.m_Inert = this.m_Inert;
      particle.m_Radius = this.m_Radius;
      if ((double) this.m_BoneTotalLength > 0.0)
      {
        float num = particle.m_BoneLength / this.m_BoneTotalLength;
        if (this.m_DampingDistrib != null && this.m_DampingDistrib.keys.Length != 0)
          particle.m_Damping *= this.m_DampingDistrib.Evaluate(num);
        if (this.m_ElasticityDistrib != null && this.m_ElasticityDistrib.keys.Length != 0)
          particle.m_Elasticity *= this.m_ElasticityDistrib.Evaluate(num);
        if (this.m_StiffnessDistrib != null && this.m_StiffnessDistrib.keys.Length != 0)
          particle.m_Stiffness *= this.m_StiffnessDistrib.Evaluate(num);
        if (this.m_InertDistrib != null && this.m_InertDistrib.keys.Length != 0)
          particle.m_Inert *= this.m_InertDistrib.Evaluate(num);
        if (this.m_RadiusDistrib != null && this.m_RadiusDistrib.keys.Length != 0)
          particle.m_Radius *= this.m_RadiusDistrib.Evaluate(num);
      }
      particle.m_Damping = Mathf.Clamp01(particle.m_Damping);
      particle.m_Elasticity = Mathf.Clamp01(particle.m_Elasticity);
      particle.m_Stiffness = Mathf.Clamp01(particle.m_Stiffness);
      particle.m_Inert = Mathf.Clamp01(particle.m_Inert);
      particle.m_Radius = Mathf.Max(particle.m_Radius, 0.0f);
    }
  }

  private void AppendParticles(Transform b, int parentIndex, float boneLength)
  {
    DynamicBone.Particle particle = new DynamicBone.Particle();
    particle.m_Transform = b;
    particle.m_ParentIndex = parentIndex;
    if (Object.op_Inequality((Object) b, (Object) null))
    {
      particle.m_Position = particle.m_PrevPosition = b.position;
      particle.m_InitLocalPosition = b.localPosition;
      particle.m_InitLocalRotation = b.localRotation;
    }
    else
    {
      Transform transform = this.m_Particles[parentIndex].m_Transform;
      if ((double) this.m_EndLength > 0.0)
      {
        Transform parent = transform.parent;
        particle.m_EndOffset = !Object.op_Inequality((Object) parent, (Object) null) ? new Vector3(this.m_EndLength, 0.0f, 0.0f) : Vector3.op_Multiply(transform.InverseTransformPoint(Vector3.op_Subtraction(Vector3.op_Multiply(transform.position, 2f), parent.position)), this.m_EndLength);
      }
      else
        particle.m_EndOffset = transform.InverseTransformPoint(Vector3.op_Addition(((Component) this).transform.TransformDirection(this.m_EndOffset), transform.position));
      particle.m_Position = particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset);
    }
    if (parentIndex >= 0)
    {
      double num = (double) boneLength;
      Vector3 vector3 = Vector3.op_Subtraction(this.m_Particles[parentIndex].m_Transform.position, particle.m_Position);
      double magnitude = (double) ((Vector3) ref vector3).magnitude;
      boneLength = (float) (num + magnitude);
      particle.m_BoneLength = boneLength;
      this.m_BoneTotalLength = Mathf.Max(this.m_BoneTotalLength, boneLength);
    }
    int count = this.m_Particles.Count;
    this.m_Particles.Add(particle);
    if (!Object.op_Inequality((Object) b, (Object) null))
      return;
    for (int index1 = 0; index1 < b.childCount; ++index1)
    {
      bool flag = false;
      if (this.m_Exclusions != null)
      {
        for (int index2 = 0; index2 < this.m_Exclusions.Count; ++index2)
        {
          if (Object.op_Equality((Object) this.m_Exclusions[index2], (Object) b.GetChild(index1)))
          {
            flag = true;
            break;
          }
        }
      }
      if (!flag)
        this.AppendParticles(b.GetChild(index1), count, boneLength);
    }
    if (b.childCount != 0 || (double) this.m_EndLength <= 0.0 && !Vector3.op_Inequality(this.m_EndOffset, Vector3.zero))
      return;
    this.AppendParticles((Transform) null, count, boneLength);
  }

  private void InitTransforms()
  {
    for (int index = 0; index < this.m_Particles.Count; ++index)
    {
      DynamicBone.Particle particle = this.m_Particles[index];
      if (Object.op_Inequality((Object) particle.m_Transform, (Object) null))
      {
        particle.m_Transform.localPosition = particle.m_InitLocalPosition;
        particle.m_Transform.localRotation = particle.m_InitLocalRotation;
      }
    }
  }

  private void ResetParticlesPosition()
  {
    for (int index = 0; index < this.m_Particles.Count; ++index)
    {
      DynamicBone.Particle particle = this.m_Particles[index];
      if (Object.op_Inequality((Object) particle.m_Transform, (Object) null))
      {
        particle.m_Position = particle.m_PrevPosition = particle.m_Transform.position;
      }
      else
      {
        Transform transform = this.m_Particles[particle.m_ParentIndex].m_Transform;
        particle.m_Position = particle.m_PrevPosition = transform.TransformPoint(particle.m_EndOffset);
      }
    }
    this.m_ObjectPrevPosition = ((Component) this).transform.position;
  }

  private void UpdateParticles1()
  {
    Vector3 gravity = this.m_Gravity;
    Vector3 normalized = ((Vector3) ref this.m_Gravity).normalized;
    Vector3 vector3_1 = this.m_Root.TransformDirection(this.m_LocalGravity);
    Vector3 vector3_2 = Vector3.op_Multiply(normalized, Mathf.Max(Vector3.Dot(vector3_1, normalized), 0.0f));
    Vector3 vector3_3 = Vector3.op_Multiply(Vector3.op_Addition(Vector3.op_Subtraction(gravity, vector3_2), this.m_Force), this.m_ObjectScale);
    for (int index = 0; index < this.m_Particles.Count; ++index)
    {
      DynamicBone.Particle particle1 = this.m_Particles[index];
      if (particle1.m_ParentIndex >= 0)
      {
        Vector3 vector3_4 = Vector3.op_Subtraction(particle1.m_Position, particle1.m_PrevPosition);
        Vector3 vector3_5 = Vector3.op_Multiply(this.m_ObjectMove, particle1.m_Inert);
        particle1.m_PrevPosition = Vector3.op_Addition(particle1.m_Position, vector3_5);
        DynamicBone.Particle particle2 = particle1;
        particle2.m_Position = Vector3.op_Addition(particle2.m_Position, Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(vector3_4, 1f - particle1.m_Damping), vector3_3), vector3_5));
      }
      else
      {
        particle1.m_PrevPosition = particle1.m_Position;
        particle1.m_Position = particle1.m_Transform.position;
      }
    }
  }

  private void UpdateParticles2()
  {
    Plane plane = new Plane();
    for (int index1 = 1; index1 < this.m_Particles.Count; ++index1)
    {
      DynamicBone.Particle particle1 = this.m_Particles[index1];
      DynamicBone.Particle particle2 = this.m_Particles[particle1.m_ParentIndex];
      Vector3 vector3_1;
      float magnitude1;
      if (Object.op_Inequality((Object) particle1.m_Transform, (Object) null))
      {
        vector3_1 = Vector3.op_Subtraction(particle2.m_Transform.position, particle1.m_Transform.position);
        magnitude1 = ((Vector3) ref vector3_1).magnitude;
      }
      else
      {
        Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
        vector3_1 = ((Matrix4x4) ref localToWorldMatrix).MultiplyVector(particle1.m_EndOffset);
        magnitude1 = ((Vector3) ref vector3_1).magnitude;
      }
      float num1 = Mathf.Lerp(1f, particle1.m_Stiffness, this.m_Weight);
      if ((double) num1 > 0.0 || (double) particle1.m_Elasticity > 0.0)
      {
        Matrix4x4 localToWorldMatrix = particle2.m_Transform.localToWorldMatrix;
        ((Matrix4x4) ref localToWorldMatrix).SetColumn(3, Vector4.op_Implicit(particle2.m_Position));
        Vector3 vector3_2 = !Object.op_Inequality((Object) particle1.m_Transform, (Object) null) ? ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(particle1.m_EndOffset) : ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(particle1.m_Transform.localPosition);
        Vector3 vector3_3 = Vector3.op_Subtraction(vector3_2, particle1.m_Position);
        DynamicBone.Particle particle3 = particle1;
        particle3.m_Position = Vector3.op_Addition(particle3.m_Position, Vector3.op_Multiply(vector3_3, particle1.m_Elasticity));
        if ((double) num1 > 0.0)
        {
          Vector3 vector3_4 = Vector3.op_Subtraction(vector3_2, particle1.m_Position);
          float magnitude2 = ((Vector3) ref vector3_4).magnitude;
          float num2 = (float) ((double) magnitude1 * (1.0 - (double) num1) * 2.0);
          if ((double) magnitude2 > (double) num2)
          {
            DynamicBone.Particle particle4 = particle1;
            particle4.m_Position = Vector3.op_Addition(particle4.m_Position, Vector3.op_Multiply(vector3_4, (magnitude2 - num2) / magnitude2));
          }
        }
      }
      if (this.m_Colliders != null)
      {
        float particleRadius = particle1.m_Radius * this.m_ObjectScale;
        for (int index2 = 0; index2 < this.m_Colliders.Count; ++index2)
        {
          DynamicBoneCollider collider = this.m_Colliders[index2];
          if (Object.op_Inequality((Object) collider, (Object) null) && ((Behaviour) collider).enabled)
            collider.Collide(ref particle1.m_Position, particleRadius);
        }
      }
      if (this.m_FreezeAxis != DynamicBone.FreezeAxis.None)
      {
        switch (this.m_FreezeAxis)
        {
          case DynamicBone.FreezeAxis.X:
            ((Plane) ref plane).SetNormalAndPosition(particle2.m_Transform.right, particle2.m_Position);
            break;
          case DynamicBone.FreezeAxis.Y:
            ((Plane) ref plane).SetNormalAndPosition(particle2.m_Transform.up, particle2.m_Position);
            break;
          case DynamicBone.FreezeAxis.Z:
            ((Plane) ref plane).SetNormalAndPosition(particle2.m_Transform.forward, particle2.m_Position);
            break;
        }
        DynamicBone.Particle particle5 = particle1;
        particle5.m_Position = Vector3.op_Subtraction(particle5.m_Position, Vector3.op_Multiply(((Plane) ref plane).normal, ((Plane) ref plane).GetDistanceToPoint(particle1.m_Position)));
      }
      Vector3 vector3_5 = Vector3.op_Subtraction(particle2.m_Position, particle1.m_Position);
      float magnitude3 = ((Vector3) ref vector3_5).magnitude;
      if ((double) magnitude3 > 0.0)
      {
        DynamicBone.Particle particle6 = particle1;
        particle6.m_Position = Vector3.op_Addition(particle6.m_Position, Vector3.op_Multiply(vector3_5, (magnitude3 - magnitude1) / magnitude3));
      }
    }
  }

  private void SkipUpdateParticles()
  {
    for (int index = 0; index < this.m_Particles.Count; ++index)
    {
      DynamicBone.Particle particle1 = this.m_Particles[index];
      if (particle1.m_ParentIndex >= 0)
      {
        DynamicBone.Particle particle2 = particle1;
        particle2.m_PrevPosition = Vector3.op_Addition(particle2.m_PrevPosition, this.m_ObjectMove);
        DynamicBone.Particle particle3 = particle1;
        particle3.m_Position = Vector3.op_Addition(particle3.m_Position, this.m_ObjectMove);
        DynamicBone.Particle particle4 = this.m_Particles[particle1.m_ParentIndex];
        float magnitude1;
        if (Object.op_Inequality((Object) particle1.m_Transform, (Object) null))
        {
          Vector3 vector3 = Vector3.op_Subtraction(particle4.m_Transform.position, particle1.m_Transform.position);
          magnitude1 = ((Vector3) ref vector3).magnitude;
        }
        else
        {
          Matrix4x4 localToWorldMatrix = particle4.m_Transform.localToWorldMatrix;
          Vector3 vector3 = ((Matrix4x4) ref localToWorldMatrix).MultiplyVector(particle1.m_EndOffset);
          magnitude1 = ((Vector3) ref vector3).magnitude;
        }
        float num1 = Mathf.Lerp(1f, particle1.m_Stiffness, this.m_Weight);
        if ((double) num1 > 0.0)
        {
          Matrix4x4 localToWorldMatrix = particle4.m_Transform.localToWorldMatrix;
          ((Matrix4x4) ref localToWorldMatrix).SetColumn(3, Vector4.op_Implicit(particle4.m_Position));
          Vector3 vector3 = Vector3.op_Subtraction(!Object.op_Inequality((Object) particle1.m_Transform, (Object) null) ? ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(particle1.m_EndOffset) : ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(particle1.m_Transform.localPosition), particle1.m_Position);
          float magnitude2 = ((Vector3) ref vector3).magnitude;
          float num2 = (float) ((double) magnitude1 * (1.0 - (double) num1) * 2.0);
          if ((double) magnitude2 > (double) num2)
          {
            DynamicBone.Particle particle5 = particle1;
            particle5.m_Position = Vector3.op_Addition(particle5.m_Position, Vector3.op_Multiply(vector3, (magnitude2 - num2) / magnitude2));
          }
        }
        Vector3 vector3_1 = Vector3.op_Subtraction(particle4.m_Position, particle1.m_Position);
        float magnitude3 = ((Vector3) ref vector3_1).magnitude;
        if ((double) magnitude3 > 0.0)
        {
          DynamicBone.Particle particle6 = particle1;
          particle6.m_Position = Vector3.op_Addition(particle6.m_Position, Vector3.op_Multiply(vector3_1, (magnitude3 - magnitude1) / magnitude3));
        }
      }
      else
      {
        particle1.m_PrevPosition = particle1.m_Position;
        particle1.m_Position = particle1.m_Transform.position;
      }
    }
  }

  private void ApplyParticlesToTransforms()
  {
    for (int index = 1; index < this.m_Particles.Count; ++index)
    {
      DynamicBone.Particle particle1 = this.m_Particles[index];
      DynamicBone.Particle particle2 = this.m_Particles[particle1.m_ParentIndex];
      if (particle2.m_Transform.childCount <= 1)
      {
        Vector3 vector3 = !Object.op_Inequality((Object) particle1.m_Transform, (Object) null) ? particle1.m_EndOffset : particle1.m_Transform.localPosition;
        Quaternion rotation = Quaternion.FromToRotation(particle2.m_Transform.TransformDirection(vector3), Vector3.op_Subtraction(particle1.m_Position, particle2.m_Position));
        particle2.m_Transform.rotation = Quaternion.op_Multiply(rotation, particle2.m_Transform.rotation);
      }
      if (Object.op_Inequality((Object) particle1.m_Transform, (Object) null))
        particle1.m_Transform.position = particle1.m_Position;
    }
  }

  public enum FreezeAxis
  {
    None,
    X,
    Y,
    Z,
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
}

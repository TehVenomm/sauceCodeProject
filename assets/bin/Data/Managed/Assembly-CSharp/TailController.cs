// Decompiled with JetBrains decompiler
// Type: TailController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TailController : MonoBehaviour
{
  public const float DEFAULT_LERP_FRAME = 0.8f;
  [SerializeField]
  private float gravity = 9.8f;
  [SerializeField]
  private float angleMax = 30f;
  [SerializeField]
  private float groundHeight;
  [SerializeField]
  private float radius = 1.5f;
  [SerializeField]
  private int uniqueID;
  [SerializeField]
  private bool isUpdate = true;
  [SerializeField]
  private Transform[] pointList;
  private List<TailController.JointInfo> m_jointInfoList = new List<TailController.JointInfo>();
  private Vector3[] m_prevPositionList;
  private Quaternion[] m_lerpRotationList;
  private Vector3[] m_lerpPositionList;
  private float m_finishLerpTime;
  private float m_lerpTime;

  private void Awake()
  {
    int length = this.pointList.Length;
    for (int index = 1; index < length; ++index)
    {
      Vector3 localPosition = this.pointList[index].localPosition;
      this.m_jointInfoList.Add(new TailController.JointInfo()
      {
        distance = ((Vector3) ref localPosition).magnitude,
        basisAxis = ((Vector3) ref localPosition).normalized
      });
    }
    this.m_prevPositionList = new Vector3[length];
    this.UpdatePreviousPositionList();
    this.m_lerpPositionList = new Vector3[length];
    this.m_lerpRotationList = new Quaternion[length];
    this.UpdateLerpInfo();
  }

  private void LateUpdate()
  {
    if (this.isUpdate)
    {
      float num = this.groundHeight + this.radius;
      for (int index = 1; index < this.pointList.Length; ++index)
      {
        TailController.JointInfo jointInfo = this.m_jointInfoList[index - 1];
        Transform point1 = this.pointList[index - 1];
        Transform point2 = this.pointList[index];
        Vector3 position = point1.position;
        Vector3 vector3_1 = point1.TransformDirection(jointInfo.basisAxis);
        Vector3 normalized1 = ((Vector3) ref vector3_1).normalized;
        vector3_1 = Vector3.op_Subtraction(this.m_prevPositionList[index], position);
        Vector3 normalized2 = ((Vector3) ref vector3_1).normalized;
        normalized2.y -= this.gravity * Time.deltaTime;
        Quaternion quaternion1 = Quaternion.AngleAxis(Mathf.Min(Vector3.Angle(normalized1, normalized2), this.angleMax), Vector3.Cross(normalized1, normalized2));
        Vector3 vector3_2 = Quaternion.op_Multiply(quaternion1, normalized1);
        ((Vector3) ref vector3_2).Normalize();
        Vector3 vector3_3 = Vector3.op_Addition(position, Vector3.op_Multiply(vector3_2, jointInfo.distance));
        Quaternion quaternion2 = Quaternion.op_Multiply(quaternion1, point1.rotation);
        Vector3 vector3_4 = vector3_3;
        if ((double) vector3_4.y < (double) num)
        {
          vector3_4.y = num;
          vector3_1 = Vector3.op_Subtraction(vector3_3, position);
          Vector3 normalized3 = ((Vector3) ref vector3_1).normalized;
          vector3_1 = Vector3.op_Subtraction(vector3_4, position);
          Vector3 normalized4 = ((Vector3) ref vector3_1).normalized;
          Vector3 vector3_5 = normalized4;
          quaternion2 = Quaternion.op_Multiply(Quaternion.FromToRotation(normalized3, vector3_5), quaternion2);
          vector3_3 = Vector3.op_Addition(position, Vector3.op_Multiply(normalized4, jointInfo.distance));
        }
        point2.position = vector3_3;
        point2.rotation = quaternion2;
      }
      this.UpdatePreviousPositionList();
    }
    else
    {
      if ((double) this.m_finishLerpTime <= 0.0)
        return;
      this.m_lerpTime += Time.deltaTime;
      float num = Mathf.Clamp01(this.m_lerpTime / this.m_finishLerpTime);
      int length = this.pointList.Length;
      for (int index = 0; index < length; ++index)
      {
        Transform point = this.pointList[index];
        point.localRotation = Quaternion.Lerp(this.m_lerpRotationList[index], point.localRotation, num);
        point.localPosition = Vector3.Lerp(this.m_lerpPositionList[index], point.localPosition, num);
      }
      if ((double) num < 1.0)
        return;
      this.m_finishLerpTime = 0.0f;
    }
  }

  public void RequestLerp(float lerpFinishTime)
  {
    if ((double) lerpFinishTime <= 0.0)
      return;
    this.m_finishLerpTime = lerpFinishTime;
    this.m_lerpTime = 0.0f;
    this.UpdateLerpInfo();
  }

  private void UpdateLerpInfo()
  {
    for (int index = 0; index < this.m_prevPositionList.Length; ++index)
    {
      this.m_lerpRotationList[index] = this.pointList[index].localRotation;
      this.m_lerpPositionList[index] = this.pointList[index].localPosition;
    }
  }

  private void UpdatePreviousPositionList()
  {
    for (int index = 0; index < this.m_prevPositionList.Length; ++index)
      this.m_prevPositionList[index] = this.pointList[index].position;
  }

  public void SetPreviousPositionList(Vector3[] posList)
  {
    if (posList == null || this.m_prevPositionList == null || posList.Length != this.m_prevPositionList.Length)
      return;
    int length = posList.Length;
    for (int index = 0; index < length; ++index)
      this.m_prevPositionList[index] = posList[index];
  }

  public Vector3[] PreviousPositionList => this.m_prevPositionList;

  public void SetUpdateFlag(bool flag, bool isUpdatePreviousPosition = true)
  {
    if (this.isUpdate == flag)
      return;
    this.isUpdate = flag;
    if (!(this.isUpdate & isUpdatePreviousPosition))
      return;
    this.UpdatePreviousPositionList();
  }

  public int UniqueID => this.uniqueID;

  public class JointInfo
  {
    public float distance = 1f;
    public Vector3 basisAxis = Vector3.zero;
  }
}

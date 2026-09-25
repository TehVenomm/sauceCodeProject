// Decompiled with JetBrains decompiler
// Type: TestRaycast
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TestRaycast : MonoBehaviour
{
  public GameObject checkObject;
  public GameObject endObject;

  private void Update()
  {
    if (Object.op_Equality((Object) this.checkObject, (Object) null) || Object.op_Equality((Object) this.endObject, (Object) null))
      return;
    this.checkObject.transform.position = this.CheckWarpPos();
  }

  private Vector3 CheckWarpPos()
  {
    float num1 = (((Component) this).GetComponent<Collider>() as SphereCollider).radius * ((Component) this).transform.lossyScale.x;
    Vector3 position1 = ((Component) this).transform.position;
    Vector3 position2 = this.endObject.transform.position;
    position1.y = num1;
    position2.y = num1;
    Vector3 vector3 = Vector3.op_Subtraction(position2, position1);
    float magnitude = ((Vector3) ref vector3).magnitude;
    float num2 = 5f;
    float num3 = 2f;
    if ((double) num2 >= (double) magnitude)
      return ((Component) this).transform.position;
    List<TestRaycast.CastHitInfo> castHitInfoList = new List<TestRaycast.CastHitInfo>();
    RaycastHit[] raycastHitArray1 = Physics.SphereCastAll(position2, num1, Vector3.op_UnaryNegation(vector3), magnitude, 393728 /*0x060200*/);
    int index1 = 0;
    for (int length = raycastHitArray1.Length; index1 < length; ++index1)
      castHitInfoList.Add(new TestRaycast.CastHitInfo()
      {
        distance = ((RaycastHit) ref raycastHitArray1[index1]).distance,
        faceToEnd = true,
        collider = ((RaycastHit) ref raycastHitArray1[index1]).collider
      });
    castHitInfoList.Add(new TestRaycast.CastHitInfo()
    {
      distance = 0.0f,
      faceToEnd = false,
      collider = (Collider) null
    });
    RaycastHit[] raycastHitArray2 = Physics.SphereCastAll(position1, num1, vector3, magnitude, 393728 /*0x060200*/);
    int index2 = 0;
    for (int length = raycastHitArray2.Length; index2 < length; ++index2)
      castHitInfoList.Add(new TestRaycast.CastHitInfo()
      {
        distance = magnitude - ((RaycastHit) ref raycastHitArray2[index2]).distance,
        faceToEnd = false,
        collider = ((RaycastHit) ref raycastHitArray2[index2]).collider
      });
    castHitInfoList.Add(new TestRaycast.CastHitInfo()
    {
      distance = magnitude,
      faceToEnd = true,
      collider = (Collider) null
    });
    castHitInfoList.Sort((Comparison<TestRaycast.CastHitInfo>) ((a, b) =>
    {
      float num4 = a.distance - b.distance;
      if ((double) num4 == 0.0)
      {
        if (a.faceToEnd == b.faceToEnd)
          return 0;
        return !a.faceToEnd ? -1 : 1;
      }
      return (double) num4 <= 0.0 ? -1 : 1;
    }));
    int index3 = 0;
    for (int count = castHitInfoList.Count; index3 < count; ++index3)
    {
      TestRaycast.CastHitInfo castHitInfo1 = castHitInfoList[index3];
      if (!castHitInfo1.checkCollider && !Object.op_Equality((Object) castHitInfo1.collider, (Object) null))
      {
        int index4 = index3;
        while (0 <= index4 && index4 < count)
        {
          TestRaycast.CastHitInfo castHitInfo2 = castHitInfoList[index4];
          if (index4 != index3)
          {
            if (Object.op_Equality((Object) castHitInfo2.collider, (Object) castHitInfo1.collider))
            {
              castHitInfo2.checkCollider = true;
              break;
            }
            castHitInfo2.enable = false;
          }
          if (castHitInfo1.faceToEnd)
            ++index4;
          else
            --index4;
        }
      }
    }
    float num5 = magnitude;
    for (int index5 = castHitInfoList.Count - 2; index5 >= 0; --index5)
    {
      TestRaycast.CastHitInfo castHitInfo3 = castHitInfoList[index5];
      TestRaycast.CastHitInfo castHitInfo4 = castHitInfoList[index5 + 1];
      if (castHitInfo3.enable && castHitInfo4.enable && !castHitInfo3.faceToEnd && castHitInfo4.faceToEnd)
      {
        if ((double) castHitInfo3.distance <= (double) num2 && (double) castHitInfo4.distance >= (double) num2)
        {
          num5 = num2;
          break;
        }
        if ((double) castHitInfo3.distance >= (double) num2)
          num5 = castHitInfo3.distance;
        else if ((double) castHitInfo4.distance >= (double) num2 - (double) num3)
        {
          num5 = castHitInfo4.distance;
          break;
        }
      }
    }
    return Vector3.op_Subtraction(this.endObject.transform.position, Vector3.op_Multiply(((Vector3) ref vector3).normalized, num5));
  }

  private Vector3 CheckWarpPos2()
  {
    float num1 = (((Component) this).GetComponent<Collider>() as SphereCollider).radius * ((Component) this).transform.lossyScale.x;
    Vector3 position1 = ((Component) this).transform.position;
    Vector3 position2 = this.endObject.transform.position;
    position1.y = num1;
    position2.y = num1;
    Vector3 vector3 = Vector3.op_Subtraction(position2, position1);
    float magnitude = ((Vector3) ref vector3).magnitude;
    float num2 = 5f;
    if ((double) num2 >= (double) magnitude)
      return ((Component) this).transform.position;
    List<TestRaycast.CastHitInfo> castHitInfoList = new List<TestRaycast.CastHitInfo>();
    RaycastHit[] raycastHitArray1 = Physics.SphereCastAll(position2, num1, Vector3.op_UnaryNegation(vector3), magnitude, 393728 /*0x060200*/);
    int index1 = 0;
    for (int length = raycastHitArray1.Length; index1 < length; ++index1)
      castHitInfoList.Add(new TestRaycast.CastHitInfo()
      {
        distance = ((RaycastHit) ref raycastHitArray1[index1]).distance,
        faceToEnd = true
      });
    castHitInfoList.Add(new TestRaycast.CastHitInfo()
    {
      distance = 0.0f,
      faceToEnd = false
    });
    RaycastHit[] raycastHitArray2 = Physics.SphereCastAll(position1, num1, vector3, magnitude, 393728 /*0x060200*/);
    int index2 = 0;
    for (int length = raycastHitArray2.Length; index2 < length; ++index2)
      castHitInfoList.Add(new TestRaycast.CastHitInfo()
      {
        distance = magnitude - ((RaycastHit) ref raycastHitArray2[index2]).distance,
        faceToEnd = false
      });
    castHitInfoList.Add(new TestRaycast.CastHitInfo()
    {
      distance = magnitude,
      faceToEnd = true
    });
    castHitInfoList.Sort((Comparison<TestRaycast.CastHitInfo>) ((a, b) =>
    {
      float num3 = a.distance - b.distance;
      if ((double) num3 == 0.0)
      {
        if (a.faceToEnd == b.faceToEnd)
          return 0;
        return !a.faceToEnd ? -1 : 1;
      }
      return (double) num3 <= 0.0 ? -1 : 1;
    }));
    Debug.Log((object) "----------------------------------------");
    int index3 = 0;
    for (int count = castHitInfoList.Count; index3 < count; ++index3)
      Debug.Log((object) $"############ : {(object) castHitInfoList[index3].distance}, {castHitInfoList[index3].faceToEnd.ToString()}");
    int index4 = 0;
    TestRaycast.CastHitInfo castHitInfo1 = (TestRaycast.CastHitInfo) null;
    while (index4 < castHitInfoList.Count)
    {
      TestRaycast.CastHitInfo castHitInfo2 = castHitInfoList[index4];
      if (castHitInfo1 == null)
      {
        castHitInfo1 = castHitInfo2;
        ++index4;
      }
      else if (castHitInfo2.faceToEnd == castHitInfo1.faceToEnd)
      {
        if (castHitInfo2.faceToEnd)
        {
          castHitInfoList.RemoveAt(index4);
        }
        else
        {
          castHitInfoList.RemoveAt(index4 - 1);
          castHitInfo1 = castHitInfo2;
        }
      }
      else
      {
        castHitInfo1 = castHitInfo2;
        ++index4;
      }
    }
    int index5 = 0;
    while (index5 + 1 < castHitInfoList.Count)
    {
      TestRaycast.CastHitInfo castHitInfo3 = castHitInfoList[index5];
      TestRaycast.CastHitInfo castHitInfo4 = castHitInfoList[index5 + 1];
      if ((double) castHitInfo3.distance == (double) castHitInfo4.distance && castHitInfo3.faceToEnd != castHitInfo4.faceToEnd)
        castHitInfoList.RemoveRange(index5, 2);
      else
        ++index5;
    }
    Debug.Log((object) "----------------------------------------");
    int index6 = 0;
    for (int count = castHitInfoList.Count; index6 < count; ++index6)
      Debug.Log((object) $"############ : {(object) castHitInfoList[index6].distance}, {castHitInfoList[index6].faceToEnd.ToString()}");
    float num4 = magnitude;
    int index7 = 0;
    for (int count = castHitInfoList.Count; index7 + 1 < count; ++index7)
    {
      TestRaycast.CastHitInfo castHitInfo5 = castHitInfoList[index7];
      TestRaycast.CastHitInfo castHitInfo6 = castHitInfoList[index7 + 1];
      if (!castHitInfo5.faceToEnd && castHitInfo6.faceToEnd)
      {
        if ((double) castHitInfo5.distance >= (double) num2)
        {
          num4 = castHitInfo5.distance;
          break;
        }
        if ((double) castHitInfo5.distance <= (double) num2 && (double) castHitInfo6.distance >= (double) num2)
        {
          num4 = num2;
          break;
        }
      }
    }
    Debug.Log((object) ("res : " + (object) num4));
    return Vector3.op_Subtraction(this.endObject.transform.position, Vector3.op_Multiply(((Vector3) ref vector3).normalized, num4));
  }

  private Vector3 CheckWarpPos3()
  {
    float num1 = 5f;
    SphereCollider component = ((Component) this).GetComponent<Collider>() as SphereCollider;
    Vector3 vector3_1 = Vector3.op_Subtraction(((Component) this).transform.position, this.endObject.transform.position);
    float magnitude = ((Vector3) ref vector3_1).magnitude;
    if ((double) magnitude < (double) num1)
      return this.endObject.transform.position;
    bool flag = true;
    RaycastHit raycastHit;
    if (Physics.SphereCast(this.endObject.transform.position, component.radius * ((Component) this).transform.lossyScale.x, vector3_1, ref raycastHit, magnitude, 393728 /*0x060200*/) && (double) ((RaycastHit) ref raycastHit).distance < (double) num1)
      flag = false;
    if (flag)
      return Vector3.op_Addition(this.endObject.transform.position, Vector3.op_Multiply(((Vector3) ref vector3_1).normalized, num1));
    Vector3 vector3_2 = Vector3.op_Subtraction(this.endObject.transform.position, ((Component) this).transform.position);
    RaycastHit[] raycastHitArray = Physics.SphereCastAll(((Component) this).transform.position, component.radius * ((Component) this).transform.lossyScale.x, vector3_2, magnitude - num1, 393728 /*0x060200*/);
    if (raycastHitArray.Length == 0)
      return this.endObject.transform.position;
    float num2 = 0.0f;
    int index = 0;
    for (int length = raycastHitArray.Length; index < length; ++index)
    {
      if ((double) num2 < (double) ((RaycastHit) ref raycastHitArray[index]).distance)
        num2 = ((RaycastHit) ref raycastHitArray[index]).distance;
    }
    return Vector3.op_Addition(((Component) this).transform.position, Vector3.op_Multiply(((Vector3) ref vector3_2).normalized, num2));
  }

  private class CastHitInfo
  {
    public float distance;
    public bool faceToEnd;
    public Collider collider;
    public bool enable = true;
    public bool checkCollider;
  }
}

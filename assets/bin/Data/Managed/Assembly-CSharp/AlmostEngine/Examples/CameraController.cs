// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.CameraController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Examples;

public class CameraController : MonoBehaviour
{
  public bool m_MouseLookOnClickOnly = true;
  public float m_RotationCoeff = 200f;
  public float m_TranslationCoeff = 8f;
  public float m_TranslationMouseCoeff = 2f;
  public float m_TranslationMouseScrollCoeff = 25f;
  private Transform m_Character;
  public Transform m_Head;
  private Vector3 m_Mouse;

  private void Start()
  {
    this.m_Character = ((Component) this).transform;
    if (Object.op_Equality((Object) this.m_Head, (Object) null))
      this.m_Head = ((Component) ((Component) this).GetComponentInChildren<Camera>()).transform;
    this.m_Mouse = Input.mousePosition;
  }

  private void Update()
  {
    float num1 = Input.GetAxis("Vertical") * Time.deltaTime * this.m_TranslationCoeff;
    float num2 = Input.GetAxis("Horizontal") * Time.deltaTime * this.m_TranslationCoeff;
    Transform transform1 = ((Component) this.m_Character).transform;
    transform1.position = Vector3.op_Addition(transform1.position, Vector3.op_Addition(Vector3.op_Multiply(((Component) this.m_Head).transform.forward, num1), Vector3.op_Multiply(((Component) this.m_Head).transform.right, num2)));
    if (Input.GetMouseButtonDown(2))
      this.m_Mouse = Input.mousePosition;
    if (Input.GetMouseButton(2))
    {
      float num3 = -Vector3.op_Subtraction(Input.mousePosition, this.m_Mouse).y * this.m_TranslationMouseCoeff * Time.deltaTime;
      float num4 = -Vector3.op_Subtraction(Input.mousePosition, this.m_Mouse).x * this.m_TranslationMouseCoeff * Time.deltaTime;
      Transform transform2 = ((Component) this.m_Character).transform;
      transform2.position = Vector3.op_Addition(transform2.position, Vector3.op_Addition(Vector3.op_Multiply(((Component) this.m_Head).transform.up, num3), Vector3.op_Multiply(((Component) this.m_Head).transform.right, num4)));
      this.m_Mouse = Input.mousePosition;
    }
    float num5 = Input.mouseScrollDelta.y * this.m_TranslationMouseScrollCoeff * Time.deltaTime;
    Transform transform3 = ((Component) this.m_Character).transform;
    transform3.position = Vector3.op_Addition(transform3.position, Vector3.op_Multiply(((Component) this.m_Head).transform.forward, num5));
    if (this.m_MouseLookOnClickOnly && !Input.GetMouseButton(1))
      return;
    float num6 = Input.GetAxis("Mouse X") * Time.deltaTime * this.m_RotationCoeff;
    this.m_Head.localRotation = this.ClampRotationAroundXAxis(Quaternion.op_Multiply(this.m_Head.localRotation, Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * Time.deltaTime * this.m_RotationCoeff, Vector3.right)));
    Transform character = this.m_Character;
    character.localRotation = Quaternion.op_Multiply(character.localRotation, Quaternion.AngleAxis(num6, Vector3.up));
  }

  private Quaternion ClampRotationAroundXAxis(Quaternion q)
  {
    q.x /= q.w;
    q.y /= q.w;
    q.z /= q.w;
    q.w = 1f;
    float num = Mathf.Clamp(114.59156f * Mathf.Atan(q.x), -80f, 80f);
    q.x = Mathf.Tan((float) Math.PI / 360f * num);
    return q;
  }
}

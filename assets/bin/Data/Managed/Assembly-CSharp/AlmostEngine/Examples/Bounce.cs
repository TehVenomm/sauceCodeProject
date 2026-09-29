// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Examples.Bounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace AlmostEngine.Examples;

public class Bounce : MonoBehaviour
{
  private Vector3 m_Origin;
  private float m_Offset;

  private void Start()
  {
    this.m_Origin = ((Component) this).transform.position;
    this.m_Offset = 3.14159274f * Random.value;
  }

  private void Update()
  {
    ((Component) this).transform.position = Vector3.op_Addition(this.m_Origin, new Vector3(0.0f, Mathf.Abs(Mathf.Sin(this.m_Offset + 3f * Time.time)), 0.0f));
  }
}

// Decompiled with JetBrains decompiler
// Type: InstantiateObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class InstantiateObject : MonoBehaviour
{
  public GameObject prefab;

  private void Awake()
  {
    if (!Object.op_Inequality((Object) this.prefab, (Object) null))
      return;
    Transform transform1 = ((Component) this).transform;
    Transform transform2 = ResourceUtility.Realizes((Object) this.prefab, transform1.parent, ((Component) this).gameObject.layer);
    transform2.localPosition = transform1.localPosition;
    transform2.localRotation = transform1.localRotation;
    transform2.localScale = transform1.localScale;
    Object.DestroyImmediate((Object) ((Component) this).gameObject);
  }
}

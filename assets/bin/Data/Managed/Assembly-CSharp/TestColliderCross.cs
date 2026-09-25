// Decompiled with JetBrains decompiler
// Type: TestColliderCross
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TestColliderCross : MonoBehaviour
{
  public GameObject checkObject;
  public GameObject moveObject;

  private void Start()
  {
  }

  private void Update()
  {
    Collider component = ((Component) this).GetComponent<Collider>();
    if (!Object.op_Inequality((Object) component, (Object) null) || !Object.op_Inequality((Object) this.checkObject, (Object) null))
      return;
    Vector3 vector3 = Utility.ClosestPointOnCollider(component, this.checkObject.transform.position);
    Debug.Log((object) ("############### : " + (object) vector3));
    if (!Object.op_Inequality((Object) this.moveObject, (Object) null))
      return;
    this.moveObject.transform.position = vector3;
  }
}

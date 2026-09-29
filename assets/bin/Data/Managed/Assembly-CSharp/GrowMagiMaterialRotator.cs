// Decompiled with JetBrains decompiler
// Type: GrowMagiMaterialRotator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class GrowMagiMaterialRotator : MonoBehaviour
{
  public float speed;
  public Vector3 axis;

  private void Awake()
  {
    this.Setup(new Vector3(Random.value * 360f, Random.value * 360f, Random.value * 360f), Random.Range(180f, 540f));
  }

  public void Setup(Vector3 axis, float speed)
  {
    this.axis = axis;
    this.speed = speed;
  }

  private void Update()
  {
    ((Component) this).transform.Rotate(this.axis, this.speed * Time.deltaTime);
  }
}

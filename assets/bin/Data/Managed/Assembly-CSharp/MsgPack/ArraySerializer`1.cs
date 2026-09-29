// Decompiled with JetBrains decompiler
// Type: MsgPack.ArraySerializer`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack.Serialization;

#nullable disable
namespace MsgPack;

public class ArraySerializer<T>(SerializationContext ownerContext) : MessagePackSerializer<T[]>(ownerContext)
{
  protected override void PackToCore(Packer packer, T[] objectTree)
  {
    MessagePackSerializer<T> serializer = this.OwnerContext.GetSerializer<T>();
    packer.PackArrayHeader(objectTree.Length);
    foreach (T obj in objectTree)
      serializer.PackTo(packer, obj);
  }

  protected override T[] UnpackFromCore(Unpacker unpacker)
  {
    MessagePackSerializer<T> serializer = this.OwnerContext.GetSerializer<T>();
    int length = unpacker.IsArrayHeader ? UnpackHelpers.GetItemsCount(unpacker) : throw SerializationExceptions.NewIsNotArrayHeader();
    T[] objArray = new T[length];
    if (!unpacker.IsArrayHeader)
      throw SerializationExceptions.NewIsNotArrayHeader();
    for (int index = 0; index < length; ++index)
    {
      if (!unpacker.Read())
        throw SerializationExceptions.NewMissingItem(index);
      T obj;
      if (!unpacker.IsArrayHeader && !unpacker.IsMapHeader)
      {
        obj = serializer.UnpackFrom(unpacker);
      }
      else
      {
        using (Unpacker unpacker1 = unpacker.ReadSubtree())
          obj = serializer.UnpackFrom(unpacker1);
      }
      objArray[index] = obj;
    }
    return objArray;
  }
}

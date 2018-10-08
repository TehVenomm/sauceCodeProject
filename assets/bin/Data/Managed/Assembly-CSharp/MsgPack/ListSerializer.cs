using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace MsgPack
{
	public class ListSerializer<T>
	{
		public ListSerializer(SerializationContext ownerContext)
			: base(ownerContext)
		{
		}

		protected override void PackToCore(Packer packer, List<T> objectTree)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			MessagePackSerializer<T> serializer = base.get_OwnerContext().GetSerializer<T>();
			T[] array = objectTree.ToArray();
			packer.PackArrayHeader(array.Length);
			T[] array2 = array;
			foreach (T val in array2)
			{
				serializer.PackTo(packer, val);
			}
		}

		protected override List<T> UnpackFromCore(Unpacker unpacker)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Expected O, but got Unknown
			MessagePackSerializer<T> serializer = base.get_OwnerContext().GetSerializer<T>();
			if (!unpacker.get_IsArrayHeader())
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			int itemsCount = UnpackHelpers.GetItemsCount(unpacker);
			List<T> list = new List<T>();
			if (!unpacker.get_IsArrayHeader())
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			for (int i = 0; i < itemsCount; i++)
			{
				if (!unpacker.Read())
				{
					throw SerializationExceptions.NewMissingItem(i);
				}
				T item = default(T);
				if (!unpacker.get_IsArrayHeader() && !unpacker.get_IsMapHeader())
				{
					item = serializer.UnpackFrom(unpacker);
				}
				else
				{
					Unpacker val = unpacker.ReadSubtree();
					try
					{
						item = serializer.UnpackFrom(val);
					}
					finally
					{
						((IDisposable)val)?.Dispose();
					}
				}
				list.Add(item);
			}
			return list;
		}
	}
}

using MsgPack.Serialization;
using System.Collections.Generic;

namespace MsgPack
{
	public class ListSerializer<T> : MessagePackSerializer<List<T>>
	{
		public ListSerializer(SerializationContext ownerContext)
			: base(ownerContext)
		{
		}

		protected override void PackToCore(Packer packer, List<T> objectTree)
		{
			MessagePackSerializer<T> serializer = base.OwnerContext.GetSerializer<T>();
			T[] array = objectTree.ToArray();
			packer.PackArrayHeader(array.Length);
			T[] array2 = array;
			foreach (T objectTree2 in array2)
			{
				serializer.PackTo(packer, objectTree2);
			}
		}

		protected override List<T> UnpackFromCore(Unpacker unpacker)
		{
			MessagePackSerializer<T> serializer = base.OwnerContext.GetSerializer<T>();
			if (!unpacker.IsArrayHeader)
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			int itemsCount = UnpackHelpers.GetItemsCount(unpacker);
			List<T> list = new List<T>();
			if (!unpacker.IsArrayHeader)
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
				if (!unpacker.IsArrayHeader && !unpacker.IsMapHeader)
				{
					item = serializer.UnpackFrom(unpacker);
				}
				else
				{
					using (Unpacker unpacker2 = unpacker.ReadSubtree())
					{
						item = serializer.UnpackFrom(unpacker2);
					}
				}
				list.Add(item);
			}
			return list;
		}
	}
}

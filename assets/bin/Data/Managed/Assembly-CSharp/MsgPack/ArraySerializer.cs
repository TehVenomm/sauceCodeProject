using MsgPack.Serialization;

namespace MsgPack
{
	public class ArraySerializer<T> : MessagePackSerializer<T[]>
	{
		public ArraySerializer(SerializationContext ownerContext)
			: base(ownerContext)
		{
		}

		protected override void PackToCore(Packer packer, T[] objectTree)
		{
			MessagePackSerializer<T> serializer = base.OwnerContext.GetSerializer<T>();
			packer.PackArrayHeader(objectTree.Length);
			foreach (T objectTree2 in objectTree)
			{
				serializer.PackTo(packer, objectTree2);
			}
		}

		protected override T[] UnpackFromCore(Unpacker unpacker)
		{
			MessagePackSerializer<T> serializer = base.OwnerContext.GetSerializer<T>();
			if (!unpacker.IsArrayHeader)
			{
				throw SerializationExceptions.NewIsNotArrayHeader();
			}
			int itemsCount = UnpackHelpers.GetItemsCount(unpacker);
			T[] array = new T[itemsCount];
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
				T val = default(T);
				if (!unpacker.IsArrayHeader && !unpacker.IsMapHeader)
				{
					val = serializer.UnpackFrom(unpacker);
				}
				else
				{
					using (Unpacker unpacker2 = unpacker.ReadSubtree())
					{
						val = serializer.UnpackFrom(unpacker2);
					}
				}
				array[i] = val;
			}
			return array;
		}
	}
}

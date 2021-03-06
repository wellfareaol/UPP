﻿
using System;
using System.Runtime.Serialization;
using V82;
using V82.Перечисления;//Ссылка;
namespace V82.Перечисления//Ссылка
{
	///<summary>
	///(Упр)
	///</summary>
	[DataContract]
	public enum ИспытательныйСрок
	{
		[NonSerialized]
		ПустаяСсылка = - 1,
		[EnumMember(Value = "{\"Ссылка\":\"feca8e78-1b60-4476-8950-478d5cef52ab\", \"Представление\":\"СИспытательнымСроком\"}")]
		СИспытательнымСроком = 0,//С испытательным сроком
		[EnumMember(Value = "{\"Ссылка\":\"80e9568e-0569-48ed-a45a-c0edb76cffac\", \"Представление\":\"БезИспытательногоСрока\"}")]
		БезИспытательногоСрока = 1,//Без испытательного срока
	}
	public static partial class ИспытательныйСрок_Значения//:ПеречислениеСсылка
	{
		public static readonly Guid СИспытательнымСроком = new Guid("8d475089-ef5c-ab52-4476-1b60feca8e78");//С испытательным сроком
		public static readonly Guid БезИспытательногоСрока = new Guid("edc05aa4-6cb7-acff-48ed-056980e9568e");//Без испытательного срока
		public static ИспытательныйСрок Получить(this ИспытательныйСрок Значение, byte[] Ссылка)
		{
			return Получить(Значение, new Guid(Ссылка));
		}
		public static ИспытательныйСрок Получить(this ИспытательныйСрок Значение, Guid Ссылка)
		{
			if(Ссылка == СИспытательнымСроком)
			{
				return ИспытательныйСрок.СИспытательнымСроком;
			}
			else if(Ссылка == БезИспытательногоСрока)
			{
				return ИспытательныйСрок.БезИспытательногоСрока;
			}
			return ИспытательныйСрок.ПустаяСсылка;
		}
		public static byte[] Ключ(this ИспытательныйСрок Значение)
		{
			return Ссылка(Значение).ToByteArray();
		}
		public static Guid Ссылка(this ИспытательныйСрок Значение)
		{
			switch (Значение)
			{
				case ИспытательныйСрок.СИспытательнымСроком: return СИспытательнымСроком;
				case ИспытательныйСрок.БезИспытательногоСрока: return БезИспытательногоСрока;
			}
			return Guid.Empty;
		}
	}
}
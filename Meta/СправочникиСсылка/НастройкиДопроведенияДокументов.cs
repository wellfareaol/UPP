﻿
using System;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.Serialization;
using ProtoBuf;/*https://github.com/ServiceStack/ServiceStack/tree/master/lib*/
using ServiceStack.Text;/*https://github.com/ServiceStack/ServiceStack.Text*/
using V82;
using V82.ОбщиеОбъекты;
using V82.СправочникиСсылка;
using V82.ДокументыСсылка;
using V82.Перечисления;//Ссылка;
namespace V82.СправочникиСсылка
{
	[ProtoContract]
	[DataContract]
	public partial class НастройкиДопроведенияДокументов:СправочникСсылка,IСериализаторProtoBuf,IСериализаторJson
	{
		public static readonly Guid ГуидКласса = new Guid("416e5962-85e4-42f2-ae9b-a5208060487d");
		public static readonly DateTime ВерсияКласса = DateTime.ParseExact("20120928012032.000", new string[] {"yyyyMMddHHmmss.fff"}, CultureInfo.InvariantCulture, DateTimeStyles.None);
		public static readonly long КонтрольнаяСуммаКласса = 123;
		[DataMember]
		[ProtoMember(1)]
		public Guid Ссылка {get;set;}
		[DataMember]
		[ProtoMember(2)]
		public long Версия {get;set;}
		public string ВерсияДанных {get;set;}
		/*static хэш сумма состава и порядка реквизитов*/
		/*версия класса восстановленного из пакета*/
		public bool ПометкаУдаления {get;set;}
		public bool Предопределенный {get;set;}
		[DataMember(Name = "Представление")]//Проверить основное представление.
		[ProtoMember(3)]
		public string/*100*/ Наименование {get;set;}
		///<summary>
		///Запускать допроведение автоматически (регламентным заданием)
		///</summary>
		public bool ФормироватьДокументыАвтоматически {get;set;}//Запускать допроведение автоматически
		///<summary>
		///Уникальный идентификатор регламентного задания
		///</summary>
		public string/*(36)*/ РегламентноеЗадание {get;set;}//Регламентное задание
		public string/*(0)*/ Комментарий {get;set;}
		public bool ДопроводитьВсеДокументы {get;set;}//Допроводить все документы
		///<summary>
		///День месяца, заканчивая которым создается задача на допроведение документов
		///</summary>
		public decimal/*(2)*/ НомерДняКонецЗапуска {get;set;}//Номер дня конец запуска
		///<summary>
		///День месяца, начиная с которого создается задача на допроведение документов
		///</summary>
		public decimal/*(2)*/ НомерДняНачалоЗапуска {get;set;}//Номер дня начало запуска
		public DateTime НачалоИнтервалаДопроведения {get;set;}//Начало интервала допроведения
		public DateTime КонецИнтервалаДопроведения {get;set;}//Конец интервала допроведения
		///<summary>
		///Количество периодов, на которые отстают обрабатываемые документы относительно текущей даты
		///</summary>
		public decimal/*(2)*/ КоличествоПериодовОтставанияКонецИнтервала {get;set;}//Количество периодов отставания конец интервала
		///<summary>
		///Количество периодов, на которые отстают обрабатываемые документы относительно текущей даты
		///</summary>
		public decimal/*(2)*/ КоличествоПериодовОтставанияНачалоИнтервала {get;set;}//Количество периодов отставания начало интервала
		///<summary>
		///Период, на который отстают обрабатываемые документы по сравнению с текущим периодом
		///</summary>
		public V82.Перечисления/*Ссылка*/.Периодичность ПериодичностьОтставанияКонецИнтервала {get;set;}//Периодичность отставания конец интервала
		///<summary>
		///Период, на который отстают обрабатываемые документы по сравнению с текущим периодом
		///</summary>
		public V82.Перечисления/*Ссылка*/.Периодичность ПериодичностьОтставанияНачалоИнтервала {get;set;}//Периодичность отставания начало интервала
		public bool РассчитыватьКонецИнтервала {get;set;}//Рассчитывать конец интервала
		public bool РассчитыватьНачалоИнтервала {get;set;}//Рассчитывать начало интервала
		
		public НастройкиДопроведенияДокументов()
		{
		}
		
		public НастройкиДопроведенияДокументов(byte[] УникальныйИдентификатор)
			: this(УникальныйИдентификатор,0)
		{
		}
		
		public НастройкиДопроведенияДокументов(byte[] УникальныйИдентификатор,int Глубина)
		{
			if (Глубина>3)
			{
				return;
			}
			if (new Guid(УникальныйИдентификатор) == Guid.Empty)
			{
				return;
			}
			using (var Подключение = new SqlConnection(СтрокаСоединения))
			{
				Подключение.Open();
				using (var Команда = Подключение.CreateCommand())
				{
					Команда.CommandText = @"Select top 1 
					_IDRRef [Ссылка]
					,_Version [Версия]
					,_Marked [ПометкаУдаления]
					,_IsMetadata [Предопределенный]
					,_Description [Наименование]
					,_Fld23582 [ФормироватьДокументыАвтоматически]
					,_Fld23583 [РегламентноеЗадание]
					,_Fld23584 [Комментарий]
					,_Fld23585 [ДопроводитьВсеДокументы]
					,_Fld23586 [НомерДняКонецЗапуска]
					,_Fld23587 [НомерДняНачалоЗапуска]
					,_Fld23588 [НачалоИнтервалаДопроведения]
					,_Fld23589 [КонецИнтервалаДопроведения]
					,_Fld23590 [КоличествоПериодовОтставанияКонецИнтервала]
					,_Fld23591 [КоличествоПериодовОтставанияНачалоИнтервала]
					,_Fld23592RRef [ПериодичностьОтставанияКонецИнтервала]
					,_Fld23593RRef [ПериодичностьОтставанияНачалоИнтервала]
					,_Fld23594 [РассчитыватьКонецИнтервала]
					,_Fld23595 [РассчитыватьНачалоИнтервала]
					From _Reference23108(NOLOCK)
					Where _IDRRef=@УникальныйИдентификатор  ";
					Команда.Parameters.AddWithValue("УникальныйИдентификатор", УникальныйИдентификатор);
					using (var Читалка = Команда.ExecuteReader())
					{
						if (Читалка.Read())
						{
							//ToDo: Читать нужно через GetValues()
							Ссылка = new Guid((byte[])Читалка.GetValue(0));
							var ПотокВерсии = ((byte[])Читалка.GetValue(1));
							Array.Reverse(ПотокВерсии);
							Версия =  BitConverter.ToInt64(ПотокВерсии, 0);
							ВерсияДанных =  Convert.ToBase64String(ПотокВерсии);
							ПометкаУдаления = ((byte[])Читалка.GetValue(2))[0]==1;
							Предопределенный = ((byte[])Читалка.GetValue(3))[0]==1;
							Наименование = Читалка.GetString(4);
								ФормироватьДокументыАвтоматически = ((byte[])Читалка.GetValue(5))[0]==1;
								РегламентноеЗадание = Читалка.GetString(6);
								Комментарий = Читалка.GetString(7);
								ДопроводитьВсеДокументы = ((byte[])Читалка.GetValue(8))[0]==1;
								НомерДняКонецЗапуска = Читалка.GetDecimal(9);
								НомерДняНачалоЗапуска = Читалка.GetDecimal(10);
								НачалоИнтервалаДопроведения = Читалка.GetDateTime(11);
								КонецИнтервалаДопроведения = Читалка.GetDateTime(12);
								КоличествоПериодовОтставанияКонецИнтервала = Читалка.GetDecimal(13);
								КоличествоПериодовОтставанияНачалоИнтервала = Читалка.GetDecimal(14);
								ПериодичностьОтставанияКонецИнтервала = V82.Перечисления/*Ссылка*/.Периодичность.ПустаяСсылка.Получить((byte[])Читалка.GetValue(15));
								ПериодичностьОтставанияНачалоИнтервала = V82.Перечисления/*Ссылка*/.Периодичность.ПустаяСсылка.Получить((byte[])Читалка.GetValue(16));
								РассчитыватьКонецИнтервала = ((byte[])Читалка.GetValue(17))[0]==1;
								РассчитыватьНачалоИнтервала = ((byte[])Читалка.GetValue(18))[0]==1;
							//return Ссылка;
						}
						else
						{
							//return null;
						}
					}
				}
			}
		}
		
		public V82.СправочникиОбъект.НастройкиДопроведенияДокументов  ПолучитьОбъект()
		{
			var Объект = new V82.СправочникиОбъект.НастройкиДопроведенияДокументов();
			Объект._ЭтоНовый = false;
			Объект.Ссылка = Ссылка;
			Объект.Версия = Версия;
			Объект.ПометкаУдаления = ПометкаУдаления;
			Объект.Предопределенный = Предопределенный;
			Объект.Наименование = Наименование;
			Объект.ФормироватьДокументыАвтоматически = ФормироватьДокументыАвтоматически;
			Объект.РегламентноеЗадание = РегламентноеЗадание;
			Объект.Комментарий = Комментарий;
			Объект.ДопроводитьВсеДокументы = ДопроводитьВсеДокументы;
			Объект.НомерДняКонецЗапуска = НомерДняКонецЗапуска;
			Объект.НомерДняНачалоЗапуска = НомерДняНачалоЗапуска;
			Объект.НачалоИнтервалаДопроведения = НачалоИнтервалаДопроведения;
			Объект.КонецИнтервалаДопроведения = КонецИнтервалаДопроведения;
			Объект.КоличествоПериодовОтставанияКонецИнтервала = КоличествоПериодовОтставанияКонецИнтервала;
			Объект.КоличествоПериодовОтставанияНачалоИнтервала = КоличествоПериодовОтставанияНачалоИнтервала;
			Объект.ПериодичностьОтставанияКонецИнтервала = ПериодичностьОтставанияКонецИнтервала;
			Объект.ПериодичностьОтставанияНачалоИнтервала = ПериодичностьОтставанияНачалоИнтервала;
			Объект.РассчитыватьКонецИнтервала = РассчитыватьКонецИнтервала;
			Объект.РассчитыватьНачалоИнтервала = РассчитыватьНачалоИнтервала;
			return Объект;
		}
		
		private static readonly Hashtable Кэш = new Hashtable(1000);
		
		public static V82.СправочникиСсылка.НастройкиДопроведенияДокументов ВзятьИзКэша(byte[] УникальныйИдентификатор)
		{
			var УИ = new Guid(УникальныйИдентификатор);
			if (Кэш.ContainsKey(УИ))
			{
				return (V82.СправочникиСсылка.НастройкиДопроведенияДокументов)Кэш[УИ];
			}
			var Ссылка = new V82.СправочникиСсылка.НастройкиДопроведенияДокументов(УникальныйИдентификатор);
			Кэш.Add(УИ, Ссылка);
			return Ссылка;
		}
		
		public void СериализацияProtoBuf(Stream Поток)
		{
			Serializer.Serialize(Поток,this);
		}
		
		public string СериализацияJson()
		{
			return this.ToJson();
		}
		
		public string СериализацияXml()
		{
			return this.ToXml();
		}
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YS.Admin.Util.Extension;

namespace YS.Admin.Util
{
	#region JsonHelper
	public static class JsonHelper
	{
		public static T ToObject<T>(this string Json)
		{
			Json = Json.Replace("&nbsp;", "");
			return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
		}

		public static JObject ToJObject(this string Json)
		{
			return Json == null ? JObject.Parse("{}") : JObject.Parse(Json.Replace("&nbsp;", ""));
		}



		/// <summary> 
		/// 生成Json格式 
		/// </summary> 
		/// <typeparam name="T"></typeparam> 
		/// <param name="obj"></param> 
		/// <returns></returns> 
		public static string GetJson<T>(T obj)
		{
			DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
			using (MemoryStream stream = new MemoryStream())
			{
				json.WriteObject(stream, obj);
				string szJson = Encoding.UTF8.GetString(stream.ToArray()); return szJson;
			}
		}
		/// <summary> 
		/// 获取Json的Model 
		/// </summary> 
		/// <typeparam name="T"></typeparam> 
		/// <param name="szJson"></param> 
		/// <returns></returns> 
		public static T ParseFromJson<T>(string szJson)
		{
			T obj = Activator.CreateInstance<T>();
			using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
				return (T)serializer.ReadObject(ms);
			}
		}
	}


	#endregion

	#region JsonConverter
	/// <summary>
	/// Json数据返回到前端js的时候，把数值很大的long类型转成字符串
	/// </summary>
	public class StringJsonConverter : JsonConverter
	{
		public StringJsonConverter() { }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return reader.Value.ParseToLong();
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			string sValue = value.ToString();
			writer.WriteValue(sValue);
		}
	}

	/// <summary>
	/// DateTime类型序列化的时候，转成指定的格式
	/// </summary>
	public class DateTimeJsonConverter : JsonConverter
	{
		public DateTimeJsonConverter() { }

		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return reader.Value.ToString().ParseToDateTime();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			DateTime? dt = value as DateTime?;
			if (dt == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteValue(dt.Value.ToString("yyyy-MM-dd HH:mm:ss"));
		}
	}
	#endregion
}

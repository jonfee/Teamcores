using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace TeamCores.Common.Utilities
{
    /// <summary>
    /// 为 <see cref="System.Enum"/> 类型扩展的辅助类。
    /// </summary>
    public static class EnumUtility
    {
        public static string Format(object value, string format)
        {
            if (value == null)
                return string.Empty;

            var enumType = GetEnumType(value.GetType());

            if (enumType != null)
                return GetEnumEntry((Enum)value).ToString(format);

            if (string.IsNullOrWhiteSpace(format))
                return string.Format("{0}", value);
            else
                return string.Format("{0:" + format + "}", value);
        }

        public static Type GetEnumType(Type type)
        {
            if (type == null)
                return null;

            if (type.GetTypeInfo().IsEnum)
                return type;

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments()[0].GetTypeInfo().IsEnum)
                return type.GetGenericArguments()[0];

            return null;
        }

        /// <summary>
        /// 获取指定枚举项对应的 <see cref="EnumEntry"/> 描述对象。
        /// </summary>
        /// <param name="enumValue">要获取的枚举项。</param>
        /// <returns>返回指定枚举值对应的<seealso cref="EnumEntry"/>对象。</returns>
        public static EnumEntry GetEnumEntry(this Enum enumValue)
        {
            return GetEnumEntry(enumValue, false);
        }

        public static EnumEntry GetEnumEntry<T>(this T enumValue)
        {
            return GetEnumEntry(enumValue as Enum, false);
        }

        /// <summary>
        /// 获取指定枚举项对应的 <see cref="EnumEntry"/> 描述对象。 
        /// </summary>
        /// <param name="enumValue">要获取的枚举项。</param>
        /// <param name="underlyingType">是否将生成的 <seealso cref="EnumEntry"/> 元素的 <seealso cref="EnumEntry.Value"/> 属性值置为 enumType 参数对应的枚举项基类型值。</param>
        /// <returns>返回指定枚举值对应的 <seealso cref="EnumEntry"/> 对象。</returns>
        public static EnumEntry GetEnumEntry(this Enum enumValue, bool underlyingType)
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());

            var description = field.GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>().FirstOrDefault().Description;

            var value = underlyingType ? System.Convert.ChangeType(field.GetValue(null), Enum.GetUnderlyingType(enumValue.GetType())) : field.GetValue(null);

            return new EnumEntry(enumValue.GetType(), field.Name, value, description);
        }

        /// <summary>
        /// 获取指定枚举的描述对象数组。
        /// </summary>
        /// <param name="enumType">要获取的枚举类型。</param>
        /// <param name="underlyingType">是否将生成的 <seealso cref="EnumEntry"/> 元素的 <seealso cref="EnumEntry.Value"/> 属性值置为 enumType 参数对应的枚举项基类型值。</param>
        /// <returns>返回的枚举描述对象数组。</returns>
        public static EnumEntry[] GetEnumEntries(Type enumType, bool underlyingType)
        {
            return GetEnumEntries(enumType, underlyingType, null, string.Empty);
        }

        /// <summary>
        /// 获取指定枚举的描述对象数组。
        /// </summary>
        /// <param name="enumType">要获取的枚举类型，可为<seealso cref="System.Nullable"/>类型。</param>
        /// <param name="underlyingType">是否将生成的 <seealso cref="EnumEntry"/> 元素的 <seealso cref="EnumEntry.Value"/> 属性值置为 enumType 参数对应的枚举项基类型值。</param>
        /// <param name="nullValue">如果参数<paramref name="enumType"/>为可空类型时，该空值对应的<seealso cref="EnumEntry.Value"/>属性的值。</param>
        /// <returns>返回的枚举描述对象数组。</returns>
        public static EnumEntry[] GetEnumEntries(Type enumType, bool underlyingType, object nullValue)
        {
            return GetEnumEntries(enumType, underlyingType, nullValue, string.Empty);
        }

        /// <summary>
        /// 获取指定枚举的描述对象数组。
        /// </summary>
        /// <param name="enumType">要获取的枚举类型，可为<seealso cref="System.Nullable"/>类型。</param>
        /// <param name="underlyingType">是否将生成的 <seealso cref="EnumEntry"/> 元素的 <seealso cref="EnumEntry.Value"/> 属性值置为 enumType 参数对应的枚举项基类型值。</param>
        /// <param name="nullValue">如果参数<paramref name="enumType"/>为可空类型时，该空值对应的<seealso cref="EnumEntry.Value"/>属性的值。</param>
        /// <param name="nullText">如果参数<paramref name="enumType"/>为可空类型时，该空值对应的<seealso cref="EnumEntry.Description"/>属性的值。</param>
        /// <returns>返回的枚举描述对象数组。</returns>
        public static EnumEntry[] GetEnumEntries(Type enumType, bool underlyingType, object nullValue, string nullText)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));

            Type underlyingTypeOfNullable = Nullable.GetUnderlyingType(enumType);
            if (underlyingTypeOfNullable != null)
                enumType = underlyingTypeOfNullable;

            EnumEntry[] entries;
            int baseIndex = (underlyingTypeOfNullable == null) ? 0 : 1;
            var fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

            if (underlyingTypeOfNullable == null)
            {
                entries = new EnumEntry[fields.Length];
            }
            else
            {
                entries = new EnumEntry[fields.Length + 1];
                entries[0] = new EnumEntry(enumType, string.Empty, nullValue, nullText);
            }

            for (int i = 0; i < fields.Length; i++)
            {
                var description = fields[i].GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>().FirstOrDefault().Description;

                var value = underlyingType ? System.Convert.ChangeType(fields[i].GetValue(null), Enum.GetUnderlyingType(enumType)) : fields[i].GetValue(null);

                entries[baseIndex + i] = new EnumEntry(enumType, fields[i].Name, value, description);
            }

            return entries;
        }

        /// <summary>
        /// 根据枚举成员的数值或名称获取描述信息
        /// </summary>
        /// <typeparam name="T">要获取的枚举类型</typeparam>
        /// <param name="valueOrName">枚举成员的数值或名称</param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(object valueOrName) where T : struct, IConvertible
        {
            if (valueOrName == null) return string.Empty;

            var type = typeof(T);

            var entries = GetEnumEntries(type, true);

            var entry = entries.FirstOrDefault(p => p.Value == valueOrName || string.Compare(p.Name, valueOrName.ToString(), true) == 0);

            return entry.Description;
        }

        /// <summary>
        /// 获取枚举成员基类型值与描述的集合
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>返回枚举成员基类型值与描述的字典集合</returns> 
        public static Dictionary<int, string> GetEnumDescriptions(Type enumType)
        {
            if (!enumType.GetTypeInfo().IsEnum)
                throw new ArgumentException(string.Format("{0} is not a valid Enum", nameof(enumType)));

            var entries = GetEnumEntries(enumType, true);

            return entries.ToDictionary(k =>Convert.ToInt32(k.Value), v => v.Description);
        }

        /// <summary>
        /// 获取枚举成员的值集合
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static IEnumerable<int> GetEnumValues(Type enumType)
        {
            var values = GetEnumDescriptions(enumType).Keys;

            return values;
        }

        /// <summary>
        /// 检测值中是否包含指定枚举成员（值）
        /// <para>一般用于以2的N次方定义的枚举中,当对象的属性成员包含多个枚举成员属性时使用</para>
        /// </summary>
        /// <param name="values">对象的属性，以同一个<seealso cref="Enum"/>枚举中的一个或多个成员值的和组成 </param>
        /// <param name="enumItem">要检测的<seealso cref="Enum"/>枚举中的成员是否存在</param>
        /// <returns></returns>
        public static bool ContainsEnumItem(int values, Enum enumItem)
        {
            int itemValue = Convert.ToInt32(enumItem);

            return (values & itemValue) == itemValue;
        }
    }
}
using Negohelp.Comun.Models;
using System.Data;
using System.Reflection;


namespace Negohelp.Comun.DataAccess
{
	public static class Mapeador
	{
		private static readonly HashSet<Type> definedDataTypes = new HashSet<Type>
		{
			typeof(TipoCliente),
			typeof(TipoIdentificacion)
		};
		public static List<T> DataTableToList<T>(this DataTable dtDatos) where T : class, new()
		{
			try
			{

				var list = new List<T>(dtDatos.Rows.Count);

				foreach (DataRow drFila in dtDatos.Rows)
				{
					var objeto = MappingColumns<T>(drFila);

					list.Add(objeto);
				}

				return list;
			}
			catch
			{
				return null;
			}
		}

		public static T DataRowToObject<T>(this DataRow drDatos) where T : class, new()
		{
			try
			{
				return MappingColumns<T>(drDatos);
			}
			catch
			{
				return null;
			}
		}
		private static T MappingColumns<T>(DataRow drDatos) where T : class, new()
		{
			var objeto = new T();
			var objType = typeof(T);

			ICollection<PropertyInfo> lstPropiedades;

			lstPropiedades = objType.GetProperties().Where(property => property.CanWrite).ToList();
			//DataRow drDatos = dtDatos.Rows.Count > 0 ? dtDatos.Rows[0] : null;

			foreach (var propiedad in lstPropiedades)
			{
				try
				{
					var propType = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
					object? safeValue;

					if (definedDataTypes.Contains(propType))
					{
						var enumValue = drDatos[propiedad.Name] == DBNull.Value ? -1 : Convert.ToInt32(drDatos[propiedad.Name]);
						safeValue = Enum.ToObject(propType, enumValue);
					}
					else
					{
						safeValue = drDatos[propiedad.Name] == DBNull.Value ? null : Convert.ChangeType(drDatos[propiedad.Name], propType);
						safeValue = propType == typeof(string) ? Convert.ToString(safeValue)?.Trim() : safeValue;
					}

					propiedad.SetValue(objeto, safeValue, null);
				}
				catch
				{
					// ignored for a while
				}
			}
			return objeto;
		}
	}
}

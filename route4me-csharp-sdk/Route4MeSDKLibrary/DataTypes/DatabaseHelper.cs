﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Route4MeSDK.DataTypes
{
    public static class DatabaseHelper
    {
        /// <summary>
        ///     Indicates whether a specified DataTable is null, has zero columns, or (optionally) zero rows.
        /// </summary>
        /// <param name="Table">DataTable to check.</param>
        /// <param name="IgnoreRows">
        ///     When set to true, the function will return true even if the table's row count is equal to
        ///     zero.
        /// </param>
        /// <returns>False if the specified DataTable null, has zero columns, or zero rows, otherwise true.</returns>
        public static bool IsValidDatatable(DataTable Table, bool IgnoreRows = false)
        {
            if (Table == null) return false;
            if (Table.Columns.Count == 0) return false;
            if (!IgnoreRows && Table.Rows.Count == 0) return false;
            return true;
        }

        /// <summary>
        ///     Indicates whether a specified Enumerable collection is null or an empty collection.
        /// </summary>
        /// <typeparam name="T">The specified type contained in the collection.</typeparam>
        /// <param name="Input">An Enumerator to the collection to check.</param>
        /// <returns>True if the specified Enumerable collection is null or empty, otherwise false.</returns>
        public static bool IsCollectionEmpty<T>(IEnumerable<T> Input)
        {
            return Input == null || Input.Count() < 1 ? true : false;
        }

        /// <summary>
        ///     Indicates whether a specified Type can be assigned null.
        /// </summary>
        /// <param name="Input">The Type to check for nullable property.</param>
        /// <returns>True if the specified Type can be assigned null, otherwise false.</returns>
        public static bool IsNullableType(Type Input)
        {
            if (!Input.IsValueType) return true; // Reference Type
            if (Nullable.GetUnderlyingType(Input) != null) return true; // Nullable<T>
            return false; // Value Type
        }

        /// <summary>
        ///     Returns all the column names of the specified DataRow in a string delimited like and SQL INSERT INTO statement.
        ///     Example: ([FullName], [Gender], [BirthDate])
        /// </summary>
        /// <returns>A string formatted like the columns specified in an SQL 'INSERT INTO' statement.</returns>
        public static string RowToColumnString(DataRow Row)
        {
            var Collection = Row.ItemArray.Select(item => item as string);
            return ListToDelimitedString(Collection, "([", "], [", "])");
        }

        /// <summary>
        ///     Returns all the values the specified DataRow in as a string delimited like and SQL INSERT INTO statement.
        ///     Example: ('John Doe', 'M', '10/3/1981'')
        /// </summary>
        /// <returns>A string formatted like the values specified in an SQL 'INSERT INTO' statement.</returns>
        public static string RowToValueString(DataRow Row)
        {
            var Collection = GetDatatableColumns(Row.Table).Select(c => c.ColumnName);
            return ListToDelimitedString(Collection, "('", "', '", "')");
        }

        /// <summary>
        ///     Enumerates a collection as delimited collection of strings.
        /// </summary>
        /// <typeparam name="T">The Type of the collection.</typeparam>
        /// <param name="Collection">An Enumerator to a collection to populate the string.</param>
        /// <param name="Prefix">The string to prefix the result.</param>
        /// <param name="Delimiter">The string that will appear between each item in the specified collection.</param>
        /// <param name="Postfix">The string to postfix the result.</param>
        public static string ListToDelimitedString<T>(IEnumerable<T> Collection, string Prefix, string Delimiter,
            string Postfix)
        {
            if (IsCollectionEmpty(Collection)) return string.Empty;

            var result = new StringBuilder();
            foreach (var item in Collection)
            {
                if (result.Length != 0)
                    result.Append(Delimiter); // Add comma

                result.Append(EscapeSingleQuotes(item as string));
            }

            if (result.Length < 1) return string.Empty;

            result.Insert(0, Prefix);
            result.Append(Postfix);

            return result.ToString();
        }

        /// <summary>
        ///     Returns an enumerator, which supports a simple iteration over a collection of all the DataColumns in a specified
        ///     DataTable.
        /// </summary>
        public static IEnumerable<DataColumn> GetDatatableColumns(DataTable Input)
        {
            if (Input == null || Input.Columns.Count < 1) return new List<DataColumn>();
            return Input.Columns.OfType<DataColumn>().ToList();
        }

        /// <summary>
        ///     Returns an enumerator, which supports a simple iteration over a collection of all the DataRows in a specified
        ///     DataTable.
        /// </summary>
        public static IEnumerable<DataRow> GetDatatableRows(DataTable Input)
        {
            if (!IsValidDatatable(Input)) return new List<DataRow>();
            return Input.Rows.OfType<DataRow>().ToList();
        }

        /// <summary>
        ///     Returns a new string in which all occurrences of the single quote character in the current instance are replaced
        ///     with a back-tick character.
        /// </summary>
        public static string EscapeSingleQuotes(string Input)
        {
            return Input.Replace('\'', '`'); // Replace with back-tick
        }
    }
}
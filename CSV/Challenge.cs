using System;
using System.Collections.Generic;
using System.Linq;


namespace CSV
{

    public class Challenge
    {
        public static string SortCsvColumns(string csv_data)
        {
            var table = Table.CreateFromCsv(csv_data);
            return table.AsCsvByOrderedColumns();
        }
    }

    //Represents a Column of the data as a table
    public class Column
    {
        public Column(string name)
        {
            Name = name;
            Values = new List<string>();
        }

        public string Name { get; }
        public IList<string> Values { get; }

        public void AddValue(string value)
        {
            Values.Add(value);
        }

        public string GetRowValue(int rowNumber)
        {
            try
            {
                return Values[rowNumber];
            }
            catch (Exception)
            {
                return "";
            }
        }
    }


    //Represents the received data as a table
    public class Table
    {
        public Table()
        {
            Columns = new List<Column>();
        }


        public IList<Column> Columns { get; }

        public void CreateColumns(string[] names)
        {
            foreach (var columnName in names)
            {
                AddColumn(new Column(columnName));
            }
        }

        public void AddColumn(Column column)
        {
            Columns.Add(column);
        }

        public Column GetColumnByName(string columnName)
        {
            return Columns.Where(c => c.Name.Equals(columnName)).FirstOrDefault();
        }

        public void AddValueToColumn(string columnName, string value)
        {
            var column = GetColumnByName(columnName);
            if (column != null)
                column.AddValue(value);
        }

        public int GetRowsCount()
        {
            return Columns.Select(c => c.Values.Count).ToList().Max();
        }

        public IList<Column> GetColumnsOrderedByName()
        {
            return Columns.OrderBy(c => c.Name).ToList();
        }

        public string AsCsvByOrderedColumns()
        {
            string result = "";
            var orderedColumns = GetColumnsOrderedByName();
            var rowsCount = GetRowsCount();

            result = string.Join(",", orderedColumns.Select(c => c.Name).ToList());
            result += "\n";

            for (int i = 1; i <= rowsCount; i++)
            {
                result += string.Join(",", orderedColumns.Select(c => c.GetRowValue(i - 1)).ToList());
                if (i < rowsCount)
                {
                    result += "\n";
                }
            }

            return result;
        }

        //A static method to create and populate the object from a CSV
        public static Table CreateFromCsv(string data)
        {
            var table = new Table();

            string[] dataAsArray = data.Split("\n");
            string[] columnNames = dataAsArray[0].Split(",");
            table.CreateColumns(columnNames);

            //fill data
            //iterating rows
            for (int i = 1; i <= dataAsArray.Length - 1; i++)
            {
                string[] values = dataAsArray[i].Split(",");

                //iterating cols
                for (int j = 0; j <= values.Length - 1; j++)
                {
                    string columnName = columnNames[j];
                    string value = values[j];
                    table.AddValueToColumn(columnName, value);
                }
            }

            return table;
        }
    }
}

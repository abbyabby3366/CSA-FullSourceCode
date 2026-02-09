using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace csa.Library
{
    public class SQLHelpers
    {
        public static string Concat(string field1, string field2, string separator = "' '")
        {
            return $"concat(IFNULL({field1},''),{separator},IFNULL({field2},''))";
        }
    }

    public class SQLJoinRelation
    {
        public SQLJoinRelation(string tableName, string _as, string _relationToAs)
        {
            TableName = tableName;
            As = _as;
            RelationToAs = _relationToAs;
        }
        public string As { get; set; }
        public string TableName { get; set; }
        public string RelationToAs { get; set; }
    }

    public class SQLSelect
    {
        public SQLSelect(string from)
        {
            From = from;
        }

        public enum OrderByEnum
        {
            ASC,
            DESC
        }

        private static T GetValueFromList<T>(List<T> values, int index)
        {
            if (index > values.Count() - 1)
                return default(T);

            return values[index];
        }

        //untuk method SQLCount agar join yg tidak dipake dibuang
        public void RemoveJoin()
        {
            List<SQLJoinRelation> relations = new List<SQLJoinRelation>();
            foreach (var w in Joins.AsEnumerable().Reverse())
            {
                string input = w.ToLower();

                Regex rx = new Regex(@"join(.*)on");
                var tableName = rx.Match(input).Groups[1].Value.Trim();
                var source = tableName.Split(' ').ToList();
                var _as = GetValueFromList(source, 1);
                if (string.IsNullOrEmpty(_as?.Trim()))
                    _as = GetValueFromList(source, 0);

                rx = new Regex(@"on(.*)=");
                var table1 = rx.Match(input).Groups[1].Value.Trim().Split('.')[0];

                rx = new Regex(@"=(.*)");
                var table2 = rx.Match(input).Groups[1].Value.Trim().Split('.')[0];

                var _relationToAs = table1;
                if (_as == table1)
                    _relationToAs = table2;

                relations.Add(new SQLJoinRelation(tableName, _as, _relationToAs));
            }

            var whereText = $" {string.Join(" ", Wheres)}";

            var tempRelation = relations.ToList();
            foreach (var r in tempRelation)
            {
                if (!Regex.IsMatch(whereText, $@"( {r.As}|,{r.As}|\({r.As})\."))
                {
                    if (relations.FirstOrDefault(x => x.RelationToAs == r.As) == null)
                    {
                        Joins.Remove(Joins.FirstOrDefault(new Regex($"(?i)(JOIN {r.TableName} ON)").IsMatch));
                        relations.Remove(r);
                    }
                }

            }
        }

        public string SQLCount(bool removeJoin = false)
        {
            var newSql = new SQLSelect(this.From);
            newSql.Selects.Add("count(*)");
            newSql.Wheres = this.Wheres;
            newSql.Joins = this.Joins;
            newSql.GroupBY = this.GroupBY;

            if (removeJoin)
                newSql.RemoveJoin();

            var sql = newSql.Result;
            if (!string.IsNullOrEmpty(this.GroupBY?.Trim()))
            {
                sql = $"SELECT COUNT(*) FROM ({sql.Remove(sql.Length - 1, 1)}) as d";
            }

            return sql;
        }

        private List<string> Selects { get; set; } = new List<string>();
        private List<string> Wheres { get; set; } = new List<string>();
        private List<string> Joins { get; set; } = new List<string>();
        private string From { get; set; }
        private string OrderBy { get; set; } = "";
        private string Limit { get; set; } = "";
        private string GroupBY { get; set; } = "";
        public void AddSelect(string select)
        {
            Selects.Add(select);
        }
        public void AddWhere(string where)
        {
            Wheres.Add(where);
        }
        public void AddLeftJoin(string table, string val1, string val2)
        {
            Joins.Add($"LEFT JOIN {table} ON {val1} = {val2}");
        }
        public void AddJoin(string table, string val1, string val2)
        {
            Joins.Add($"JOIN {table} ON {val1} = {val2}");
        }
        public void SetGroupBY(string groupBy)
        {
            GroupBY = $"GROUP BY {groupBy}";
        }
        public void SetOrderBY(string name, OrderByEnum by)
        {
            OrderBy = $"ORDER BY {name} {by}";
        }

        public void SetLimit(int start, int length)
        {
            Limit = $"LIMIT {start},{length}";
        }
        public string Result
        {
            get
            {
                if (Selects.Count == 0)
                    Selects.Add("*");

                StringBuilder sb = new StringBuilder();
                sb.Append($" SELECT {string.Join(",", Selects)} FROM {From} ");

                if (Joins.Count > 0)
                {
                    sb.Append($" {string.Join(" ", Joins)} ");
                }

                if (Wheres.Count > 0)
                {
                    sb.Append($" WHERE {string.Join(" AND ", Wheres)} ");
                }

                sb.Append($" {GroupBY} ");

                sb.Append($" {OrderBy} ");

                sb.Append($" {Limit} ");

                //sb.Append(";");

                return sb.ToString();
            }
        }

        public string IFNULL(string field, string defaultValue = "")
        {
            return $"IFNULL({field},'{defaultValue}')";
        }

    }
}

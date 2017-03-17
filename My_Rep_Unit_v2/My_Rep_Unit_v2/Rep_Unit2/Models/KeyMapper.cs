using Rep_Unit2.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Rep_Unit2.Models
{
    public class KeyMapper
    {
        SchoolContext context;
        public KeyMapper(SchoolContext tcontext)
        {
            context = tcontext;
        }
        public int getKEYForTable(String tableName)
        {
            var idParam = new SqlParameter {      ParameterName = "tableName",
                                                  Value = tableName
            };
            int result = context.Database.ExecuteSqlCommand("Next_KEY @tableName", idParam);
            return result;
        }

      
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertSQLGenerator
{
    class Program
    {
        //tables
        public const string DEPARTMENT = "DEPARTMENT";
        public const string PROJECT = "PROJECTT";
        public const string WORKSON = "WORKSON";
        public const string EMPLOYEE = "EMPLOYEE";
        public const string QUOTE = "\"";
        //foreign keys
        public List<string> emplNo;
        public List<string> deptNo;
        public List<string> projNo;

        public StreamWriter sw;
        public Random r;

        public Program(string file)
        {
            sw = File.CreateText(file);
            this.emplNo = new List<string>();
            this.deptNo = new List<string>();
            this.projNo = new List<string>();
            this.r = new Random();
        }

        static void Main(string[] args)
        {
            string output = @"C:\Users\prestamo\Documents\GitHub\Insert_Generator_SQL\input\INSERTS.sql";
            string deptFile = @"C:\Users\prestamo\Documents\GitHub\Insert_Generator_SQL\input\DEPARTMENT_DATA.csv";
            string empFile = @"C:\Users\prestamo\Documents\GitHub\Insert_Generator_SQL\input\EMPLOYEE_DATA.csv";
            string projFile = @"C:\Users\prestamo\Documents\GitHub\Insert_Generator_SQL\input\PROJECTT_DATA.csv";
            string workFile = @"C:\Users\prestamo\Documents\GitHub\Insert_Generator_SQL\input\WORKSON_DATA.csv";
            Program program = new Program(output);
            program.loadData(deptFile);
            program.loadData(empFile);
            program.loadData(projFile);
            program.loadData(workFile);
            program.sw.Close();


        }

        public void loadData(String path)
        {
            string table = EMPLOYEE;

            if (path.Contains(DEPARTMENT))
            {
                table = DEPARTMENT;
            }else if (path.Contains(PROJECT))
            {
                table = PROJECT;
            }else if (path.Contains(WORKSON))
            {
                table = WORKSON;
            }
            
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] fields = s.Split(',', ';');

                    

                    if (!table.Equals(WORKSON))
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            fields[i] = QUOTE + fields[i] + QUOTE;                          
                        }
                        generateInsert(table, fields);
                    }
                    else
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (i != fields.Length - 1)
                            {
                                fields[i] = QUOTE + fields[i] + QUOTE;
                            }                      
                        }
                        generateInsert(table, fields);
                    }
                    //Foreign keys storage
                    if (table.Equals(DEPARTMENT))
                    {
                        deptNo.Add(fields[0]);
                    }else if (table.Equals(EMPLOYEE))
                    {
                        emplNo.Add(fields[0]);
                    }else if (table.Equals(PROJECT))
                    {
                        projNo.Add(fields[0]);
                    }
                }
            }
        }

        public void generateInsert(string table,string[]fields)
        {
            
            string values ="";
            for(int i = 0; i < fields.Length; i++)
            {
                if (i == fields.Length - 1)
                {
                    values += fields[i];
                }
                else
                {
                    values += fields[i] + ",";
                }
            }
            if (table.Equals(EMPLOYEE))
            {   
                int n = r.Next(0,deptNo.Count() - 1);      
                values += "," + deptNo[n];
                
            }else if (table.Equals(PROJECT)) {
                int n = deptNo.Count() - 1;
                values += "," + deptNo[r.Next(0, n)];
            }else if (table.Equals(WORKSON))
            {            
                string temp = values;
                int n = emplNo.Count() - 1;
                int j = projNo.Count() - 1;
                string foreigns = emplNo[r.Next(0, n)] + "," + projNo[r.Next(0, j)] + ",";
                values = foreigns + temp;              
            }
            string line ="INSERT INTO " + table + " VALUES("+values+");";
            sw.WriteLine(line);
        }
    }
}

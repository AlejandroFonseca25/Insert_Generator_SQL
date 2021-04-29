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
        public const string QUOTE = "\'";
        //foreign keys
        public List<string> emplNo;
        public List<string> deptNo;
        public List<string> projNo;

        public StreamWriter sw;
        public Random r;
        private int count;

        private string[] genders = { QUOTE + "M" + QUOTE, QUOTE + "F" + QUOTE };
        private string[] names = { 
            QUOTE+"David"+QUOTE, QUOTE + "Alejandro" + QUOTE, QUOTE + "Marco" + QUOTE, QUOTE + "Diego" + QUOTE, 
            QUOTE + "Felipe" + QUOTE,QUOTE+"Marcela"+QUOTE,QUOTE+"Valentina"+QUOTE,QUOTE+"Sara"+QUOTE,QUOTE+"Marta"+QUOTE,
            QUOTE+"Karen"+QUOTE };
        private string[] lastNames = {
            QUOTE+"Garcia"+QUOTE, QUOTE + "Rodriguez" + QUOTE, QUOTE + "Perez" + QUOTE, QUOTE + "Fonseca" + QUOTE,
            QUOTE + "Vasquez" + QUOTE,QUOTE+"Montoya"+QUOTE,QUOTE+"Villa"+QUOTE,QUOTE+"Marquez"+QUOTE,QUOTE+"Cardona"+QUOTE,
            QUOTE+"Torres"+QUOTE };
        private string[] deptNames = {
            QUOTE+"Ventas"+QUOTE, QUOTE + "Investigacion" + QUOTE, QUOTE + "Mantenimiento" + QUOTE, QUOTE + "Desarrollo" + QUOTE,
            QUOTE + "Produccion" + QUOTE,QUOTE+"Recursos Humanos"+QUOTE,QUOTE+"Contaduria"+QUOTE,QUOTE+"Leyes"+QUOTE,QUOTE+"Apoyo"+QUOTE,
            QUOTE+"Ingenieria"+QUOTE };
        private string[] empPosition = {
            QUOTE+"Asesor de ventas"+QUOTE, QUOTE + "Investigador" + QUOTE, QUOTE + "Ingeniero" + QUOTE, QUOTE + "Analista" + QUOTE,
            QUOTE + "Contador" + QUOTE,QUOTE+"Asistente"+QUOTE,QUOTE+"Diseñador" };
        private string[] projName = {
            QUOTE+"Bygram"+QUOTE, QUOTE + "Tolvex" + QUOTE, QUOTE + "Riesco" + QUOTE, QUOTE + "Wasxion" + QUOTE,
            QUOTE + "CardFill" + QUOTE,QUOTE+"Losdern"+QUOTE,QUOTE+"Opela"+QUOTE,QUOTE+"Tampflex"+QUOTE,QUOTE+"Volcox"+QUOTE,
            QUOTE+"Optomus"+QUOTE };
        private string[] streets = {"Simon Bol", "Ave. 5ta ", "Cll Porvenir", "Ave. 6ta", 
            "Ave. Pasoancho ", "Belalcazar ", "Ave. Panamericana", "Cra. 37", "Ave. Paraiso",
            "ShakedownRoad"};

       
        public Program(string file)
        {
            sw = File.CreateText(file);
            this.emplNo = new List<string>();
            this.deptNo = new List<string>();
            this.projNo = new List<string>();
            this.r = new Random();
            this.count = 1;
        }

        static void Main(string[] args)
        {
            //Poner el path completo de la carpeta output del repositorio
            string output = @"C:\Users\prestamo\Documents\GitHub\Insert_Generator_SQL\output\INSERTS.sql";
            Program program = new Program(output);
            program.loadData(DEPARTMENT);
            program.loadData(EMPLOYEE);
            program.loadData(PROJECT);
            program.loadData(WORKSON);
            program.sw.Close();


        }

        public void loadData(String table)
        {          
            string values = "";
            switch (table)
            {
                case DEPARTMENT:
                    for(int i = 0; i < 20; i++)
                    {
                        values = getDeptCode() +","+ getDeptName();
                        generateInsert(table, values);
                    }
                    break;
                case PROJECT:
                    for (int i = 0; i < 20; i++)
                    {
                        values = getProjectCode() + "," + getProjName() + "," + getDeptCodeFromList();
                        generateInsert(table, values);
                    }
                    break;
                case EMPLOYEE:
                    for (int i = 0; i < 20; i++)
                    {
                        values = getEmployeeCode() + "," + getName() + "," + getLastname() + "," + getAdress() + ","
                                + getDOB() + "," + getGender() + "," + getEmpPosition() + "," + getDeptCodeFromList();
                        generateInsert(table, values);
                    }
                    break;
                case WORKSON:
                    for (int i = 0; i < 20; i++)
                    {
                        values = getEmpCodeFromList() + "," + getProjCodeFromList() + "," + getDateWorked() + "," + getWorkedHours();
                        generateInsert(table, values);
                    }
                    break;
            }
            
        }

        public void generateInsert(string table,string values)
        {
            string line ="INSERT INTO " + table + " VALUES("+values+");";
            sw.WriteLine(line);
        }
        public string getName()
        {
            return names[r.Next(0,names.Length-1)];
        }

        public string getLastname()
        {
            return lastNames[r.Next(0, lastNames.Length - 1)];
        }

        public string getDeptCode()
        {
            string code;
            if (count < 10)
            {
                code = QUOTE + "DP" + "0" + count + QUOTE;
            }
            else
            {
                code = QUOTE + "DP" + count + QUOTE;    
            }
            deptNo.Add(code);
            count++;
            return code;
        }

        public string getDeptCodeFromList()
        {   
            return deptNo[r.Next(0,deptNo.Count-1)];
        }
        public string getEmpCodeFromList()
        {
            return emplNo[r.Next(0, emplNo.Count - 1)];
        }
        public string getProjCodeFromList()
        {
            return projNo[r.Next(0, projNo.Count - 1)];
        }
        public string getProjectCode()
        {
            string code = QUOTE + "PJ" + count + QUOTE;
            projNo.Add(code);
            count++;
            return code;
        }
        public string getEmployeeCode()
        {
            string code = QUOTE + "EM" + count + QUOTE;
            emplNo.Add(code);
            count++;
            return code;
        }

        public string getAdress()
        {
            string adress = QUOTE + streets[r.Next(0, streets.Length - 1)] + " " + r.Next(0, 100) + "-" + r.Next(0, 300) + QUOTE;
            return adress;
        }
        public string getDOB()
        {
            DateTime start = new DateTime(1970, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime final = start.AddDays(r.Next(range));
            return QUOTE + final.ToString("dd-MM-yyyy") + QUOTE;
        }

        public string getDeptName()
        {
            return deptNames[r.Next(0, deptNames.Length - 1)];
        }
        public string getProjName()
        {
            return projName[r.Next(0, projName.Length - 1)];
        }

        public string getEmpPosition()
        {
            return empPosition[r.Next(0, empPosition.Length - 1)];
        }

        public string getGender()
        {
            return genders[r.Next(0, 2)];
        }
        public string getWorkedHours()
        {
            int h = r.Next(0, 20);
            return QUOTE + h + QUOTE;
        }
        public string getDateWorked()
        {
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime final = start.AddDays(r.Next(range));
            return QUOTE + final.ToString("dd-MM-yyyy") + QUOTE;
        }
    }


}

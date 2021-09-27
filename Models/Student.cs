using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BackTest.Models
{
    class Student
    {

        #region Fields & Properties

        protected int _idEstudiante;
        public int IdEstudiante
        {
            get { return _idEstudiante; }
            set { _idEstudiante = value; }
        }

        protected int _nota;
        public int Nota
        {
            get { return _nota; }
            set { _nota = value; }
        }

        #endregion

        #region Builders

        public Student()
        {
            this._idEstudiante = 0;
            this._nota = 0;
        }

        public Student(int idEstudiante, int nota)
        {
            this._idEstudiante = idEstudiante;
            this._nota = nota;
        }

        public Student(Student obj)
        {
            this._idEstudiante = obj.IdEstudiante;
            this._nota = obj.Nota;
        }

        #endregion

        #region Methods

        public static bool saveStudentGrade(Student student) {
            bool result = false;
            try
            {
                SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=backend;Trusted_Connection=True;");
                using (conexion)
                {
                    string insert = "INSERT INTO estudiantes " +
                                           "(id_estudiante" +
                                           ", nota_estudiante)" +
                                     "VALUES" +
                                           "(" + student.IdEstudiante +
                                           ", " + student.Nota + ");";
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(insert, conexion);
                    int i = comando.ExecuteNonQuery();
                    if (i > 0) result = true;
                    conexion.Close();
                }
                return result;
            } catch (Exception e){
                Console.WriteLine(e);
                return result;
            }
        }

        public static List<Student> getStudentAverage()
        {
            try
            {
                SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=backend;Trusted_Connection=True;");
                List<Student> result = new List<Student>();
                using (conexion)
                {
                    string select = "SELECT " +
                                        "id_estudiante," + 
                                        "(SUM(nota_estudiante) / 3) AS promedio " +
                                    "FROM estudiantes " +
                                    "GROUP BY id_estudiante ";
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(select, conexion);
                    SqlDataReader registros = comando.ExecuteReader();
                    while (registros.Read())
                    {
                        Student student = new Student();
                        student.IdEstudiante = Convert.ToInt32(registros["id_estudiante"]);
                        student.Nota = Convert.ToInt32(registros["promedio"]);
                        result.Add(student);
                    }
                    conexion.Close();                   
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Student>();
            }
        }

        public static void cleanStudentsGrades()
        {
            try
            {
                SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=backend;Trusted_Connection=True;");
                using (conexion)
                {
                    string insert = "DELETE FROM estudiantes;";
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(insert, conexion);
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion


    }
}

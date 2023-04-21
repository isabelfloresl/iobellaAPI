using iobellaAPI.NewFolder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System;

namespace iobellaAPI.Controllers
{
    

        [Route("api/[controller]")]
        [ApiController]
        public class CitaController : ControllerBase
        {


            private readonly IConfiguration _configuration;

            private readonly IWebHostEnvironment _env;

            public CitaController(IConfiguration configuration, IWebHostEnvironment env)
            {
                _configuration = configuration;
                _env = env;
            }



            [HttpGet]
            public JsonResult Get()
            {
                string query = @"
                            select idCita, nombre, telefono,
                            correo,servicio
                            from
                            dbo.AgendarCita
                            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EstilistaAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult(table);
            }

            [HttpPost]
            public JsonResult Post(Cita est)
            {
                string query = @"
                            insert into dbo.AgendarCita
                           (nombre,telefono,correo,servicio)
                    values (@nombre,@telefono,@correo,@servicio)
                            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EstilistaAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
             
                        myCommand.Parameters.AddWithValue("@nombre", est.Nombre);
                        myCommand.Parameters.AddWithValue("@telefono", est.Telefono);
                        myCommand.Parameters.AddWithValue("@correo", est.Correo);
                        myCommand.Parameters.AddWithValue("@servicio", est.Servicio);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Añadido Exitosamente");
            }


            [HttpPut("{id}")]
            public JsonResult Put(Cita est)
            {
                string query = @"
                          update dbo.AgendarCita
                           set nombre= @nombre,
                            telefono=@telefono,
                            correo=@correo,
                            servicio=@servicio
                            where idCita=@idCita
                            ";


                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EstilistaAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@idCita", est.IdCita);
                        myCommand.Parameters.AddWithValue("@nombre", est.Nombre);
                        myCommand.Parameters.AddWithValue("@telefono", est.Telefono);
                        myCommand.Parameters.AddWithValue("@correo", est.Correo);
                        myCommand.Parameters.AddWithValue("@servicio", est.Servicio);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Actualizado Exitosamente");
            }

            [HttpDelete("{id}")]
            public JsonResult Delete(int id)
            {
                string query = @"
                          delete from dbo.AgendarCita
                            where idCita=@idCita
                            ";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EstilistaAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@idCita", id);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Eliminado Exitosamente");
            }



            


        }

}


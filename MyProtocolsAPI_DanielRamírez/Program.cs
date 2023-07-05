using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyProtocolsAPI_DanielRamírez.Models;

namespace MyProtocolsAPI_DanielRamírez
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //vamos a leer la etiqueta CNNSTR de appsettings.json para configurar la conexión a la bd
            var CnnStrigBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

            //quitamos de CNNSTR el dato de password ya que seria muy sencillo obtener la info de conexión del usuario de sql serer del archivo config app setting
            CnnStrigBuilder.Password = "123456";

            //es un objecto que permite la contrucción de cadenas de conexión de bd 
            //se pueden modifiar cada parte de la misma, pero el final debemos extraer un string con la info final
            string cnnStr = CnnStrigBuilder.ConnectionString;


            //ahora conectado el proyecto a la bd usando cnnStr
            builder.Services.AddDbContext<MyProtocolsDBContext>(options => options.UseSqlServer(cnnStr));







            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

// Importa os namespaces necess�rios para o funcionamento da aplica��o
using Microsoft.EntityFrameworkCore; // Utilizado para trabalhar com Entity Framework Core
using SistemaDeTarefas.Data; // Namespace do projeto para acesso aos dados
using SistemaDeTarefas.Repositorios; // Namespace do projeto para os reposit�rios
using SistemaDeTarefas.Repositorios.Interfaces; // Namespace do projeto para as interfaces dos reposit�rios

namespace SistemaDeTarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cria o builder da aplica��o web
            var builder = WebApplication.CreateBuilder(args);

            // Configura a pol�tica de CORS para permitir todas as origens, m�todos e cabe�alhos
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Adiciona os servi�os ao cont�iner de inje��o de depend�ncias

            // Adiciona o servi�o de controladores
            builder.Services.AddControllers();
            // Adiciona o servi�o para explorar os endpoints da API
            builder.Services.AddEndpointsApiExplorer();
            // Adiciona o servi�o do Swagger para documenta��o da API
            builder.Services.AddSwaggerGen();

            // Configura o Entity Framework com o provedor do SQL Server e a string de conex�o
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemaTarefasDBContex>(
                   options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            // Adiciona a interface do reposit�rio de usu�rio e sua implementa��o ao cont�iner
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IRestauranteRepositorio, RestauranteRepositorio>();
            builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            // Constr�i a aplica��o
            var app = builder.Build();

            // Configura o pipeline de tratamento de requisi��es HTTP
            if (app.Environment.IsDevelopment())
            {
                // Se o ambiente for de desenvolvimento, usa o Swagger e sua UI
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Usa a redirecionamento HTTPS
            app.UseHttpsRedirection();

            // Habilita a pol�tica de CORS configurada anteriormente
            app.UseCors("AllowAllOrigins");

            // Habilita a autoriza��o
            app.UseAuthorization();

            // Mapeia os controladores
            app.MapControllers();

            // Executa a aplica��o
            app.Run();
        }
    }
}

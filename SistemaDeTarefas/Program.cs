
// Importa os namespaces necessários para o funcionamento da aplicação
using Microsoft.EntityFrameworkCore; // Utilizado para trabalhar com Entity Framework Core
using SistemaDeTarefas.Data; // Namespace do projeto para acesso aos dados
using SistemaDeTarefas.Repositorios; // Namespace do projeto para os repositórios
using SistemaDeTarefas.Repositorios.Interfaces; // Namespace do projeto para as interfaces dos repositórios

namespace SistemaDeTarefas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cria o builder da aplicação web
            var builder = WebApplication.CreateBuilder(args);

            // Configura a política de CORS para permitir todas as origens, métodos e cabeçalhos
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

            // Adiciona os serviços ao contêiner de injeção de dependências

            // Adiciona o serviço de controladores
            builder.Services.AddControllers();
            // Adiciona o serviço para explorar os endpoints da API
            builder.Services.AddEndpointsApiExplorer();
            // Adiciona o serviço do Swagger para documentação da API
            builder.Services.AddSwaggerGen();

            // Configura o Entity Framework com o provedor do SQL Server e a string de conexão
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemaTarefasDBContex>(
                   options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            // Adiciona a interface do repositório de usuário e sua implementação ao contêiner
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IRestauranteRepositorio, RestauranteRepositorio>();
            builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            // Constrói a aplicação
            var app = builder.Build();

            // Configura o pipeline de tratamento de requisições HTTP
            if (app.Environment.IsDevelopment())
            {
                // Se o ambiente for de desenvolvimento, usa o Swagger e sua UI
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Usa a redirecionamento HTTPS
            app.UseHttpsRedirection();

            // Habilita a política de CORS configurada anteriormente
            app.UseCors("AllowAllOrigins");

            // Habilita a autorização
            app.UseAuthorization();

            // Mapeia os controladores
            app.MapControllers();

            // Executa a aplicação
            app.Run();
        }
    }
}

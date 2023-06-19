using HospitalManagement.DataAccess;
using HospitalManagement.DataAccess.Repository;
using HospitalManagement.Service.Services;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<IPatientService, PatientService>();
        builder.Services.AddTransient<IPatientRepository, PatientRepository>();
        builder.Services.AddTransient<DataSeeder>();
        builder.Services.AddDbContext<PatientDBContext>();
        var app = builder.Build();
        SeedData(app);

        void SeedData(IHost app){
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopedFactory?.CreateScope())
            {
                var service = scope?.ServiceProvider.GetService<DataSeeder>();
                    service?.SeedData();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
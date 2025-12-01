using Background.Interface;
using Background.Interface.Service;
using Background.Worker;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Notifiers;
using Domain.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Auth.Configuration;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Infrastructure.Mail;
using Infrastructure.Mail.Options;
using Microsoft.EntityFrameworkCore;
using PsicoAgendaAPI.AutoMapper.Mapper;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityWithJwt(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.ModelValidatorProviders.Clear();
});
builder.Services.AddDbContext<PsicoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAddressMapper, AddressMapper>();
builder.Services.AddScoped<IAvailabilitieMapper, AvailabilitieMapper>();
builder.Services.AddScoped<IConsentMapper, ConsentMapper>();
builder.Services.AddScoped<IHealthCareProfissionalMapper, HealthCareProfessionalMapper>();
builder.Services.AddScoped<ILocationMapper, LocationMapper>();
builder.Services.AddScoped<IPatientMapper, PatientMapper>();
builder.Services.AddScoped<ISessionNoteMapper, SessionNoteMapper>();
builder.Services.AddScoped<ISessionScheduleMapper, SessionScheduleMapper>();
builder.Services.AddScoped<ISpecialityMapper, SpecialityMapper>();
builder.Services.AddScoped<ITermsAcceptanceMapper, TermsAcceptanceMapper>();
builder.Services.AddScoped<ITermOfUseMapper, TermOfUseMapper>();
builder.Services.AddScoped<IUserMapper, UserMapper>();
builder.Services.AddScoped<IWaitMapper, WaitMapper>();


builder.Services.AddValidatorsFromAssemblyContaining(typeof(AddressValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(AvailabilityValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ConsentValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(HealthCareProfessionalValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(LocationValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(PatientValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(SessionNoteValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(SessionScheduleValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(TermsOfUseValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(WaitValidator));

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAvailabilitieRepository, AvailabilitieRepository>();
builder.Services.AddScoped<IConsentRepository, ConsentRepository>();
builder.Services.AddScoped<IHealthCareProfissionalRepository, HealthCareProfissionalRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ISessionNoteRepository, SessionNoteRepository>();
builder.Services.AddScoped<ISessionScheduleRepository, SessionScheduleRepository>();
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<ITermsAcceptanceRepository, TermsAcceptanceRepository>();
builder.Services.AddScoped<ITermOfUseRepository, TermOfUseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWaitRepository, WaitRepository>();

builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAvailabilitieService, AvailabilitieService>();
builder.Services.AddScoped<IConsentService, ConsentService>();
builder.Services.AddScoped<IHealthCareProfissionalService, HealthCareProfissionalService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ISessionNoteService, SessionNoteService>();
builder.Services.AddScoped<ISessionScheduleService, SessionScheduleService>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<ITermsAcceptanceService, TermsAcceptanceService>();
builder.Services.AddScoped<ITermOfUseService, TermOfUseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWaitService, WaitService>();

builder.Services.Configure<EmailProviderOptions>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();
builder.Services.AddScoped<EmailBackgroundWorker>();
builder.Services.AddSingleton<IHostedService>(sp =>
{
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    var scope = scopeFactory.CreateScope();
    return scope.ServiceProvider.GetRequiredService<EmailBackgroundWorker>();
});
builder.Services.AddScoped<INotifier, Notifier>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



await app.RunAsync();
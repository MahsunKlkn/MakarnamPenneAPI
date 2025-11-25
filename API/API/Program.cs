using System.Text;
using Business.Abstract;
using Business.Concrete;
using DataAccessLayer.Abstarct;
using DataAccessLayer.Concrete.EntityRepository;
using DataAccessLayer.Concrete.EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using DataAccessLayer.Concrete.EntityRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Read the PORT environment variable (Render provides it)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"))
    options.UseNpgsql(builder.Configuration.GetConnectionString("RenderConnection"))
);


string jwtKey = "araba_kalem_ceket_green_sapka_motor_ehliyet_matematik_kozmetik_tarım_sınav_duman";
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

// 2. Authentication (Kimlik Doğrulama) servisini ekleyin
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Sadece imza anahtarını doğrula
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = securityKey, // Koda gömülü anahtarı kullan

        // Issuer ve Audience kontrolü yapma
        ValidateIssuer = false,
        ValidateAudience = false,
        
        // Önemli: Token'ın son kullanma tarihini kontrol et
        ValidateLifetime = true 
    };
});

// 3. Authorization (Yetkilendirme) servisini ve Rol Politikalarını ekleyin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmployeePolicy", policy => 
        policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "Employee"));

    options.AddPolicy("CourierPolicy", policy =>
        policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "Courier"));

    options.AddPolicy("CustomerPolicy", policy => 
        policy.RequireClaim(System.Security.Claims.ClaimTypes.Role, "Customer"));
});
builder.Services.AddScoped<IKullaniciDal, EfKullaniciRepository>();
builder.Services.AddScoped<IKullaniciService, KullaniciManager>();
builder.Services.AddScoped<IProductDal, EfProductRepository>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IBasketDal, EfBasketRepository>();
builder.Services.AddScoped<IBasketService, BasketManager>();
builder.Services.AddScoped<IPaymentDal, EfPaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentManager>();
builder.Services.AddScoped<IOrderDal, EfOrderRepository>();
builder.Services.AddScoped<IOrderService, OrderManager>();

// CORS politikası ekleme
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", 
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

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

app.UseHttpsRedirection();

// CORS middleware'ini UseRouting'den sonra ve UseAuthorization'dan önce ekleyin
app.UseCors("AllowWebApp");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

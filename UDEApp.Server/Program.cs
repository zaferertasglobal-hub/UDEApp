using Microsoft.EntityFrameworkCore;
using UDEApp.Server.Data;
using UDEApp.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Servisler
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabanı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=udeapp.db"));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// VERİTABANI + TÜM DERSLER (KESİN ÇALIŞIR!)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // ESKİ TABLOYU SİL, YENİDEN OLUŞTUR (EN TEMİZ ÇÖZÜM)
    db.Database.EnsureDeleted();
    db.Database.Migrate(); // FullContent ve ShortDescription alanlarını ekler

    if (!db.Courses.Any())
    {
        db.Courses.AddRange(
            new Course
            {
                Name = "Mathematics I",
                Specialization = "Both",
                Semester = "WS",
                ECTS = 8,
                ShortDescription = "Lineer cebir ve analize giriş",
                FullContent = @"# Mathematics I
• Vektörler, matrisler, determinant
• Lineer denklem sistemleri (Gauss-Jordan)
• Özdeğer ve özvektörler
• Limit, türev, integral
• Kısmi türevler ve çok katlı integraller"
            },

            new Course
            {
                Name = "Fundamentals of Computer Engineering 1",
                Specialization = "Both",
                Semester = "WS",
                ECTS = 4,
                ShortDescription = "Dijital mantık temelleri",
                FullContent = @"# Dijital Mantık
• Boolean cebiri ve Karnaugh haritaları
• Kombinezonsal devreler (multiplexer, adder)
• Ardışıl devreler (flip-flop, register)
• Sonlu durum makineleri"
            },

            new Course
            {
                Name = "Discrete Mathematics",
                Specialization = "Both",
                Semester = "WS",
                ECTS = 5,
                ShortDescription = "Ayrık yapılar",
                FullContent = @"# Discrete Mathematics
• Mantık ve ispat yöntemleri
• Kümeler, ilişkiler, fonksiyonlar
• Kombinatorik ve graf teorisi
• Dijkstra, Euler yolu"
            },

            new Course
            {
                Name = "Data Structures and Algorithms",
                Specialization = "Software",
                Semester = "SS",
                ECTS = 8,
                ShortDescription = "Veri yapıları ve algoritmalar",
                FullContent = @"# Veri Yapıları ve Algoritmalar
• Array, Linked List, Stack, Queue
• Binary Search Tree, AVL ağacı
• Hash tabloları
• QuickSort, MergeSort, Dijkstra"
            },

            new Course
            {
                Name = "Automata and Formal Languages",
                Specialization = "Software",
                Semester = "SS",
                ECTS = 6,
                ShortDescription = "Biçimsel diller teorisi",
                FullContent = @"# Automata and Formal Languages
• DFA, NFA, düzenli ifadeler
• Pumping Lemma
• Context-free diller ve pushdown otomat
• Turing makineleri
• Karar verilebilirlik"
            },

            new Course
            {
                Name = "Operating Systems",
                Specialization = "Software",
                Semester = "WS",
                ECTS = 6,
                ShortDescription = "İşletim sistemi temelleri",
                FullContent = @"# Operating Systems
• Process vs Thread
• CPU Scheduling (Round Robin, SJF)
• Deadlock ve Banker algoritması
• Paging, TLB, LRU"
            },

            new Course
            {
                Name = "Software Engineering",
                Specialization = "Software",
                Semester = "WS",
                ECTS = 6,
                ShortDescription = "Yazılım süreçleri",
                FullContent = @"# Software Engineering
• Agile, Scrum, Sprint
• UML diyagramları
• Design Patterns (Singleton, Factory)
• Git ve CI/CD"
            },

            new Course
            {
                Name = "Databases",
                Specialization = "Software",
                Semester = "WS",
                ECTS = 4,
                ShortDescription = "Veritabanı tasarımı",
                FullContent = @"# Databases
• ER modelleme
• Normalizasyon (1NF-3NF)
• SQL (JOIN, INDEX)
• Transaction ACID"
            },

            new Course
            {
                Name = "Embedded Systems",
                Specialization = "Both",
                Semester = "WS",
                ECTS = 5,
                ShortDescription = "Mikrodenetleyici programlama",
                FullContent = @"# Embedded Systems
• ARM Cortex-M
• GPIO, ADC, PWM
• Kesmeler (interrupt)
• FreeRTOS görevleri"
            },

            new Course
            {
                Name = "Mobile Communications",
                Specialization = "Communications",
                Semester = "SS",
                ECTS = 4,
                ShortDescription = "GSM'den 5G'ye",
                FullContent = @"# Mobile Communications
• 5G NR, Massive MIMO
• Beamforming
• OFDMA vs SC-FDMA
• Fiziksel katman kanalları"
            },

            new Course
            {
                Name = "Digital Signal Processing",
                Specialization = "Communications",
                Semester = "WS",
                ECTS = 5,
                ShortDescription = "DFT, FFT, filtreler",
                FullContent = @"# Digital Signal Processing
• DFT ve FFT
• Z-dönüşümü
• FIR/IIR filtre tasarımı
• MATLAB uygulamaları"
            },

            new Course
            {
                Name = "Machine Learning",
                Specialization = "Software",
                Semester = "WS",
                ECTS = 6,
                ShortDescription = "Temel ML ve derin öğrenme",
                FullContent = @"# Machine Learning
• Lineer regresyon, SVM
• Neural network, backpropagation
• TensorFlow ile model eğitimi"
            },

            new Course
            {
                Name = "Bachelor Thesis",
                Specialization = "Both",
                Semester = "WS/SS",
                ECTS = 12,
                ShortDescription = "Bitirme projesi",
                FullContent = @"# Bitirme Tezi
• 30-40 sayfa rapor
• Kod + test
• Sunum ve savunma
• Önerilen konular: 5G, TinyML, IoT"
            }
        );
        db.SaveChanges();
    }
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
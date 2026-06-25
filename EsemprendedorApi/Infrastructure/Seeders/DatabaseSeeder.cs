using EsemprendedorApi.Domain.Entities;
using EsemprendedorApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EsemprendedorApi.Infrastructure.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Sections.AnyAsync())
            return;

        var sections = new List<Section>
        {
            new Section
            {
                Slug = "gastronomia",
                Title = "Gastronomía",
                Label = "Sección 01",
                BgLight = false,
                Keywords = "gastronomia gastronomía comida catering tortas pastelería panadería confitería lunch viandas",
                Cards = new List<Card>
                {
                    new Card { Icon = "🎂", BackgroundImage = "assets/panabruzzone.png", Chip = "Panadería · Confitería", Name = "Bruzzone", Service = "Cocinamos en familia y con mucho amor. Tortas, cheesecakes y productos artesanales elaborados con dedicación.", Contact = "📍 Mendez 206, Glew", Featured = true },
                    new Card { Icon = "🍽️", Chip = "Catering", Name = "Mountain Dew Catering", Service = "Desayunos keto · Casamientos · Bandejas dulces. El servicio ideal para cada ocasión.", Contact = "📲 @mountaindewcatering" },
                    new Card { Icon = "🍞", Chip = "Panadería · Confitería", Name = "Los Manjares de Alé", Service = "Elaboración artesanal en horno a leña. Lunch · Viandas · Facturas · Tartas.", Contact = "📍 Plaza Brown 211 – Adrogué<br>☎ Tel. 4600-0503 · 15-3732-2344<br>📍 Dr. Lucio Melendez 1531 – Adrogué · Tel. 4214-0714" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Pastelería Express", Service = "Pedidos de tortas y postres listos en menos de 24 horas.", Contact = "📲 +54 9 11 1010-2020" },
                    new SimpleCard { Name = "Menú Semanal", Service = "Platos caseros y cajas de viandas para la semana.", Contact = "📧 menu@emprendegastro.com" },
                    new SimpleCard { Name = "Catering en Casa", Service = "Servicio a domicilio para eventos pequeños y reuniones.", Contact = "📲 +54 9 11 3030-4040" }
                }
            },
            new Section
            {
                Slug = "educacion",
                Title = "Educación",
                Label = "Sección 02",
                BgLight = true,
                Keywords = "educación cursos talleres formación capacitación crédito",
                Cards = new List<Card>
                {
                    new Card { Icon = "📚", Chip = "Estudio", Name = "Elevarte Estudio", Service = "Sumate a elevar tus capacidades. Formación, talleres y cursos para crecer personal y profesionalmente.", Contact = "📍 Pl. Adrogué 54, Adrogué · 📲 Elevarte.estudio" },
                    new Card { Icon = "💡", Chip = "Capacitación", Name = "Mejorísdar", Service = "Haciendo que las cosas pasen. Líneas de crédito para emprendedores, mejoramiento habitacional y cursos de capacitación.", Contact = "Escribinos para más información" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Taller Creativo", Service = "Clases cortas para emprendedores que quieren crear su marca.", Contact = "📲 +54 9 11 1313-1414" },
                    new SimpleCard { Name = "Mentoría Online", Service = "Acompañamiento digital para mejorar tus habilidades.", Contact = "📧 mentor@educacion.com" },
                    new SimpleCard { Name = "Clases Grupales", Service = "Cursos en grupo con descuentos especiales.", Contact = "📲 +54 9 11 1515-1616" }
                }
            },
            new Section
            {
                Slug = "servicios",
                Title = "Servicios Profesionales",
                Label = "Sección 03",
                BgLight = false,
                Keywords = "servicios profesionales seguros distribuidora cotización",
                Cards = new List<Card>
                {
                    new Card { Icon = "🏠", Chip = "Seguros", Name = "Grupo Integro", Service = "¿Tu casa está asegurada? Solicitá una cotización y comprobá lo económico que es estar cubierto. Asesores de seguros con respaldo de la SSN.", Contact = "📞 11-6243-6177", Featured = true },
                    new Card { Icon = "📦", Chip = "Distribuidora", Name = "Enece Distribuidora", Service = "Distribución profesional de productos. Consultanos para conocer nuestro catálogo completo.", Contact = "Escribinos para más información" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Consultoría Express", Service = "Asesoría rápida para tu empresa o emprendimiento.", Contact = "📲 +54 9 11 1717-1818" },
                    new SimpleCard { Name = "Seguro Fácil", Service = "Cotizaciones de seguros en pocos minutos.", Contact = "📧 seguros@profesionales.com" },
                    new SimpleCard { Name = "Cotización Directa", Service = "Consultas y presupuestos sin compromiso por WhatsApp.", Contact = "📲 +54 9 11 1919-2020" }
                }
            },
            new Section
            {
                Slug = "alojamientos",
                Title = "Alojamientos",
                Label = "Sección 04",
                BgLight = true,
                Keywords = "alojamientos hotel apart departamentos estadía hospedaje",
                Cards = new List<Card>
                {
                    new Card { Icon = "🏨", Chip = "Apart Hotel", Name = "Adrogué Apart Hotel", Service = "Alojamiento confortable en el corazón de Adrogué. Departamentos equipados con todo lo que necesitás para una estadía perfecta.", Contact = "Consultanos por disponibilidad", Featured = true }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Check-in 24h", Service = "Llegadas nocturnas y atención fuera de horario.", Contact = "📲 +54 9 11 2121-2222" },
                    new SimpleCard { Name = "Suite Ejecutiva", Service = "Espacios cómodos para estadías de trabajo y descanso.", Contact = "📧 suites@alojamientos.com" },
                    new SimpleCard { Name = "Renta Mensual", Service = "Tarifas especiales para estadías prolongadas.", Contact = "📲 +54 9 11 2323-2424" }
                }
            },
            new Section
            {
                Slug = "electricidad",
                Title = "Electricidad",
                Label = "Sección 05",
                BgLight = false,
                Keywords = "electricidad taller instalaciones reparaciones eléctrico",
                Cards = new List<Card>
                {
                    new Card { Icon = "⚡", Chip = "Taller", Name = "Del Río e Hijos", Service = "Taller de electricidad con trayectoria. Servicio técnico, instalaciones y reparaciones para el hogar y la industria.", Contact = "Consultanos para presupuesto", Keywords = "electricista instalaciones reparaciones doméstico industrial placas" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Instalación Express", Service = "Conexiones y reparaciones eléctricas en menos de 48 horas.", Contact = "📲 +54 9 11 2525-2626" },
                    new SimpleCard { Name = "Revisión Técnica", Service = "Inspecciones preventivas para dejar todo en regla.", Contact = "📧 inspeccion@electricidad.com" },
                    new SimpleCard { Name = "Mantenimiento", Service = "Servicio continuo para instalaciones domiciliarias.", Contact = "📲 +54 9 11 2727-2828" }
                }
            },
            new Section
            {
                Slug = "eventos",
                Title = "Eventos",
                Label = "Sección 06",
                BgLight = true,
                Keywords = "eventos quincho cumpleaños casamientos despedidas salón fiestas",
                Cards = new List<Card>
                {
                    new Card { Icon = "🎉", Chip = "Salón · Quincho", Name = "El Quincho", Service = "Un espacio perfecto para tus eventos sociales. Por día o estadía en Burzaco. Cumpleaños · Despedidas · Casamientos · Estadías.", Contact = "Reservas y consultas: escribinos", Featured = true }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Decoración Premium", Service = "Montaje completo de ambientación para tu evento.", Contact = "📲 +54 9 11 2929-3030" },
                    new SimpleCard { Name = "Sonido y Luces", Service = "Equipo profesional para fiestas y reuniones.", Contact = "📧 sonido@eventos.com" },
                    new SimpleCard { Name = "Coordinación Integral", Service = "Organización full service para celebraciones.", Contact = "📲 +54 9 11 3131-3232" }
                }
            },
            new Section
            {
                Slug = "opticas",
                Title = "Ópticas",
                Label = "Sección 07",
                BgLight = false,
                Keywords = "ópticas optica ortopedia lentes marcos visual",
                Cards = new List<Card>
                {
                    new Card { Icon = "👓", Chip = "Óptica · Ortopedia", Name = "Óptica y Ortopedia González", Service = "Tu salud visual y ortopédica en manos expertas. Gran variedad de marcos, lentes y productos ortopédicos.", Contact = "Consultanos por turno y productos" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Lentes de Sol", Service = "Catálogo moderno con protección UV.", Contact = "📲 +54 9 11 3333-3434" },
                    new SimpleCard { Name = "Examen Visual", Service = "Chequeo de la vista con profesionales en el local.", Contact = "📧 examen@opticas.com" },
                    new SimpleCard { Name = "Marcos con Estilo", Service = "Diseños exclusivos para todos los gustos.", Contact = "📲 +54 9 11 3535-3636" }
                }
            },
            new Section
            {
                Slug = "hogar",
                Title = "Hogar",
                Label = "Sección 08",
                BgLight = true,
                Keywords = "hogar colchones futón muebles cama descanso",
                Cards = new List<Card>
                {
                    new Card { Icon = "🛏️", Chip = "Colchones", Name = "Colchones para Futón Adrogué", Service = "Colchones y futones de calidad para tu hogar. Amplia variedad de medidas y modelos.", Contact = "📞 1163647870" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Descanso Premium", Service = "Colchones ergonómicos con entrega en Adrogué.", Contact = "📲 +54 9 11 9900-7777" }
                }
            },
            new Section
            {
                Slug = "estacionamientos",
                Title = "Estacionamientos",
                Label = "Sección 09",
                BgLight = false,
                Keywords = "estacionamiento parking auto vehículo garage",
                Cards = new List<Card>
                {
                    new Card { Icon = "🅿️", Chip = "Parking", Name = "Estacionamiento Parking Adrogué", Service = "Tu vehículo seguro en el centro de Adrogué. Estacionamiento cubierto y vigilado.", Contact = "Consultanos por tarifas y disponibilidad" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Guardado Seguro", Service = "Estacionamiento cubierto con vigilancia 24/7.", Contact = "📲 +54 9 11 8801-2323" }
                }
            },
            new Section
            {
                Slug = "distribuidoras",
                Title = "Distribuidoras",
                Label = "Sección 10",
                BgLight = true,
                Keywords = "distribuidoras distribución productos mayorista",
                Cards = new List<Card>
                {
                    new Card { Icon = "📦", Chip = "Distribución", Name = "Marbyn", Service = "Distribución eficiente de productos con cobertura en la zona sur del Gran Buenos Aires.", Contact = "Consultanos por catálogo" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Envío Eficiente", Service = "Logística y entrega para pedidos mayoristas.", Contact = "📲 +54 9 11 7701-3434" }
                }
            },
            new Section
            {
                Slug = "logisticas",
                Title = "Logística",
                Label = "Sección 11",
                BgLight = false,
                Keywords = "logística logistica entregas envíos distribución transporte fletes",
                Cards = new List<Card>
                {
                    new Card { Icon = "🚛", Chip = "Logística · Distribución", Name = "Lucpack", Service = "Agilidad, seguridad y cumplimiento en cada operación. Eficiencia logística para empresas que necesitan resultados.", Contact = "🌐 www.lucpack.com · 📲 @lucpack.logistica", Featured = true },
                    new Card { Icon = "📬", Chip = "Entregas", Name = "Flexisur", Service = "Flexibilizá tus entregas. Servicio de distribución adaptable a las necesidades de tu negocio.", Contact = "Consultanos por cobertura y tarifas" }
                },
                SimpleCards = new List<SimpleCard>
                {
                    new SimpleCard { Name = "Envíos Flexibles", Service = "Planes de entrega adaptados a tus necesidades.", Contact = "📲 +54 9 11 6601-4545" }
                }
            }
        };

        await context.Sections.AddRangeAsync(sections);
        await context.SaveChangesAsync();
    }
}
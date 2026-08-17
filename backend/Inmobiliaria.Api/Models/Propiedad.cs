using System.Text.Json.Serialization;

namespace Inmobiliaria.Api.Models;

// Clase base de la jerarquía TPH: todos los tipos de propiedad heredan estos campos
// y se guardan en una única tabla "Propiedades", con la columna "Tipo" como discriminador.
//
// Los atributos JsonDerivedType le enseñan a System.Text.Json a serializar/deserializar
// automáticamente el subtipo correcto según el campo "tipo" del JSON (mismo nombre que
// usa el discriminador de EF Core). Así, tanto para leer como para crear/editar una
// propiedad, el controller puede trabajar directamente con "Propiedad" sin mapear a mano
// cada uno de los tipos.
//
// IMPORTANTE: para que la deserialización (POST/PUT) funcione, "tipo" tiene que ser la
// PRIMERA propiedad del JSON recibido. Si va en otro lugar, falla con
// "must specify a type discriminator" aunque el campo esté presente.
[JsonPolymorphic(TypeDiscriminatorPropertyName = "tipo")]
[JsonDerivedType(typeof(Casa), nameof(TipoPropiedad.Casa))]
[JsonDerivedType(typeof(Departamento), nameof(TipoPropiedad.Departamento))]
[JsonDerivedType(typeof(Galpon), nameof(TipoPropiedad.Galpon))]
[JsonDerivedType(typeof(Local), nameof(TipoPropiedad.Local))]
[JsonDerivedType(typeof(Quinta), nameof(TipoPropiedad.Quinta))]
[JsonDerivedType(typeof(Terreno), nameof(TipoPropiedad.Terreno))]
[JsonDerivedType(typeof(PH), nameof(TipoPropiedad.PH))]
[JsonDerivedType(typeof(Emprendimiento), nameof(TipoPropiedad.Emprendimiento))]
public abstract class Propiedad
{
    public int Id { get; set; }

    // Datos básicos
    public required string Titulo { get; set; }

    // Nullable a nivel de base de datos (comparten tabla los 8 tipos), pero el frontend
    // los pide como obligatorios para cualquier tipo de propiedad, incluido Emprendimiento.
    public Operacion? Operacion { get; set; }
    public EstadoPublicacion Estado { get; set; } = EstadoPublicacion.Vigente;

    // Precios
    public decimal? Monto { get; set; }
    public Moneda? Moneda { get; set; }
    public bool NoPublicarPrecio { get; set; }
    public decimal? ExpensasMonto { get; set; }
    // Valores esperados: "No incluidas" | "Incluidas" | "A consultar" | "No paga expensas" | "Fijos" | "Aproximados"
    public string? ExpensasTipo { get; set; }

    // Condicionales generales
    public bool AptoProfesional { get; set; }
    public bool AceptaMascotas { get; set; }
    public bool? PropiedadOcupada { get; set; }
    public bool EsBarrioCerradoOrCountry { get; set; }

    // Ubicación
    public required string ZonaGeografica { get; set; }
    public required string PartidoLocalidad { get; set; }
    public required string BarrioCiudad { get; set; }
    public required string Calle { get; set; }
    public string? NroCalle { get; set; }
    public string? Piso { get; set; }
    public string? Depto { get; set; }
    public VisibilidadDireccion VisibilidadDireccion { get; set; } = VisibilidadDireccion.DireccionReal;
    public string? EntreCalle1 { get; set; }
    public string? EntreCalle2 { get; set; }
    public string? CercaDe { get; set; }
    public double? Latitud { get; set; }
    public double? Longitud { get; set; }

    // Entorno / condición
    public CondicionPropiedad? EstadoPropiedad { get; set; }
    public int? AntiguedadAnos { get; set; }
    public bool? EsAEstrenar { get; set; }
    // Valores esperados: "Mar" | "Lago / laguna" | "Río"
    public string? TipoCosta { get; set; }
    // Valores esperados: "Vista al mar" | "Vista al lago" | "Vista al río" | "Vista a la montaña" | "Vista al bosque" | "Vista al golf" | "Vista a la ciudad"
    public string? TipoVista { get; set; }
    // Valores esperados: "Plano" | "Suave" | "Pronunciado" | "Muy pronunciado"
    public string? TipoPendiente { get; set; }

    // Contenido
    public required string Descripcion { get; set; }

    public List<ImagenPropiedad> Imagenes { get; set; } = [];
}

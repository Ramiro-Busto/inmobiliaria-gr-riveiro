namespace Inmobiliaria.Api.Models;

// Discriminador de tabla (TPH): identifica qué tipo de propiedad es cada fila.
public enum TipoPropiedad
{
    Casa,
    Departamento,
    Galpon,
    Local,
    Quinta,
    Terreno,
    PH,
    Emprendimiento
}

public enum Operacion
{
    Venta,
    Alquiler,
    AlquilerTemporario,
    Remate
}

// Estado de la publicación (no confundir con la condición física de la propiedad).
public enum EstadoPublicacion
{
    Vigente,
    Reservado,
    Suspendido,
    Historico,
    EnTasacion,
    Alquilado,
    Vendido,
    Borrador
}

public enum Moneda
{
    Pesos,
    Dolares
}

public enum VisibilidadDireccion
{
    DireccionAproximada,
    DireccionReal,
    OcultarDireccion
}

// Condición/estado físico de la propiedad (campo "estadoPropiedad" del diseño original).
public enum CondicionPropiedad
{
    Excelente,
    MuyBueno,
    Bueno,
    Regular,
    ARefaccionar
}

// Tipo de consulta recibida desde el sitio público.
public enum TipoConsulta
{
    Contacto,
    Tasacion
}

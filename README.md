# AppGeneral

Una biblioteca de clases en C# que implementa muchas acciones utilizadas frecuentemente en aplicaciones, como manipulación de cadenas, formateo de fechas, manejo de archivos, operaciones de correo electrónico y registro de eventos.

## Descripción del Proyecto

`AppGeneral` es una utilidad (Class Library) escrita en C# diseñada para simplificar y reutilizar tareas comunes de desarrollo de software. Proporciona métodos útiles para limpiar cadenas (para SQL o XML), codificar/decodificar Base64, gestionar logs de eventos del sistema (EventLog), validar direcciones de correo electrónico, y una completa gestión e inspección de directorios y archivos.

## Pila Tecnológica (Tech Stack)

* **Lenguaje:** C#
* **Framework:** .NET Framework 4.5.2
* **Entorno de Construcción:** MSBuild / Visual Studio 2013+
* **Tipo de Proyecto:** Biblioteca de Clases (Class Library / `.dll`)

## Estructura Principal del Repositorio

```text
.
├── AppGeneral/               # Código fuente principal de la biblioteca
│   ├── AppGeneral.cs         # Clase principal con todas las funciones utilitarias
│   ├── AppGeneral.csproj     # Archivo de proyecto de C#
│   └── Properties/
│       └── AssemblyInfo.cs   # Información de ensamblado
├── AppGeneral.sln            # Archivo de Solución (Solution File)
└── README.md                 # Este archivo
```

## Instrucciones de Instalación y Configuración

Sigue estos pasos para clonar y compilar la biblioteca localmente:

### Requisitos Previos

* Sistema Operativo Windows (recomendado por el uso de `System.Diagnostics.EventLog` que es específico de Windows) o un entorno que soporte Mono/.NET Core adaptado (aunque está enfocado a .NET Framework 4.5.2).
* MSBuild instalado en tu PATH (usualmente instalado con Visual Studio o .NET SDK).
* Git para clonar el repositorio.

### Paso a paso

1. **Clonar el repositorio:**
   Abre una terminal o símbolo del sistema y ejecuta:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd <NOMBRE_DEL_DIRECTORIO_CLONADO>
   ```

2. **Compilar el código fuente:**
   Puedes compilar directamente desde la línea de comandos usando `MSBuild`:
   ```bash
   msbuild AppGeneral.sln /p:Configuration=Release
   ```
   *Nota: Si estás usando Linux o macOS, puedes usar `xbuild` o mono si tienes instaladas las herramientas de desarrollo de Mono.*

3. **Verificar el resultado:**
   Si la compilación es exitosa, se generará la biblioteca en:
   `AppGeneral\bin\Release\AppGeneral.dll`

4. **Uso en tu proyecto:**
   Para utilizar la biblioteca, añade la referencia del archivo `.dll` generado a tu proyecto de .NET y añade el espacio de nombres `using AppGeneral;` en tu código.

## Guía Básica de Uso

A continuación, se presentan algunos ejemplos de comandos y cómo instanciar y usar los métodos de la clase `AppGeneral`:

```csharp
using System;
using AppGeneral;

class Program
{
    static void Main()
    {
        // 1. Instanciar la clase principal
        var utils = new AppGeneral.AppGeneral();

        // 2. Formateo de fechas
        // Transforma "31/12/2023" a "2023-12-31" (Formato 5: YYYY-MM-DD)
        string fechaFormateada = utils.CadenaFecha("31/12/2023", AppGeneral.AppGeneral.FormatoFecha.Guion_YYYY_MM_DD, false);
        Console.WriteLine($"Fecha: {fechaFormateada}");

        // 3. Validar un correo electrónico
        bool esValido = utils.eMailVerifica("usuario@ejemplo.com");
        Console.WriteLine($"¿Email válido? {esValido}");

        // 4. Codificar texto en Base64
        string base64 = utils.Cadena_To_Base64("Hola Mundo");
        Console.WriteLine($"Base64: {base64}");

        // Decodificar desde Base64
        string texto = utils.Cadena_From_Base64(base64);
        Console.WriteLine($"Texto original: {texto}");

        // 5. Limpiar cadenas para prevenir inyecciones SQL básicas
        string sqlSegura = utils.CleanStringforSQL("SELECT * FROM users WHERE name = 'admin'; --");
        Console.WriteLine($"SQL Limpio: {sqlSegura}");

        // 6. Listar archivos en un directorio (Ejemplo: C:\temp) ordenados por fecha de creación
        var archivos = utils.Get_Files_List(@"C:\temp", AppGeneral.AppGeneral.FileOrden.Fec_Creacion, false, false, "*.*");
        foreach (var archivo in archivos)
        {
            Console.WriteLine($"Archivo: {archivo.Nombre} - Tamaño: {archivo.Tamano} bytes");
        }
    }
}
```

## Contribuciones

Si deseas mejorar o extender las utilidades que proporciona `AppGeneral`, ¡eres bienvenido! Siéntete libre de hacer un fork del proyecto, crear tu rama de funcionalidad, y enviar un Pull Request.


# DawerBook

Sistema de gestión de biblioteca desarrollado en ASP.NET Core MVC.

## Tecnologías utilizadas

- C#
- ASP.NET Core MVC
- Entity Framework Core
- Bootstrap
- SQL

## Funcionalidades

- Gestión de libros.
- Gestión de usuarios.
- Registro de préstamos.
- Registro de devoluciones.

## ACTUALIZACION - 24/11/2026

- Se agregó una validación en el método de creación de libros para impedir el registro de ejemplares con una cantidad igual a cero.

- Se estandarizó el almacenamiento de los datos en mayúsculas, incluyendo los títulos, géneros y autores de los libros, así como los nombres de los usuarios.

- Se estandarizaron los textos correspondientes a los estados y disponibilidades de los libros.

- Se reemplazó el campo de texto del estado de los libros por un selector (`select`) con opciones predefinidas, evitando la introducción manual de valores incorrectos.

- Se incorporaron el controlador LoginController y la vista Login como estructura inicial para la futura implementación del sistema de autenticación.

- Se incorporó el archivo README.md para documentar el proyecto y sus actualizaciones.

- Se implementó la lógica de autenticación y autorización en el `LoginController`.

- Se configuró la autenticación mediante cookies en `Program.cs`.

- Se implementó la validación de credenciales contra la base de datos.

- Se establecieron dos roles de acceso: Administrador y Vendedor.

## ÚLTIMA ACTUALIZACIÓN - 27/11/2026

- Se agregaron los perfiles de usuario en la tabla `Login`, correspondientes a los roles de Administrador y Bibliotecario.

- Se implementó el formulario de inicio de sesión, incluyendo los mensajes de validación enviados desde el controlador y el diseño de referencia.

- Se incorporó un menú lateral que muestra el nombre y el rol del usuario autenticado mediante Claims, además de la opción de cerrar sesión.

- Se creó una pantalla de inicio sencilla que presenta un resumen de los libros, usuarios y préstamos registrados. La implementación del dashboard completo, con estadísticas y gráficos, queda como una tarea pendiente.

- Se implementó la autorización basada en roles en los métodos de los controladores, estableciendo los siguientes permisos:
  - **Administrador:** acceso completo a todos los módulos y funcionalidades del sistema.
  - **Bibliotecario:** puede consultar, registrar y editar libros; registrar y editar usuarios; y registrar préstamos.
  - **Ambos roles:** pueden consultar el historial de préstamos devueltos.

- Se reorganizó la configuración de autenticación en `Program.cs`, ubicándola antes de `builder.Build()`.

- Se protegieron las rutas del sistema mediante atributos de autorización, restringiendo el acceso según los permisos correspondientes a cada rol.

- Se incorporaron nuevas validaciones para garantizar la integridad de los datos y evitar registros duplicados:
  - Se impide registrar dos usuarios con el mismo correo electrónico.
  - El sistema verifica si un libro ya se encuentra registrado antes de guardarlo, evitando la duplicación de registros.

### Cómo abrir

1. Extraer el ZIP y abrir `DawerBook.sln` en Visual Studio con soporte de .NET 9.
2. Restaurar los paquetes NuGet y ejecutar el proyecto DawerBook.
3. Probar el ingreso y cierre de sesión con las cuentas de la base incluida.

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

## ULTIMA ACTUALIZACION - 24/11/2026

- Se agregó una validación en el método de creación de libros para impedir el registro de ejemplares con una cantidad igual a cero.

- Se estandarizó el almacenamiento de los datos en mayúsculas, incluyendo los títulos, géneros y autores de los libros, así como los nombres de los usuarios.

- Se estandarizaron los textos correspondientes a los estados y disponibilidades de los libros.

- Se reemplazó el campo de texto del estado de los libros por un selector (`select`) con opciones predefinidas, evitando la introducción manual de valores incorrectos.

- Se incorporaron el controlador LoginController y la vista Login como estructura inicial para la futura implementación del sistema de autenticación.

- Se incorporó el archivo README.md para documentar el proyecto y sus actualizaciones.
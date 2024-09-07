# Versión

.NET Core 8.0.401 and Entity Framework back-end, Base de Datos MySql

## Prerequisitos

* [.NET Core 8.0.401 SDK](https://dotnet.microsoft.com/download)

* [VS Code](https://code.visualstudio.com/)

Una vez clonado el repositorio se debe ejecutar el script de la base de datos:
db.sql

## Levantar el proyecto

Ejecutar el comando:
dotnet run

## Probar los EndPoints

Se puede importar el json que se encuentra en el aplicativo, en el postman, el cual tiene ejemplos de consumos de los servicios web
InitumS.postman_collection

## Consideraciones

El test realizado fue utilizado Entity Framework Core y la inyección de dependencias, se crearon Controladores, Servicios y Repositorios.
Se implementó la autenticación JWT y la seguridad en el manejo de credenciales.
Se implementaron los métodos solicitados en el ejercicio, y se manejaron los mensajes y errores adecuadamente utilizando además la interfaz ILogger para registrar eventos y errores. 



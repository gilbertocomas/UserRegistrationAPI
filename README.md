# UserRegistrationAPI

Este proyecto consiste en una API que expone un Endpoint para registro de
usuarios, fue desarrollada con **ASP.NET core 8** y **Entity Framework Core**.
Proporciona funcionalidades para registrar usuarios, a través de una entrada
JSON:

```json
{
  "name": "Gilberto Comas",
  "email": "test@test.com",
  "password": "TestSobreTest@125",
  "phones": [
    {
      "number": "2232536",
      "cityCode": "1",
      "countryCode": "809"
    }
  ]
}
```

**En esta entrada se realizan las siguientes validaciones:**

- Que el correo sea válido a través de una expresión regular.
- Que no exista un usuario registrado con el mismo correo.
- Que la contraseña sea mayor a 8 carácteres, que posea por lo menos una letra
  mayúscula, que posea por lo menos un número y carácter especial.

## Requisitos Previos

- [.NET 8 SDK]
- [SQL Server]

## Configuración Inicial

1. **Clonar el Repositorio**

Clona el proyecto en tu máquina local:

```bash
git clone https://github.com/gilbertocomas/UserRegistrationAPI.git
cd UserRegistrationAPI
```

2. **Configurar el Connection String**

Ve al archivo appsettings.json en el proyecto y reemplaza el valor del
ConnectionString con los datos de tu servidor de base de datos.

```json
"ConnectionStrings": {
  "defaultConnection": "Data Source = .; Initial Catalog = UserRegistrationDB; Integrated Security = True; TrustServerCertificate = True"
}
```

# Opciones para Configurar la Base de Datos

Hay dos opciones para configurar la base de datos y ejecutar el proyecto:

## Opción 1: Migraciones Automáticas

Ejecuta el siguiente comando en la `Consola del Administrador de paquetes` para
aplicar las migraciones directamente a tu base de datos configurada en
`ConnectionString`:

```bash
update-database
```

Este comando tomará las migraciones realizadas en el desarrollo de este
aplicativo y creará todos los objetos en la Base de Datos configurada, partiendo
de las declaraciones que se hicieren en la etapa de desarrollo en el
`ApplicationDbContext`.

## Opción 2: Ejecutar Scripts SQL Manualmente

Si lo prefieres, tienes la opcion de ejectuar el script `InitialCrate.sql` que
se encuentra en el directorio `db`, utilizando una herramienta como
`SQL Server Management Studio (SSMS)`.

Una vez el `ConnectionString` esté debidamente configurado para su ambiente, no
debería existir ningún tipo de inconviente para la ejecutación, comución y
posterior creación de usuarios en el aplicativo.

# Ejecución de la API

Para ejecutar la API, puede usar `VisualStudio 2022` o el siguiente comando
desde el CLI:

```bash
dotnet run
```

La API estará disponible en su explorar principal e iniciará usando Swagger `https://localhost:7170/swagger/index.html`. También en la url `https://localhost:7170`.

# Endpoints Disponibles

1. **Registro de Usuario** 
   Tendrá disponible un Endpoint **`POST` `/api/UserRegistrationAPI`**. Crea un nuevo usuario
   en la base de datos. Los datos del usuario deben enviarse en el cuerpo de la
   solicitud en formato JSON.

```json
{
  "name": "Gilberto Comas",
  "email": "test@test.com",
  "password": "TestSobreTest@125",
  "phones": [
    {
      "number": "2232536",
      "cityCode": "1",
      "countryCode": "809"
    }
  ]
}
```

# Seguridad y Configuración de Patrones de Expresiones Regulares

Los patrones de validación (correo electrónico y contraseña), así como la clave
para `JWT` se encuentran en el archivo `appsettings.json`:

```json
"RegexPatterns": {
  "EmailPattern": "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,24}$",
  "PasswordPattern": "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"
}
```

```json
"Jwt": {
  "Key": "Clave segura para generar el Token con JWT"
}
```

Obtamos por la parametrización de los patrones de expresiones regulares en el
`appsettings.json` por motivo de simplificar el desarrollo por una
disponibilidad de tiempo. Pero estos muy bien pudieron ser a través de
configuración de la `Base de Datos` o realizar un
`Endpoint de actualización expresiones regulares` que me permitiera
actualizarlas sin la necesidad de recurrir a los recursos técnicos.

# Notas Adicionales

## Uso de ModelState

Hicimos uso del `ModelState` para ir agregando los diferentes mensajes de
errores según las validaciones y de esa manera, poder hacer una devolución de un
`List` de esos mensajes de manera simultaneo (evitando un `BadRequest` en cada
validación), evitando así que el usuario vaya superando validaciones y que sigan
apareciendo validaciones posteriores (evitando superar validación tras
validación).

## Creación Modelo APIResponse

Decidimos crear un modelo `APIResponse` con el objetivo de estandarizar la
respuesta del Endpoint desarrollado, así como de futuros posibles Endpoints, de
esta manera el usuario siempre tendrá una respuesta Homogenea.

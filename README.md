## Despliegue desde Docker Hub

Se creó el archivo `despliegue.yml` para ejecutar el backend y PostgreSQL mediante Docker Compose, utilizando la imagen publicada en Docker Hub.

**Imagen utilizada:**

`diego1hoyos/monitorservidores-api:1.0`

**Repositorio Docker Hub:**

https://hub.docker.com/r/diego1hoyos/monitorservidores-api

Para realizar el despliegue se utilizaron los siguientes comandos:

**1. Descargar las imágenes:**

```bash
docker compose --env-file .env -f despliegue.yml pull
```

**2. Ejecutar los contenedores sin compilar:**

```bash
docker compose --env-file .env -f despliegue.yml up -d --no-build
```

**3. Verificar los contenedores:**

```bash
docker compose --env-file .env -f despliegue.yml ps
```

**4. Comprobar el funcionamiento de la API:**

```bash
curl http://localhost:8080/api/reglas-alerta
```

La API respondió correctamente y devolvió información almacenada en PostgreSQL.

## Documentación con Scalar

Se integró Scalar para visualizar y probar los endpoints de la API.

La documentación está disponible en la siguiente dirección:

http://localhost:8080/scalar/

Se verificó que Scalar muestra correctamente los cinco endpoints del módulo Reglas de alerta:

- GET: Obtener todas las reglas.
- GET por ID: Obtener una regla específica.
- POST: Registrar una nueva regla.
- PUT: Actualizar una regla.
- DELETE: Eliminar una regla.

También se comprobó la consulta de registros desde Scalar y la recuperación de información almacenada en PostgreSQL.

## Uso de inteligencia artificial

Se utilizó ChatGPT como herramienta de apoyo para preparar la configuración del archivo `despliegue.yml`, integrar Scalar y actualizar la imagen publicada en Docker Hub.

Se consultó cómo configurar Docker Compose para utilizar una imagen publicada en Docker Hub, establecer la conexión entre una API .NET 10 y PostgreSQL 18, utilizar variables de entorno y conservar los datos mediante volúmenes Docker.

También se consultó cómo integrar Scalar para documentar y probar los endpoints de la API.

Las propuestas se verificaron ejecutando los comandos correspondientes en Ubuntu.

Se comprobó que Docker Compose descargó la imagen publicada, inició ambos contenedores y permitió consultar los registros de PostgreSQL mediante la API y Scalar.

Estas pruebas demostraron que el despliegue funciona sin necesidad de volver a compilar el código fuente.
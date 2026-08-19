# 💧 Agua Tracker API - Asistente e Historial de Hidratación Inteligente

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)
![MySQL](https://img.shields.io/badge/MySQL-00758F?style=for-the-badge&logo=mysql&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-Protected-black?style=for-the-badge&logo=jsonwebtokens)

API RESTful desarrollada en .NET enfocada en la gestión personalizada y cálculo automático de metas de hidratación diaria. Diseñada bajo una **Arquitectura en Capas (Layered Architecture)**, priorizando el desacoplamiento, la seguridad y las mejores prácticas de desarrollo backend.

---

## 🚀 Características Principales

- **Cálculo Fisiológico de Meta Diaria:** Algoritmo que calcula la meta recomendada de agua (en ml) según el peso, y nivel de actividad física del usuario.
- **Autenticación y Autorización Segura:** Implementación de tokens **JWT (JSON Web Tokens)** mediante middlewares de autorización para proteger endpoints privados.
- **Inyección de Dependencias Desacoplada:** Registro encapsulado de servicios y contexto de base de datos mediante *Extension Methods* para un `Program.cs` limpio.
- **Gestión de Consumo e Historial:** Registro diario de ingesta de agua con agregación de datos para la consulta de progreso en tiempo real.

---

## 🏗️ Arquitectura del Proyecto

El sistema está estructurado bajo una **Arquitectura en Capas** que separa claramente las responsabilidades:

- 📂 **`AguaTracker.Domain`**: Entidades del modelo, Enums (`NivelActividad`) y reglas de negocio puras.
- 📂 **`AguaTracker.Repository`**: Acceso a datos con Entity Framework Core, configuración de base de datos y migraciones.
- 📂 **`AguaTracker.Service`**: Servicios de aplicación y lógica principal de cálculo e hidratación diaria.
- 📂 **`AguaTracker.API`**: Controladores REST, Middlewares de autenticación y configuración general de la aplicación.

---

## 🛠️ Tecnologías Utilizadas

- **Lenguaje:** C#
- **Framework:** ASP.NET Core Web API
- **ORM:** Entity Framework Core
- **Base de Datos:** MySQL / SQL Server
- **Seguridad:** JWT (JSON Web Tokens)
- **Documentación:** Swagger / OpenAPI

---

## ⚙️ Instrucciones de Instalación y Ejecución

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/JensyMoralesYedom/Backend_AguaTracker.git
   
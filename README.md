# Yet Another Template Thing / Service

This is the backend part of **Yatt**.

But why? What is Yatt?

**Yatt** is not just a simple "Hello World". It’s a **template, reference, and showcase** for a **modern, modular full-stack architecture**.  
Built on **.NET** and **Angular**, it demonstrates use of:

- State management with **NgRx**  
- Authentication and authorization with **Keycloak**  
- API integration using **OpenAPI**  
- Modular, replaceable layers for frontend, backend, and data storage

### Docker
```powershell
docker build -f Yatt-Service/Dockerfile -t yatt-service .

docker run -p 80:80 yatt-service
```

## To Do

- [X] OpenAPI
- [X] Database
- [ ] Database Migrations
- [ ] Executables version, show in API?
- [ ] Keycloak
- [ ] Kubernetes

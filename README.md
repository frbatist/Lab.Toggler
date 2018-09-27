# Lab.Toggler
An Api to provide info about the state of a feature, and to togle this state as on/off

# Endpoints
The api provides endpoints for Application, Feature and application especific features management. 

The full endpoint documentation is in the swagger page of the api: 
http://localhost:5001/swagger

- Application: http://localhost:5001/application
```
  Provides: 
  HTTP-POST for adding new application/version. 
```
- Feature: http://localhost:5001/feature
```
  Provides: 
  HTTP-POST for adding new feature. 
  HTTP-PUT for altering a feature (enable or disalbe it).
```
- Application Feature: http://localhost:5001/application-feature
```
  Provides: 
  HTTP-POST for adding new application feature. 
  HTTP-PUT for altering a application feature (enable or disalbe it).
  HTTP-GET for getting feature data, shows if it's enabled or disabled.
  HTTP-GET/all for getting all features for the provided service.
```

# Authentication

The api uses OAuth2, currently ther's no configuration, so there's a pre defined admin user and application client as follow;

```
  Application Client:
  
  Id = Xpto.01
  Grant Types: ResourceOwnerPassword And ClientCredentials
  Secret: Xpto012018
  Allowed Scopes: "Toggler.Api", "profiles", "roles"
  
```
```
  Admin User:
  
  SubjectId = "1"
  Username = "admin"
  Password = "adminToggle2018",                  
  Role = "admin"
  
```

# TODO

- Add integration by message queue, I'ts initiated, but, not concluded;
- Users and application client dynamic configuration for Identity server.
- Docker file and docker compose;
- NoSql implementation of the repository pattern.

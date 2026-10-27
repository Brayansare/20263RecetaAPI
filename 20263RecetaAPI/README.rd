propiedad del modelo anotaciones tipo fluent [StringLength(60)]
get para obtener set para mandar
postgres DateTime get; set; anotacion de postgres UtcNow para estabdarizar datos
todas las tablas quedicen aspnet son las que genera identity framework para la autenticacion y autorizacion de usuarios

Paso 5
Crear contexto de datos
creamos la carpeta context y creamos la clase applicationDbContext.cs
por convencion se pone applicationDbContext.cs
hereda de identityDbContext para que tenga todas las tablas de identity framework

agregar el cosnstructor y hereda del constructor base(options)
sobreescribr el metodo onModelCreating para agregar las configuraciones de las entidades
primero ejecuta lo base y despue slo mio 
dentro del metodo vamos aponer todo lo relacinado con la base de datos y las entidades las relaciones uno a uno uno a muchoa y muchos a muchos
el modelo que yo llame category va hacer una una tabla en la base de datos y se va a llamar categories por convenciónfeamework singular y plural en base de datos 

Paso 6 
Cear los modelos de datos
Entity siempre nos pide que le pongamos un id para que sea la llave primaria de la tabla y con mayuscula par que lo reconozca 
Pnemos el id, nombre, descripcion y la fecha de creacion y modificacion 

Paso 7
Crear la migracion Add-Migration "InitialCreate" 
La migracion genera el documento de la base de datos y con el update-database crea las tablas en la base de datos	y no se puede modificar el documento de la base de datos a menos que se haga una nueva migracion 

Paso 8
Crear los DTOs
Hcer el dto de userCredentials oonemos email password
RegisterUser

Respuesta de autenticacion para retornar token este es el que vamos a mandar a mi front es el token expiracion usuarioid 
Paso 9
Crear controlador de autenticacion
AccountsController
crear metodo async 
hay una tabla con los claims de los usuarios identity genera una tabla de roles

register:

renew:

login: 
Aqui se configura todo l de verificacion de dos pasos validacio numero de telefono 







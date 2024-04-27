
--BD UTILIZADA PARA REALIZAR EL EXAMEN
CREATE DATABASE examenfinal;
GO
USE examenfinal;

CREATE TABLE UsuariosLogin (
    ID INT IDENTITY PRIMARY KEY,
    Usuario VARCHAR(50) UNIQUE NOT NULL,
    Contraseña VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Agentes (
    ID INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(50)
);
GO

CREATE TABLE Clientes (
    ID INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(50),
    Email VARCHAR(100),
    Telefono VARCHAR(20)
);
GO

CREATE TABLE Casas (
    ID INT IDENTITY PRIMARY KEY,
    Direccion VARCHAR(100),
    Ciudad VARCHAR(50),
    Precio DECIMAL(10, 2)
);
GO

CREATE TABLE Ventas (
    ID INT IDENTITY PRIMARY KEY,
    ID_Agente INT,
    ID_Cliente INT,
    ID_Casa INT,
    Fecha DATE,
    FOREIGN KEY (ID_Agente) REFERENCES Agentes(ID),
    FOREIGN KEY (ID_Cliente) REFERENCES Clientes(ID),
    FOREIGN KEY (ID_Casa) REFERENCES Casas(ID)
);
GO

-- Creación del procedimiento almacenado
CREATE PROCEDURE GestionarAgentes
    @accion NVARCHAR(10),
    @agente_id INT = NULL,
    @agente_nombre NVARCHAR(50) = NULL
AS
BEGIN
    IF @accion = 'agregar'
    BEGIN
        INSERT INTO Agentes (Nombre) VALUES (@agente_nombre);
    END
    ELSE IF @accion = 'borrar'
    BEGIN
        DELETE FROM Agentes WHERE ID = @agente_id;
    END
    ELSE IF @accion = 'modificar'
    BEGIN
        UPDATE Agentes SET 
            Nombre = @agente_nombre
        WHERE ID = @agente_id;
    END
    ELSE IF @accion = 'consultar'
    BEGIN
        SELECT * FROM Agentes;
    END
    ELSE
    BEGIN
        SELECT 'Acción no válida';
    END
END;
GO

-- Creación del procedimiento almacenado para autenticar el user

CREATE PROCEDURE AutenticarUsuario
    @usuario VARCHAR(50),
    @contraseña VARCHAR(100)
AS
BEGIN
    DECLARE @resultado INT;

    -- Verificar si el usuario y la contraseña coinciden
    SELECT @resultado = COUNT(*)
    FROM UsuariosLogin
    WHERE Usuario = @usuario AND Contraseña = @contraseña;

    -- Devolver el resultado de la autenticación
    IF @resultado = 1
    BEGIN
        SELECT 'Autenticación exitosa' AS Mensaje;
    END
    ELSE
    BEGIN
        SELECT 'Nombre de usuario o contraseña incorrectos' AS Mensaje;
    END
END;
GO

-- Inserción de datos y pruebas
INSERT INTO UsuariosLogin (Usuario, Contraseña) VALUES
('admin', 'admin123'),
('user1', 'password1'),
('user2', 'password2');

INSERT INTO Agentes (Nombre) VALUES
('Juan Pérez'),
('María López'),
('Carlos González');

INSERT INTO Clientes (Nombre, Email, Telefono) VALUES
('Laura Martínez', 'laura@example.com', '111-222-3333'),
('Pedro Rodríguez', 'pedro@example.com', '444-555-6666'),
('Ana García', 'ana@example.com', '777-888-9999');

INSERT INTO Casas (Direccion, Ciudad, Precio) VALUES
('Calle 123', 'Madrid', 250000.00),
('Avenida 456', 'Barcelona', 300000.00),
('Calle 789', 'Valencia', 200000.00);

INSERT INTO Ventas (ID_Agente, ID_Cliente, ID_Casa, Fecha) VALUES
(1, 1, 1, '2024-04-01'),
(2, 2, 2, '2024-04-03'),
(3, 3, 3, '2024-04-05');

-- Pruebas del procedimiento almacenado
EXEC GestionarAgentes 'agregar', NULL, 'Nuevo Agente';
EXEC GestionarAgentes 'borrar', 4;
EXEC GestionarAgentes 'modificar', 1, 'Juan Pérez Modificado';
EXEC GestionarAgentes 'consultar';


select * from agentes
select * from casas
select * from Clientes
select * from ventas
select * from UsuariosLogin
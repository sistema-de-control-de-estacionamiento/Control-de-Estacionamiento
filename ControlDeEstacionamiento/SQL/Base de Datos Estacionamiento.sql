/*
	Clase: Programacion de negocios 
	Catedratico: Ing. Hector Sabillon
	Integrantes: Jackeline Varela
				 Jorge Sanchez
	Nombre del proyeco: Estacionamiento
	Fecha: 5/7/2019
*/

USE tempdb
GO

--Creacion de la base de datos y sus tablas
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Estacionamiento')
	BEGIN
		CREATE DATABASE Estacionamiento;
	END
GO

USE Estacionamiento
GO

CREATE SCHEMA Vehiculo
GO

CREATE TABLE Vehiculo.TipoVehiculo(
	Id int identity (1,1) NOT NULL
		CONSTRAINT PK_tipovehiculo_id PRIMARY KEY CLUSTERED,
	tipo NVARCHAR(50) NOT NULL,
	
)
GO

CREATE TABLE Vehiculo.VehiculoIngresado(
	numeroPlaca NVARCHAR(7) NOT NULL 
	 CONSTRAINT PK_vehiculo_placa 
	PRIMARY KEY CLUSTERED,
	idTipoVehiculo INT NOT NULL,
	estado BIT NOT NULL,
	descripcion NVARCHAR(200)
)
GO

CREATE TABLE Vehiculo.Detalle(
	Id int identity (1,1) NOT NULL,
	horaDeIngreso DATETIME NOT NULL,
	horaSalida DATETIME,
	totalCobro decimal,
	numeroPlacaVehiculo NVARCHAR(7) NOT NULL
)
GO

--Relaciones
ALTER TABLE Vehiculo.VehiculoIngresado
	ADD CONSTRAINT
		FK_Vehiculo_VehiculoIngresado$EsUn$Tipo_Vehiculo
		FOREIGN KEY (idTipoVehiculo) REFERENCES Vehiculo.TipoVehiculo(id)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION
GO

ALTER TABLE Vehiculo.Detalle
	ADD CONSTRAINT 
		FK_Vehiculo_SalidaVehiculo$TieneUna$Vehiculo_Ingresado_Placa
		FOREIGN KEY (numeroPlacaVehiculo) REFERENCES Vehiculo.VehiculoIngresado(numeroPlaca)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION
GO


--Agregar los tipos de vehiculo
INSERT INTO Vehiculo.TipoVehiculo (tipo)
VALUES ('Turismo'),
	   ('Pick-up'),
	   ('Camioneta'),
	   ('Cami�n'),
	   ('Bus'),
	   ('Rastra'),
	   ('Motocicleta')
GO


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
		CREATE DATABASE Estacionamiento
	END

USE Estacionamiento
GO

CREATE SCHEMA Vehiculo
GO

CREATE TABLE Vehiculo.VehiculoIngresado(
	numeroPlaca NVARCHAR(7) NOT NULL
		CONSTRAINT PK_vehiculo_placa PRIMARY KEY CLUSTERED,
	horaDeIngreso DATETIME,
	tipoVehiculo NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE Vehiculo.TipoVehiculo(
	Id int identity (1,1) NOT NULL
		CONSTRAINT PK_tipovehiculo_id PRIMARY KEY CLUSTERED,
	tipo NVARCHAR(50) NOT NULL,
	modelo NVARCHAR(50) NOT NULL,
	descripcion NVARCHAR(150)
)

CREATE TABLE Vehiculo.SalidaVehiculo(
	IdSalida int identity (1,1) NOT NULL,
	placa NVARCHAR(7) NOT NULL,
	tipoVehiculo NVARCHAR(50) NOT NULL,
	horaIngreso DATETIME NOT NULL,
	horaSalida DATETIME NOT NULL,
	totalCobro INT NOT NULL
)
GO

--Relaciones
ALTER TABLE Vehiculo.VehiculoIngresado
	ADD CONSTRAINT
		FK_Vehiculo_VehiculoIngresado$EsUn$Tipo_Vehiculo
		FOREIGN KEY (tipoVehiculo) REFERENCES Vehiculo.TipoVehiculo(tipo)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO

ALTER TABLE Vehiculo.SalidaVehiculo
	ADD CONSTRAINT 
		FK_Vehiculo_SalidaVehiculo$TieneUna$Vehiculo_Ingresado_Placa
		FOREIGN KEY (placa) REFERENCES Vehiculo.VehiculoIngresado(numeroPlaca)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO

ALTER TABLE Vehiculo.SalidaVehiculo
	ADD CONSTRAINT 
		FK_Vehiculo_SalidaVehiculo$EsUn$Tipo_Vehiculo
		FOREIGN KEY (tipoVehiculo) REFERENCES Vehiculo.TipoVehiculo(tipo)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO

ALTER TABLE Vehiculo.SalidaVehiculo
	ADD CONSTRAINT 
		FK_Vehiculo_SalidaVehiculo$TieneUna$Vehiculo_Ingresado_Hora_Ingreso
		FOREIGN KEY (HoraIngreso) REFERENCES Vehiculo.VehiculoIngresado(horaDeIngreso)
		ON UPDATE CASCADE
		ON DELETE NO ACTION
GO
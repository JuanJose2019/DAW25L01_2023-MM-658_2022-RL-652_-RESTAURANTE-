Create database restauranteDB;

use restauranteDB;

create table clientes (
clienteId int primary key identity,
nombreCliente varchar(200) Not null,
direccion varchar(500) not null
);

create table motoristas (
motoristaId int primary key identity,
nombreMotorista varchar(200) Not null
);

create table platos (
platoId int primary key identity,
nombrePlato varchar(200) Not null,
precio numeric(18,4) Not null
);

create table pedidos (
pedidoId int primary key identity,
motoristaId int Not null,
clienteId int Not null,
platoId int Not null,
cantidad int Not null,
precio numeric(18,4) Not null,

FOREIGN KEY (motoristaId) REFERENCES motoristas(motoristaId) ON DELETE CASCADE,
FOREIGN KEY (clienteId) REFERENCES clientes(clienteId) ON DELETE CASCADE,
FOREIGN KEY (platoId) REFERENCES platos(platoId) ON DELETE CASCADE,
);



INSERT INTO clientes (nombreCliente, direccion) VALUES 
('Raul Pérez', 'Calle 123, Ciudad'),
('María López', 'Avenida Central 456, Ciudad');


INSERT INTO motoristas (nombreMotorista) VALUES 
('Carlos González'),
('Ana Ramírez');


INSERT INTO platos (nombrePlato, precio) VALUES 
('Pizza Margarita', 8.99),
('Hamburguesa Clásica', 5.50),
('Sopa', 12.30);


INSERT INTO pedidos (motoristaId, clienteId, platoId, cantidad, precio) VALUES 
(1, 1, 1, 2, 17.98),
(2, 2, 3, 1, 12.30);











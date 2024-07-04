-- Create the GaVietNam database
USE master;
GO

CREATE DATABASE GaVietNam;
GO

USE GaVietNam;
GO

-- Table Role
CREATE TABLE Role (
    id BIGINT PRIMARY KEY IDENTITY,
    role_name VARCHAR(50) NOT NULL UNIQUE
);

-- Table Chicken
CREATE TABLE Chicken (
    id BIGINT PRIMARY KEY IDENTITY,
    name VARCHAR(255) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    stock INT NOT NULL,
    createDate DATETIME,
    modifiedDate DATETIME,
    status bit
);

-- Table Kind
CREATE TABLE Kind (
    id BIGINT PRIMARY KEY IDENTITY,
    kindName VARCHAR(50) NOT NULL,
    image VARCHAR(255),
    quantity INT NOT NULL,
    status bit,
    chicken_id BIGINT,
    FOREIGN KEY (chicken_id) REFERENCES Chicken(id)
);

-- Table User
CREATE TABLE [User] (
    id BIGINT PRIMARY KEY IDENTITY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    identity_card VARCHAR(20) NOT NULL UNIQUE,
    dob DATE NOT NULL,
    phone VARCHAR(15) NOT NULL,
    create_date DATE DEFAULT GETDATE(),
    status VARCHAR(50) NOT NULL,
    role_id BIGINT,
    FOREIGN KEY (role_id) REFERENCES Role(id)
);

-- Table Admin
CREATE TABLE Admin (
    id BIGINT PRIMARY KEY IDENTITY,
    user_id BIGINT,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES [User](id)
);

-- Table Order
CREATE TABLE [Order] (
    id BIGINT PRIMARY KEY IDENTITY,
    userId BIGINT,
    adminId BIGINT,
    orderRequirement VARCHAR(255),
    orderCode VARCHAR(255),
    paymentMethod VARCHAR(255),
    createDate DATETIME,
    totalPrice DECIMAL(10, 2),
    status VARCHAR(255),
    FOREIGN KEY (userId) REFERENCES [User](id),
    FOREIGN KEY (adminId) REFERENCES Admin(id)
);

-- Table OrderItem
CREATE TABLE OrderItem (
    id BIGINT PRIMARY KEY IDENTITY,
    order_id BIGINT,
    chicken_id BIGINT,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES [Order](id),
    FOREIGN KEY (chicken_id) REFERENCES Chicken(id)
);

-- Table Contact
CREATE TABLE Contact (
    id BIGINT PRIMARY KEY IDENTITY,
    phone VARCHAR(50),
    email VARCHAR(255),
    facebook VARCHAR(255),
    instagram VARCHAR(255),
    tiktok VARCHAR(255),
    shoppee VARCHAR(255)
);

-- Table Bill
CREATE TABLE Bill (
    id BIGINT PRIMARY KEY IDENTITY,
    order_id BIGINT,
    status VARCHAR(255),
    FOREIGN KEY (order_id) REFERENCES [Order](id)
);

-- Insert sample data into Role table
INSERT INTO Role (role_name) VALUES ('Admin');
INSERT INTO Role (role_name) VALUES ('User');

-- Insert sample data into Chicken table
INSERT INTO Chicken (name, price, stock, createDate, modifiedDate, status)
VALUES ('Whole Regular Chicken', 15.99, 50, GETDATE(), GETDATE(), 1);

INSERT INTO Chicken (name, price, stock, createDate, modifiedDate, status)
VALUES ('Half Premium Chicken', 12.99, 30, GETDATE(), GETDATE(), 1);

-- Insert sample data into Kind table
INSERT INTO Kind (kindName, image, quantity, status, chicken_id)
VALUES ('Regular', 'regular.jpg', 100, 1, 1);

INSERT INTO Kind (kindName, image, quantity, status, chicken_id)
VALUES ('Premium', 'premium.jpg', 50, 1, 2);

-- Insert sample data into User table
INSERT INTO [User] (username, password, identity_card, dob, phone, create_date, status, role_id)
VALUES ('admin_user', 'password123', '1234567890', '1990-01-01', '123456789', GETDATE(), 'Active', 1);

INSERT INTO [User] (username, password, identity_card, dob, phone, create_date, status, role_id)
VALUES ('regular_user', 'password456', '0987654321', '1995-05-05', '987654321', GETDATE(), 'Active', 2);

-- Insert sample data into Admin table
INSERT INTO Admin (user_id, username, password)
VALUES (1, 'admin', 'adminpassword');

-- Insert sample data into Order table
INSERT INTO [Order] (userId, adminId, orderRequirement, orderCode, paymentMethod, createDate, totalPrice, status)
VALUES (2, 1, 'Delivery to home', 'ORD123456', 'Credit Card', GETDATE(), 28.50, 'Pending');

-- Insert sample data into OrderItem table
INSERT INTO OrderItem (order_id, chicken_id, quantity, price)
VALUES (1, 1, 1, 15.99);

-- Insert sample data into Contact table
INSERT INTO Contact (phone, email, facebook, instagram, tiktok, shoppee)
VALUES ('123456789', 'contact@example.com', 'facebook.com/example', 'instagram.com/example', 'tiktok.com/example', 'shopee.com/example');

-- Insert sample data into Bill table
INSERT INTO Bill (order_id, status)
VALUES (1, 'Paid');

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Bakery')
Create database Bakery;
use Bakery;

CREATE TABLE Products (
    id INT PRIMARY KEY IDENTITY(1,1),
    pName NVARCHAR(50),
    ptype NVARCHAR(100),
    pricePerUnit decimal,
	outputVolume int,
	shelflife date
);

CREATE TABLE Ingredients (
    id INT PRIMARY KEY IDENTITY(1,1),
    iName NVARCHAR(50),
    unitOfMeasurement NVARCHAR(50),
    pricePerUnit decimal,
    availableQuantity int
);

CREATE TABLE ProductСomposition (
    id INT PRIMARY KEY IDENTITY(1,1),
    product_id int,
    ingredient_id int,
    FOREIGN KEY (product_id) REFERENCES Products(id),
    FOREIGN KEY (ingredient_id) REFERENCES Ingredients(id)
);

CREATE TABLE Suppliers (
    id INT PRIMARY KEY IDENTITY(1,1),
    sName NVARCHAR(100),
    contactInformation NVARCHAR(100)
);

CREATE TABLE suppliedIngredients (
    id INT PRIMARY KEY IDENTITY(1,1),
    supplier_id int,
    ingredient_id int,
	ingredient_quntity int,
	price decimal,
	FOREIGN KEY (supplier_id) REFERENCES Suppliers(id),
    FOREIGN KEY (ingredient_id) REFERENCES Ingredients(id)
);

CREATE TABLE Orders (
    id INT PRIMARY KEY IDENTITY(1,1),
    orderDate DATETIME,
    condition NVARCHAR(50)
);

CREATE TABLE OrderedProducts(
    id INT PRIMARY KEY IDENTITY(1,1),
    order_id int,
    product_id int,
    quantityOfProducts int,
	price decimal,
	FOREIGN KEY (order_id) REFERENCES Orders(id),
    FOREIGN KEY (product_id) REFERENCES Products(id)
);

CREATE TABLE Roles (
    id INT PRIMARY KEY IDENTITY(1,1),
    title_of_role NVARCHAR(50),
    role_email NVARCHAR(100),
    role_password NVARCHAR(100)
);
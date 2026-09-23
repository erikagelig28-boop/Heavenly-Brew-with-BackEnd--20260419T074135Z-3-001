-- ============================================
-- CAPSTONE EXERCISE
-- GENERATING PROFESSIONAL REPORTS
-- MYSQL
-- ============================================

-- ============================================
-- EXERCISE 1: STUDENT ENROLLMENT REPORT
-- ============================================

DROP TABLE IF EXISTS enrollments;
DROP TABLE IF EXISTS courses;
DROP TABLE IF EXISTS students;

CREATE TABLE students (
    student_id INT PRIMARY KEY,
    student_name VARCHAR(50),
    email VARCHAR(50)
);

CREATE TABLE courses (
    course_id INT PRIMARY KEY,
    course_name VARCHAR(50)
);

CREATE TABLE enrollments (
    enrollment_id INT PRIMARY KEY,
    student_id INT,
    course_id INT,
    enrollment_date DATE,
    FOREIGN KEY (student_id) REFERENCES students(student_id),
    FOREIGN KEY (course_id) REFERENCES courses(course_id)
);

INSERT INTO students VALUES
(1, 'Erika Mae', 'erika@gmail.com'),
(2, 'Maria Santos', 'maria@gmail.com'),
(3, 'John Cruz', 'john@gmail.com'),
(4, 'Angela Reyes', 'angela@gmail.com');

INSERT INTO courses VALUES
(101, 'Information Technology'),
(102, 'Nursing'),
(103, 'Business Administration'),
(104, 'Computer Science');

INSERT INTO enrollments VALUES
(1, 1, 101, '2026-06-10'),
(2, 2, 102, '2026-06-11'),
(3, 3, 103, '2026-06-12'),
(4, 4, 104, '2026-06-13'),
(5, 1, 103, '2026-06-14');

SELECT
    students.student_id,
    students.student_name,
    courses.course_name,
    enrollments.enrollment_date
FROM students
INNER JOIN enrollments
ON students.student_id = enrollments.student_id
INNER JOIN courses
ON enrollments.course_id = courses.course_id
ORDER BY enrollments.enrollment_date;


-- ============================================
-- EXERCISE 2: SALES REPORT
-- ============================================

DROP TABLE IF EXISTS order_items;
DROP TABLE IF EXISTS orders;
DROP TABLE IF EXISTS products;
DROP TABLE IF EXISTS customers;

CREATE TABLE customers (
    customer_id INT PRIMARY KEY,
    customer_name VARCHAR(50),
    address VARCHAR(50)
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(50),
    price DECIMAL(10,2)
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_date DATE,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

INSERT INTO customers VALUES
(1, 'Erika Mae', 'Cebu City'),
(2, 'Maria Santos', 'Lapu-Lapu'),
(3, 'John Cruz', 'Mandaue');

INSERT INTO products VALUES
(101, 'Coffee', 80.00),
(102, 'Burger', 120.00),
(103, 'Milk Tea', 100.00),
(104, 'French Fries', 70.00);

INSERT INTO orders VALUES
(1001, 1, '2026-09-20'),
(1002, 2, '2026-09-21'),
(1003, 3, '2026-09-22');

INSERT INTO order_items VALUES
(1, 1001, 101, 2),
(2, 1001, 104, 1),
(3, 1002, 102, 2),
(4, 1002, 103, 1),
(5, 1003, 103, 2);

SELECT
    customers.customer_name,
    orders.order_id,
    orders.order_date,
    products.product_name,
    order_items.quantity,
    products.price,
    order_items.quantity * products.price AS total_amount
FROM customers
INNER JOIN orders
ON customers.customer_id = orders.customer_id
INNER JOIN order_items
ON orders.order_id = order_items.order_id
INNER JOIN products
ON order_items.product_id = products.product_id
ORDER BY orders.order_date;

SELECT
    SUM(order_items.quantity * products.price) AS total_revenue
FROM orders
INNER JOIN order_items
ON orders.order_id = order_items.order_id
INNER JOIN products
ON order_items.product_id = products.product_id;


-- ============================================
-- EXERCISE 3: EMPLOYEE ASSIGNMENT REPORT
-- ============================================

DROP TABLE IF EXISTS employee_assignments;
DROP TABLE IF EXISTS project_tasks;
DROP TABLE IF EXISTS projects;
DROP TABLE IF EXISTS employees;
DROP TABLE IF EXISTS departments;

CREATE TABLE departments (
    department_id INT PRIMARY KEY,
    department_name VARCHAR(50)
);

CREATE TABLE employees (
    employee_id INT PRIMARY KEY,
    employee_name VARCHAR(50),
    department_id INT,
    FOREIGN KEY (department_id) REFERENCES departments(department_id)
);

CREATE TABLE projects (
    project_id INT PRIMARY KEY,
    project_name VARCHAR(50)
);

CREATE TABLE project_tasks (
    task_id INT PRIMARY KEY,
    project_id INT,
    task_name VARCHAR(50),
    FOREIGN KEY (project_id) REFERENCES projects(project_id)
);

CREATE TABLE employee_assignments (
    assignment_id INT PRIMARY KEY,
    employee_id INT,
    task_id INT,
    assignment_date DATE,
    FOREIGN KEY (employee_id) REFERENCES employees(employee_id),
    FOREIGN KEY (task_id) REFERENCES project_tasks(task_id)
);

INSERT INTO departments VALUES
(1, 'IT Department'),
(2, 'Marketing'),
(3, 'Accounting');

INSERT INTO employees VALUES
(101, 'Erika Mae', 1),
(102, 'Maria Santos', 2),
(103, 'John Cruz', 3),
(104, 'Angela Reyes', 1);

INSERT INTO projects VALUES
(201, 'Website Project'),
(202, 'Marketing Project'),
(203, 'Accounting Project');

INSERT INTO project_tasks VALUES
(301, 201, 'Design Website'),
(302, 201, 'Test Website'),
(303, 202, 'Create Poster'),
(304, 203, 'Prepare Report');

INSERT INTO employee_assignments VALUES
(1, 101, 301, '2026-09-20'),
(2, 104, 302, '2026-09-21'),
(3, 102, 303, '2026-09-21'),
(4, 103, 304, '2026-09-22');

SELECT
    employees.employee_id,
    employees.employee_name,
    departments.department_name,
    projects.project_name,
    project_tasks.task_name,
    employee_assignments.assignment_date
FROM employees
INNER JOIN departments
ON employees.department_id = departments.department_id
INNER JOIN employee_assignments
ON employees.employee_id = employee_assignments.employee_id
INNER JOIN project_tasks
ON employee_assignments.task_id = project_tasks.task_id
INNER JOIN projects
ON project_tasks.project_id = projects.project_id
ORDER BY departments.department_name;

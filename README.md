# LedMagazineBack

A backend API for managing magazine content, users, orders, rentals, and more.

---

## API Endpoints

Below are the main REST endpoints exposed by this backend.  
**Note**: Some endpoints require authentication and specific user roles (`admin`, `customer`, `guest`).  
For full controller details, see the [Controllers directory](https://github.com/Roman1488e/LedMagazineBack/tree/main/LedMagazineBack/Controllers).

---

### 🛒 Cart

| Method | Endpoint                         | Description                          | Auth/Role            |
|--------|----------------------------------|--------------------------------------|----------------------|
| GET    | `/api/carts`                     | Get all carts                        |                      |
| GET    | `/api/carts/by-id/{id}`          | Get cart by ID                       | admin                |
| GET    | `/api/carts/by-session-id/`      | Get cart by session ID               | guest                |
| GET    | `/api/carts/by-customer-id/`     | Get cart by customer ID              | admin, customer      |
| DELETE | `/api/carts/{id}`                | Delete cart by ID                    | admin                |

---

### 🛍️ Cart Items

| Method | Endpoint                              | Description               | Auth/Role   |
|--------|---------------------------------------|---------------------------|-------------|
| GET    | `/api/cartItems`                      | Get all cart items        | admin       |
| GET    | `/api/cartItems/{id}`                 | Get cart item by ID       | admin       |
| POST   | `/api/cartItems`                      | Create cart item          | authenticated |
| GET    | `/api/cartItems/by-cartId/{id}`       | Get items by cart ID      | guest       |

---

### 👤 Customers

| Method | Endpoint                                 | Description                     | Auth/Role             |
|--------|------------------------------------------|---------------------------------|-----------------------|
| GET    | `/api/customers`                         | Get all customers               | admin                 |
| GET    | `/api/customers/by-id/{id}`              | Get customer by ID              | admin                 |
| POST   | `/api/customers/by-username/{username}`  | Get customer by username        | admin                 |
| POST   | `/api/customers/register`                | Register as customer            | guest                 |
| PUT    | `/api/customers/change-password`         | Change customer password        | admin, customer       |
| PUT    | `/api/customers/change-number`           | Change customer number          | admin, customer       |
| PUT    | `/api/customers/change-name`             | Change customer name            | admin, customer       |
| PUT    | `/api/customers/change-username`         | Change customer username        | admin, customer       |
| DELETE | `/api/customers/{id}`                    | Delete customer account         | admin, customer       |

---

### 👥 Guests

| Method | Endpoint                                  | Description                  | Auth/Role   |
|--------|-------------------------------------------|------------------------------|-------------|
| POST   | `/api/guests/generate`                    | Generate new guest           |             |
| GET    | `/api/guests`                             | Get all guests               |             |
| GET    | `/api/guests/by-id/{id}`                  | Get guest by ID              |             |
| GET    | `/api/guests/by-sessionId/{sessionId}`    | Get guest by session ID      |             |
| DELETE | `/api/guests/{sessionId}`                 | Delete guest by session ID   | admin       |

---

### 📦 Products

| Method | Endpoint                                  | Description                       | Auth/Role   |
|--------|-------------------------------------------|-----------------------------------|-------------|
| GET    | `/api/products`                           | Get all products                  | authenticated |
| GET    | `/api/products/{id}`                      | Get product by ID                 | authenticated |
| POST   | `/api/products`                           | Create a product                  | admin        |
| DELETE | `/api/products/{id}`                      | Delete product                    | admin        |
| PUT    | `/api/products/{id}/general-info`         | Update general product info       | admin        |
| PUT    | `/api/products/{id}/image`                | Update product image              | admin        |
| PUT    | `/api/products/{id}/video`                | Update product video              | admin        |
| PUT    | `/api/products/{id}/price`                | Update product price              | admin        |
| PUT    | `/api/products/{id}/isActive`             | Set product active/inactive       | admin        |

---

### 📑 Orders

| Method | Endpoint                                 | Description                        | Auth/Role   |
|--------|------------------------------------------|------------------------------------|-------------|
| GET    | `/api/orders`                            | Get all orders                     | admin       |
| GET    | `/api/orders/{id}`                       | Get order by ID                    | admin       |
| POST   | `/api/orders`                            | Create order                       | authenticated |
| PUT    | `/api/orders/{id}`                       | Update order                       | authenticated |
| DELETE | `/api/orders/{id}`                       | Delete order                       | authenticated |
| PUT    | `/api/orders/{id}/accept`                | Accept order                       | authenticated |
| PUT    | `/api/orders/{id}/cancel`                | Cancel order                       | authenticated |

---

### 📥 Order Items

| Method | Endpoint                                    | Description                   | Auth/Role   |
|--------|---------------------------------------------|-------------------------------|-------------|
| GET    | `/api/orderitem`                            | Get all order items           | admin       |
| GET    | `/api/orderitem/{id}`                       | Get order item by ID          | admin       |
| GET    | `/api/orderitem/by-orderid/{id}`            | Get items by order ID         | authenticated |
| GET    | `/api/orderitem.by-productname/{name}`      | Get order item by product name| admin       |
| DELETE | `/api/orderitem/{id}`                       | Delete order item by ID       | admin       |

---

### 🖥️ Screen Specifications

| Method | Endpoint                                            | Description                       | Auth/Role   |
|--------|-----------------------------------------------------|-----------------------------------|-------------|
| GET    | `/api/screen-specifications`                        | Get all screen specifications     | admin       |
| GET    | `/api/screen-specifications/by-id/{id}`             | Get screen specification by ID    | admin       |
| POST   | `/api/screen-specifications/by-product-id/{productId}`| Get by product ID               | authenticated |
| POST   | `/api/screen-specifications/create`                 | Create screen specification       | admin       |
| PUT    | `/api/screen-specifications/{id}/update`            | Update screen specification       | admin       |
| DELETE | `/api/screen-specifications/{id}`                   | Delete screen specification       | admin       |

---

## 🔒 Authentication

- Many endpoints require a bearer JWT token in the `Authorization` header.
- Some endpoints are restricted by user role (`admin`, `customer`, `guest`).

## 📚 More Information

- Browse all controllers: [Controllers directory](https://github.com/Roman1488e/LedMagazineBack/tree/main/LedMagazineBack/Controllers)
- For request/response models, see the `Models` folder in the repository.
- For database structure, see the `Entities` and `Context` folders.

---

## 📝 License

Specify your license here (e.g., MIT, Apache 2.0, etc.).

---

> _Want to contribute? Fork the repo, make your changes, and submit a pull request!_

# LedMagazineBack

## Важно! Авторизация обязательна для всех запросов

**Все эндпоинты, кроме `/api/guests/generate`, требуют наличия корректного JWT токена в заголовке Authorization в формате `Bearer {token}`.**
Без этого токена любой запрос получит ошибку 403 Forbidden.

---

## Инструкция для фронтендера

### 1. Получение гостевого токена

Для любого пользователя (в том числе анонимного гостя) первым шагом всегда должен быть запрос на генерацию гостевого токена.

**Запрос:**
```http
POST /api/guests/generate
```

**Ответ:**
```json
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

Это строка — и есть JWT токен.

---

### 2. Использование токена

Все последующие запросы к API (кроме `/api/guests/generate`) должны содержать в заголовке:

```
Authorization: Bearer {ваш_токен}
```

**ВАЖНО:**  
- Обязательно используйте префикс `Bearer`, затем пробел и только потом сам токен.
- Без этого заголовка или с неверным токеном будет ошибка 403.

---

### 3. Пример полного сценария

```http
POST /api/guests/generate
```
**-> в ответе получили токен**

```http
GET /api/products
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```
**-> получаете список продуктов**

---

## Доступные эндпоинты (полный список)

### Гости
| Метод | URL                              | Авторизация | Описание                                   |
|-------|----------------------------------|-------------|---------------------------------------------|
| POST  | `/api/guests/generate`           | —           | Получить гостевой JWT токен                 |
| GET   | `/api/guests`                    | guest/admin | Получить список гостей                      |
| GET   | `/api/guests/by-id/{id}`         | guest/admin | Получить гостя по id                        |
| GET   | `/api/guests/by-sessionId/{sessionId}` | guest/admin | Получить гостя по sessionId           |
| DELETE| `/api/guests/{sessionId}`        | admin       | Удалить гостя по sessionId                  |

---

### Продукты
| Метод | URL                                | Авторизация          | Описание                              |
|-------|------------------------------------|----------------------|---------------------------------------|
| GET   | `/api/products`                    | guest/customer/admin | Получить список всех продуктов        |
| GET   | `/api/products/{id}`               | guest/customer/admin | Получить продукт по id                |
| POST  | `/api/products`                    | admin                | Создать продукт                       |
| DELETE| `/api/products/{id}`               | admin                | Удалить продукт                       |
| PUT   | `/api/products/{id}/general-info`  | admin                | Обновить общую информацию о продукте  |
| PUT   | `/api/products/{id}/image`         | admin                | Обновить изображение продукта         |
| PUT   | `/api/products/{id}/video`         | admin                | Обновить видео продукта               |
| PUT   | `/api/products/{id}/price`         | admin                | Обновить цену продукта                |
| PUT   | `/api/products/{id}/isActive`      | admin                | Активировать/деактивировать продукт   |

---

### Корзины
| Метод | URL                                | Авторизация          | Описание                              |
|-------|------------------------------------|----------------------|---------------------------------------|
| GET   | `/api/carts`                       | guest/customer/admin | Получить все корзины                  |
| GET   | `/api/carts/by-id/{id}`            | admin                | Получить корзину по id                |
| GET   | `/api/carts/by-session-id/`        | guest                | Получить корзину по sessionId         |
| GET   | `/api/carts/by-customer-id/`       | admin/customer       | Получить корзину по customerId        |
| DELETE| `/api/carts/{id}`                  | admin                | Удалить корзину                       |

---

### Элементы корзины
| Метод | URL                                | Авторизация          | Описание                              |
|-------|------------------------------------|----------------------|---------------------------------------|
| GET   | `/api/cartItems`                   | admin                | Получить все элементы корзин          |
| GET   | `/api/cartItems/{id}`              | admin                | Получить элемент корзины по id        |
| POST  | `/api/cartItems`                   | guest/customer/admin | Добавить элемент в корзину            |
| GET   | `/api/cartItems/by-cartId/{id}`    | guest                | Получить элементы по id корзины       |

---

### Пользователи (Customers)
| Метод | URL                                | Авторизация          | Описание                              |
|-------|------------------------------------|----------------------|---------------------------------------|
| GET   | `/api/customers`                   | admin                | Получить всех пользователей           |
| GET   | `/api/customers/by-id/{id}`        | admin                | Получить пользователя по id           |
| POST  | `/api/customers/by-username/{username}` | admin          | Получить пользователя по username     |
| POST  | `/api/customers/register`          | guest                | Зарегистрировать нового пользователя  |
| PUT   | `/api/customers/change-password`   | admin/customer       | Сменить пароль                        |
| PUT   | `/api/customers/change-number`     | admin/customer       | Сменить номер телефона                |
| PUT   | `/api/customers/change-name`       | admin/customer       | Сменить имя пользователя              |
| PUT   | `/api/customers/change-username`   | admin/customer       | Сменить username                      |
| DELETE| `/api/customers/{id}`              | admin/customer       | Удалить пользователя по id            |

---

### Заказы (Orders)
| Метод | URL                                    | Авторизация           | Описание                                       |
|-------|----------------------------------------|-----------------------|------------------------------------------------|
| GET   | `/api/orders`                          | admin                 | Получить все заказы                            |
| GET   | `/api/orders/{id}`                     | admin                 | Получить заказ по id                            |
| POST  | `/api/orders/from-cart-users`          | admin/customer        | Создать заказ из корзины (для пользователя)     |
| POST  | `/api/orders/from-cart-guests`         | guest                 | Создать заказ из корзины (для гостя)            |
| POST  | `/api/orders/for-guests`               | guest                 | Создать заказ для гостя                         |
| POST  | `/api/orders/for-users`                | customer/admin        | Создать заказ для пользователя                  |
| PUT   | `/api/orders/{id}`                     | authenticated         | Изменить заказ                                  |
| DELETE| `/api/orders/{id}`                     | authenticated         | Удалить заказ                                   |
| PUT   | `/api/orders/{id}/accept`              | authenticated         | Принять заказ                                   |

---

### Элементы заказа (Order Items)
| Метод | URL                                    | Авторизация           | Описание                                       |
|-------|----------------------------------------|-----------------------|------------------------------------------------|
| GET   | `/api/orderitem`                       | admin                 | Получить все элементы заказа                    |
| GET   | `/api/orderitem/{id}`                  | admin                 | Получить элемент заказа по id                   |
| GET   | `/api/orderitem/by-orderid/{id}`       | authenticated         | Получить элементы заказа по id заказа           |
| GET   | `/api/orderitem.by-productname/{name}` | admin                 | Получить элемент заказа по названию продукта    |
| DELETE| `/api/orderitem/{id}`                  | admin                 | Удалить элемент заказа по id                    |
| POST  | `/api/orderitem`                       | authenticated         | Добавить элемент заказа                         |

---

### Время аренды (Rent Time)
| Метод | URL                                    | Авторизация           | Описание                                       |
|-------|----------------------------------------|-----------------------|------------------------------------------------|
| GET   | `/api/rent-times`                      | admin                 | Получить все параметры аренды                   |
| PUT   | `/api/rent-times`                      | admin                 | Изменить параметры аренды                       |
| GET   | `/api/rent-time/by-id/{id}`            | admin                 | Получить параметры аренды по id                 |
| DELETE| `/api/rent-time/{id}`                  | admin                 | Удалить параметры аренды                        |
| GET   | `/api/rent-time/by-orderItemid/{id}`   | authenticated         | Получить параметры аренды по id элемента заказа |
| GET   | `/api/rent-time/by-cartItemId/{id}`    | authenticated         | Получить параметры аренды по id элемента корзины|

---

### Мультипликаторы времени аренды (Rent Time Multiplayer)
| Метод | URL                                    | Авторизация           | Описание                                       |
|-------|----------------------------------------|-----------------------|------------------------------------------------|
| GET   | `/api/rent-time-multiplayers`          | admin                 | Получить все мультипликаторы                    |
| POST  | `/api/rent-time-multiplayers`          | admin                 | Создать мультипликатор                          |
| PUT   | `/api/rent-time-multiplayers`          | admin                 | Изменить мультипликатор                         |
| DELETE| `/api/rent-time-multiplayers/{id}`     | admin                 | Удалить мультипликатор по id                    |
| GET   | `/api/rent-time-multiplayers/by-productId/{id}` | admin         | Получить по productId                           |
| GET   | `/api/rent-time-multiplayers/by-Id/{id}`        | admin         | Получить по id                                  |

---

### Локации (Locations)
| Метод | URL                                    | Авторизация           | Описание                                       |
|-------|----------------------------------------|-----------------------|------------------------------------------------|
| GET   | `/api/locations`                       | admin                 | Получить все локации                            |
| GET   | `/api/locations/by-id/{id}`            | admin                 | Получить локацию по id                          |
| POST  | `/api/locations`                       | admin                 | Создать локацию                                 |
| DELETE| `/api/locations/{id}`                  | admin                 | Удалить локацию                                 |
| PUT   | `/api/locations/{id}`                  | admin                 | Изменить локацию                                |
| GET   | `/api/locations/by-productId/{id}`     | admin                 | Получить локации по productId                   |

---

### Характеристики экрана (Screen Specifications)
| Метод | URL                                    | Авторизация           | Описание                                       |
|-------|----------------------------------------|-----------------------|------------------------------------------------|
| GET   | `/api/screen-specifications`           | admin                 | Получить все характеристики экранов             |
| GET   | `/api/screen-specifications/by-id/{id}`| admin                 | Получить характеристики по id                   |
| GET   | `/api/screen-specifications/by-product-id/{productId}` | authenticated | Получить по productId         |
| POST  | `/api/screen-specifications`           | admin                 | Создать характеристики экрана                   |
| PUT   | `/api/screen-specifications/{id}`      | admin                 | Изменить характеристики экрана                  |
| DELETE| `/api/screen-specifications/{id}`      | admin                 | Удалить характеристики экрана                   |

---

### Блог (Blog)
| Метод | URL                                    | Авторизация           | Описание                                       |
|-------|----------------------------------------|-----------------------|------------------------------------------------|
| GET   | `/api/blogs`                           | authenticated         | Получить все блоги                              |
| GET   | `/api/blogs/{id}`                      | authenticated         | Получить блог по id                             |
| POST  | `/api/blogs`                           | admin                 | Создать блог                                    |
| DELETE| `/api/blogs/{id}`                      | admin                 | Удалить блог по id                              |

---

## JWT и авторизация

- Все защищённые эндпоинты требуют JWT-токен в заголовке `Authorization` в формате `Bearer {token}`.
- Токены генерируются для гостей и пользователей отдельно.
- Время жизни токена может быть ограничено (см. настройки сервера).
- Параметры JWT (Issuer, Audience, Key) настраиваются в `appsettings.json`:

```json
"JwtParameters": {
  "Issuer": "Magazine.Api",
  "Audience": "Magazine.Client",
  "Key": "qwertyuiopasdfgh1234567890"
}
```

---

## FAQ

**Q: Нужно ли генерировать токен для каждого пользователя?**  
A: Да, даже для гостя. Без токена API недоступно.

**Q: Можно ли использовать один токен долго?**  
A: Токен имеет ограниченное время жизни, если получаете 401 — запрашивайте новый через `/api/guests/generate`.

**Q: Как использовать токен для авторизации?**  
A: Добавляйте заголовок Authorization в каждый запрос, кроме первого на `/api/guests/generate`.

---

## Контакты

Если есть вопросы по интеграции — обращайтесь к бэкендеру.
Телеграм @greatpool

---

> **Сначала всегда делай запрос на `/api/guests/generate`, иначе ни один другой эндпоинт не будет работать!**
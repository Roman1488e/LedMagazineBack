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
## Интеграция с Telegram для уведомлений

### Конфигурация Telegram-бота

В файле `appsettings.json` для отправки уведомлений через Telegram используется следующий блок настроек:

```json
"TelegramSettings": {
  "BotToken": "8189559523:AAGYnnD3GCUWM3a8FRtVENBAybI021dRj14",
  "ChatIds": [ "527422045", "1271625094" ]
}
```

- **BotToken** — токен Telegram-бота.
- **ChatIds** — список идентификаторов чатов (обычно — Telegram user ID или group ID), куда будут отправляться уведомления о заказах и других событиях.

> Если нужно добавить получателей уведомлений — просто добавьте их chat ID в массив.

---

## Аккаунт администратора по умолчанию

В проекте предусмотрен seed-аккаунт администратора для начального доступа.

**Данные для входа:**

- **Логин:** `admin`
- **Пароль:** `ynI8$56S`

> После первого входа настоятельно рекомендуется сменить пароль администратора!

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
| DELETE| `/api/guests/delete-all`         | admin       | Удалить всех гостей                         |

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
| DELETE| `/api/orders/{id}`                     | admin                 | Удалить заказ                                   |
| PUT   | `/api/orders/{id}/accept`              | admin                 | Принять заказ                                   |
| GET   | `/api/orders/{number}`                 | admin                 | получить заказ по его номеру                    |

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

## Доступные эндпоинты и используемые модели (с валидацией)

> ⚠️ Перечень моделей и валидаторов может быть неполным. Актуальные классы и описание см. в исходниках.  
> [Смотреть все модели на GitHub →](https://github.com/Roman1488e/LedMagazineBack/tree/main/LedMagazineBack/Models)  
> [Смотреть все валидаторы на GitHub →](https://github.com/Roman1488e/LedMagazineBack/tree/main/LedMagazineBack/Validators)

---

### Пример описания (далее по каждому эндпоинту):

#### Эндпоинт

`POST /api/orders/for-guests`

#### Ожидаемая модель:

```csharp
// Models/OrderModels/CreationModels/CreateOrderModel.cs
public class CreateOrderModel
{
    public string OrganisationName { get; set; }
    public string PhoneNumber { get; set; }
}
```
**Валидация (CreateOrderValidator):**
- OrganisationName: не null, не пустое, максимум 30 символов
- PhoneNumber: не null, не пустое, формат Uzbekistan: ^998\d{9}$

---

### Все найденные модели и валидаторы

#### Логин

`POST /api/customers/login`

```csharp
// Models/UserModels/Auth/LoginModel.cs
public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
```
**Валидация ([Validators/UserValidators/AuthValidators/LoginValidator.cs]):**
- Username: не null, не пусто, минимум 3 символа, максимум 30 символов
- Password: не null, не пусто, минимум 6 символов  
*(см. детали в файле /validators/uservalidators/authvalidators/LoginValidator.cs)*

---

#### Регистрация пользователя

`POST /api/customers/register`

```csharp
// Models/UserModels/Auth/RegisterModel.cs
public class RegisterModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Name { get; set; }
    public string OrganisationName { get; set; }
    public string ContactNumber { get; set; }
}
```
**Валидация ([Validators/UserValidators/AuthValidators/RegisterValidation.cs]):**
- Username: не null, не пусто, минимум 3 символа, максимум 30 символов
- Password: не null, не пусто, минимум 6 символов
- ConfirmPassword: должен совпадать с Password
- Name: не null, не пусто
- OrganisationName: не null, не пусто
- ContactNumber: формат Uzbekistan: ^998\d{9}$  
*(см. детали в файле /validators/uservalidators/authvalidators/RegisterValidation.cs)*

---

#### Добавление статьи

`POST /api/articles`

```csharp
// Models/BlogModels/CreationModels/CreateArticleModel.cs
public class CreateArticleModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IFormFile Image { get; set; }
    public IFormFile? Video { get; set; }
    public Guid BlogId { get; set; }
}
```
**Валидация ([Validators/BlogValidators/CreateArticleValidator.cs]):**
- Title: не null, не пусто, длина (обычно 3-100 символов, см. валидатор)
- Content: не null, не пусто
- Image: не null
- BlogId: не null  
*(см. детали в файле /validators/blogvalidators/CreateArticleValidator.cs)*

---

#### Обновление статьи

`PUT /api/articles/{id}/general-info`

```csharp
// Models/BlogModels/UpdateModels/UpdateArticleModel.cs
public class UpdateArticleModel
{
    public string? Title { get; set; }
    public string? Content { get; set; }
}
```
**Валидация ([Validators/BlogValidators/UpdateArticleValidator.cs]):**
- Если Title передан: не пусто, длина (обычно 3-100 символов, см. валидатор)
- Если Content передан: не пусто  
*(см. детали в файле /validators/blogvalidators/UpdateArticleValidator.cs)*

---

#### Смена роли пользователя

`PUT /api/customers/change-role`

```csharp
public class UpdateRoleModel
{
    public string? Role { get; set; }
}
```
**Валидация ([Validators/UserValidators/ChangeRoleValidation.cs]):**
- Role: не null, не пусто, одно из: Admin, Guest, Customer

---

#### Смена username

`PUT /api/customers/change-username`

```csharp
public class UpdateUsernameModel
{
    public string? Username { get; set; }
}
```
**Валидация ([Validators/UserValidators/ChangeUsernameValidation.cs]):**
- Username: не null, не пусто, длина 3-30 символов

---

#### Обновление заказа

`PUT /api/orders/{id}`

```csharp
public class UpdateOrderModel
{
    public string? OrganisationName { get; set; }
    public string? PhoneNumber { get; set; }
}
```
**Валидация ([Validators/OrderValidators/UpdateValidators/UpdateOrderValidator.cs]):**
- OrganisationName: если указан — не пусто, максимум 30 символов
- PhoneNumber: если указан — не пусто, формат ^998\d{9}$

---

#### Создание элемента заказа (OrderItem)

`POST /api/orderitem`

```csharp
public class CreateOrderItemModel
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int RentMonths { get; set; }
    public int RentSeconds { get; set; }
}
```
**Валидация ([Validators/OrderValidators/CreateOrderItemValidator.cs]):**
- OrderId: не null, не пусто
- ProductId: не null, не пусто
- RentMonths: > 0
- RentSeconds: одно из 5/10/15

---

#### Создание мультипликатора времени аренды

`POST /api/rent-time-multiplayers`

```csharp
public class CreateRentTimeMulModel
{
    public int MonthsDifferenceMultiplayer { get; set; }
    public int SecondsDifferenceMultiplayer { get; set; }
    public Guid ProductId { get; set; }
}
```
**Валидация ([Validators/RentTimeValidators/CreateRentTimeMulValidator.cs]):**
- MonthsDifferenceMultiplayer: 1-2
- SecondsDifferenceMultiplayer: 1-2
- ProductId: не пусто

---

#### Обновление мультипликатора времени аренды

`PUT /api/rent-time-multiplayers`

```csharp
public class UpdateRentTimeMultModel
{
    public int MonthsDifferenceMultiplayer { get; set; }
    public int SecondsDifferenceMultiplayer { get; set; }
    // остальные поля уточните в исходниках
}
```
**Валидация ([Validators/RentTimeValidators/UpdateValidators/UpdateRentTimeMulValidator.cs]):**
- MonthsDifferenceMultiplayer: не null, 1-2
- SecondsDifferenceMultiplayer: не null, 1-2

---

#### Обновление времени аренды

`PUT /api/rent-times`

```csharp
public class UpdateRentTimeModel
{
    public int RentMonths { get; set; }
    public int RentSeconds { get; set; }
}
```
**Валидация ([Validators/RentTimeValidators/UpdateValidators/UpdateRentTimeValidator.cs]):**
- RentMonths: не null, >0
- RentSeconds: не null, одно из 5/10/15

---

#### Создание элемента корзины

`POST /api/cartItems`

```csharp
public class CreateCartItemModel
{
    public byte RentSeconds { get; set; }
    public byte RentMonths { get; set; }
    public Guid ProductId { get; set; }
}
```
**Валидация:**  
- (валидатор не найден, проверьте ограничения на уровне контроллера)

---

#### Фильтрация продуктов

`GET /api/products?...`

```csharp
public class ProductsFilterModel
{
    public string? districts  { get; set; }
    public string? screenSizes {get; set;}
    public decimal minPrice {get; set;}
    public decimal maxPrice {get; set;}
    public string? screenResolutions {get; set;}
    public bool? isActive  { get; set; }
    public int page {get; set;}
    public int pageSize {get; set;}
}
```
**Валидация:**  
- (валидатор не найден, проверьте ограничения на уровне контроллера)

---

## Валидация в контроллерах

- Для методов, отмеченных атрибутом `[Validate]` или `[Validation]`, все входные модели автоматически валидируются через FluentValidation.
- Если валидация не проходит — возвращается 400 с описанием ошибок.

---

## FAQ

**Q: Что делать, если 400 с ошибками валидации?**  
A: Проверьте соответствие данных требованиям модели и валидатора.

---

> **Если необходимы другие модели или полная структура запроса — ищите в исходниках или пишите бэкендеру!**

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

## ВАЖНО: Бизнес-потоки и скрытые нюансы работы API

### 1. JWT и гостевая сессия — всегда первым шагом

- **Любой клиент (даже гость) должен первым делом получить JWT через POST `/api/guests/generate`**.
- Без JWT ни один другой эндпоинт не будет работать — всегда 403.

---

### 2. Cart (корзина) создаётся автоматически

- **Для гостя:** корзина создаётся автоматически при генерации гостя (`/api/guests/generate`).
- **Для зарегистрированного пользователя:** корзина создаётся при регистрации (`/api/customers/register`).
- **Создавать корзину вручную через отдельный эндпоинт не нужно!**
- После получения токена (гостевого или после регистрации) корзина уже есть — можно сразу добавлять в неё товары (`POST /api/cartItems`).

---

### 3. CartItem — корзина должна существовать заранее (создаётся автоматически, см. выше)

- Для добавления товара в корзину (`POST /api/cartItems`) **не требуется** сначала создавать корзину.
- Просто вызывайте этот эндпоинт — корзина привязана к текущей сессии (гостя или пользователя) и будет найдена автоматически.

---

### 4. Заказ (Order) и OrderItem — строгая последовательность действий

- **Нельзя создать OrderItem без Order!**
  - Сначала необходимо создать заказ (`POST /api/orders/for-guests` для гостей, `POST /api/orders/for-users` для пользователей).
  - Только после этого можно добавлять позиции в заказ (`POST /api/orderitem`).
- Для большинства сценариев заказ создаётся автоматически из корзины (например, через `/api/orders/from-cart-guests` или `/api/orders/from-cart-users`), и OrderItem'ы формируются автоматически из CartItem'ов.
  - Если требуется ручное добавление позиций — сначала создайте сам заказ, затем добавляйте OrderItem.

---

### 5. Order из корзины

- Для создания заказа чаще всего используется сценарий "оформить всё из корзины":
  - Для гостей: `POST /api/orders/from-cart-guests`
  - Для пользователей: `POST /api/orders/from-cart-users`
- В этом случае:
  - Все CartItem текущей корзины автоматически станут OrderItem'ами.
  - Корзина после этого может быть очищена.

---

### 6. Привязка данных и идентификаторов

- **Все действия с корзиной/заказом/элементами происходят только в рамках текущего пользователя/гостя** (по JWT).
- Нет необходимости (и нельзя) работать с чужими корзинами или заказами.

---

### 7. Заказ для гостя и пользователя — разные эндпоинты

- Для гостей и зарегистрированных пользователей используются разные маршруты для создания заказа:
  - **Гость:** `/api/orders/for-guests` или `/api/orders/from-cart-guests`
  - **Пользователь:** `/api/orders/for-users` или `/api/orders/from-cart-users`
- В запросе для гостей часто требуется передать дополнительные данные (имя, телефон, организация), для зарегистрированных пользователей эти данные подставляются из профиля.

---

### 8. OrderItem и CartItem — ключевые связи

- **OrderItem** требует существующего Order (сначала создать заказ!).
- **CartItem** требует существующей Cart (создаётся автоматически при генерации гостя или регистрации пользователя).

---

### 9. Флоу: от гостя к пользователю

- После регистрации пользователя корзина привязывается к новому пользователю (и далее работает как "user cart").

---

### 10. Удаление корзины, заказов, позиций

- Удалять можно только свои объекты (те, что привязаны к вашему JWT).

---

### 11. Валидация

- Все модели, которые передаются в POST/PUT, валидируются до входа в бизнес-логику.
- Если что-то не так — 400 с описанием ошибок.

---

### 12. Примеры флоу

#### Быстрый старт для гостя

1. POST `/api/guests/generate` — получить JWT
2. POST `/api/cartItems` — добавить товар в корзину
3. POST `/api/orders/from-cart-guests` — оформить заказ из корзины

#### Быстрый старт для пользователя

1. POST `/api/guests/generate` — получить гостевой JWT (или сразу регистрация)
2. POST `/api/customers/register` — регистрация пользователя (корзина будет привязана)
3. POST `/api/cartItems` — добавить товар в корзину
4. POST `/api/orders/from-cart-users` — оформить заказ из корзины

---

### 13. Важные ограничения

- **Нельзя создать OrderItem без Order.**
- **Cart и CartItem можно создавать только для текущей сессии/JWT.**
- **Корзина создаётся автоматически — не пытайтесь создавать её руками!**


---

## Контакты

Если есть вопросы по интеграции — обращайтесь к бэкендеру.
Телеграм @greatpool

---

> **Сначала всегда делай запрос на `/api/guests/generate`, иначе ни один другой эндпоинт не будет работать!**

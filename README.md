# TestTask.MaternityHospital

## 📘 Описание

Веб-приложение для работы с данными родильного дома. Реализован REST API для управления информацией о пациентах, включая:

- добавление пациентов,
- редактирование,
- удаление,
- поиск по дате рождения и другим параметрам,
- получение по идентификатору.

Дополнительно реализовано консольное приложение `PatientUploader`, которое автоматически генерирует и отправляет тестовые данные в API.

---

## 📦 Структура проекта

- `TestTask.MaternityHospital.App` — Web API (ASP.NET Core)
- `TestTask.MaternityHospital.PatientUploader` — консольное приложение, отправляющее данные в API
- `docker-compose.yml` — сборка и запуск всех компонентов
- `PostmanCollection.json` — коллекция запросов для тестирования API в Postman

---

## 🚀 Быстрый старт

### 🔧 Предварительные требования

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

---

### 🐳 Запуск проекта

Выполни команду:

```bash
docker-compose up app db
```

Что произойдёт:

- Запустится контейнер с MySQL
- Запустится Web API по адресу `http://localhost:5000`

---

### 📬 Тестирование API

#### 🔹 Swagger UI

Открой в браузере:

```
http://localhost:5000/swagger
```

#### 🔹 Postman

Импортируй файл `PostmanCollection.json` в Postman и протестируй:

- Добавление пациента
- Получение по ID
- Поиск по дате рождения
- Обновление
- Удаление

---

## 🧪 Ручной запуск генерации данных

Если ты хочешь наполнить API тестовыми данными, выполни (только после полного старта app и db):

```bash
docker-compose run --rm uploader
```

Приложение без запроса автоматически отправит 100 новых случайных пациентов в API.

---

## 🧹 Завершение работы

Для остановки контейнеров:

```bash
docker-compose down
```

Если хочешь полностью удалить тома с БД (и все данные):

```bash
docker-compose down -v
```

---

## 🛠️ Стек технологий

- **.NET 6**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MySQL 8**
- **Docker**
- **Swagger / OpenAPI**

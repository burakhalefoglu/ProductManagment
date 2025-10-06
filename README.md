# 🚀 Product Management Case — Development Guide

## 🔑 Credentials

| Key          | Value                      |
| ------------ | -------------------------- |
| **Email**    | `burakhalefoglu@gmail.com` |
| **Password** | `Developer**++`            |

---

## ⚙️ Local Environment Setup

### 1️⃣ Run Redis (before backend)

```bash
docker run -d \
  --name redis \
  --restart unless-stopped \
  -p 6379:6379 \
  redis:6.2-alpine
```

---

### 2️⃣ Run Backend (.NET)

Run in **Development mode**:

```bash
dotnet run --launch-profile "Development"
```

> 💡 Make sure the Redis container is running before starting the backend.

---

### 3️⃣ Run Angular (Frontend)

Install dependencies and start the development server:

```bash
npm install
ng serve
```

---

## 🌐 URLs

### 🧬 Backend

* 🔹 [Development](https://dev.api.productmanagementcase.burakhalefoglu.com)
* 🔹 [Production](https://api.productmanagementcase.burakhalefoglu.com)

### 💻 Angular (Frontend)

* 🔹 [Development](https://dev.productmanagementcase.burakhalefoglu.com)
* 🔹 [Production](https://productmanagementcase.burakhalefoglu.com)

### 🧠 Services

* 🐇 RabbitMQ (Dev): [https://dev.rabbitmq.pm.burakhalefoglu.com](https://dev.rabbitmq.pm.burakhalefoglu.com)
* 🐇 RabbitMQ (Prod): [https://rabbitmq.pm.burakhalefoglu.com](https://rabbitmq.pm.burakhalefoglu.com)
* ⚙️ Jenkins: [https://jenkins.scpbilisim.com](https://jenkins.scpbilisim.com)

---

## 🧾 Notes

* Ensure Docker is running before launching the services.
* Use **Development** environment for testing and debugging.
* Check `.env` variables if any connection issues occur.

---

🧑‍💻 **Maintainer:** Burak Halefoglu
📧 [burakhalefoglu@gmail.com](mailto:burakhalefoglu@gmail.com)

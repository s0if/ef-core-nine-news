# DigitalOcean Deployment Documentation

## 🧩 Problem Encountered

At the beginning of this task, I was unable to perform the actual deployment on DigitalOcean because it requires a **paid subscription and credit card verification**, which was not available to me at the time of implementation.

However, the full deployment plan and technical steps are documented below as if the environment were available.

---

## 🖥️ Chosen Option

**Option 1: Windows Droplet + IIS**

---

## 🛠️ Deployment Flow (Simplified)

### 1️⃣ Create Windows Droplet

- Login to DigitalOcean
- Create Droplet
- Choose:
  - OS: **Windows Server**
  - Plan: Basic
  - Region: nearest location
- Assign public IP

---

### 2️⃣ Access the Server

- Connect using **Remote Desktop (RDP)**
- Login with administrator credentials

---

### 3️⃣ Install IIS Web Server

- Open _Server Manager_
- Add Role: **Web Server (IIS)**
- Enable:
  - HTTP features
  - Application Development
  - Logging & Diagnostics

---

### 4️⃣ Configure Application

#### 🔐 Environment Variables

- Open **System Properties**
- Add required environment variables for the application

#### 🔥 Firewall & Ports

- Open required ports:
  - `80` (HTTP)
  - `443` (HTTPS)
- Allow application port if needed

---

### 5️⃣ Health Check & Logging

- Configure IIS logging:

```
C:\inetpub\logs\LogFiles
```

- Create health check endpoint:

```
/health
```

Returns:

```json
{ "status": "OK" }
```

---

### 6️⃣ Optional: SSL Configuration

(If domain & certificate are available)

- Bind HTTPS on port 443
- Install SSL certificate
- Force HTTPS redirection

---

## 📦 Deliverables

Due to subscription limitations, the following were delivered instead:

- This documentation file
- Step-by-step deployment flow
- System configuration design

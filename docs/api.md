# API – Portal B2B dla dystrybutorów

## 1. Autoryzacja
### POST /api/auth/login
- Request:
{
  "username": "user1",
  "password": "Password123!"
}
- Response (200):
{
  "token": "<JWT_TOKEN>",
  "user": {
    "id": 1,
    "username": "user1",
    "role": "Distributor"
  }
}
- Response (401): { "error": "Invalid credentials" }

### POST /api/auth/change-password
- Request:
{
  "oldPassword": "Password123!",
  "newPassword": "NewPass!2025"
}
- Response (200): { "message": "Password changed successfully" }
- Response (400): { "error": "Invalid old password" }

### POST /api/auth/unlock/{userId}
- Dostęp: admin/super-admin.
- Response (200): { "message": "User unlocked" }

---

## 2. Sales
### GET /api/sales
- Query params: ?distributorId=1
- Response:
[
  {
    "quarter": "Q1 2025",
    "currency": "PLN",
    "professional": 10000,
    "pharmacy": 5000,
    "ecommerceB2C": 3000,
    "ecommerceB2B": 2000,
    "thirdParty": 1000,
    "other": 500,
    "total": 21500,
    "newClients": 3,
    "eurTotal": 4800
  }
]

### POST /api/sales
- Request:
{
  "quarter": "Q1 2025",
  "currency": "PLN",
  "professional": 10000,
  "pharmacy": 5000,
  "ecommerceB2C": 3000,
  "ecommerceB2B": 2000,
  "thirdParty": 1000,
  "other": 500,
  "newClients": 3
}
- Response (201): { "message": "Sales data saved" }

### GET /api/sales/export
- Response: plik CSV (UTF-8).

---

## 3. Purchases
### GET /api/purchases
- Response:
[
  {
    "quarter": "Q1 2025",
    "lastYearSales": 15000,
    "purchases": 12000,
    "budget": 18000,
    "actualSales": 21500,
    "yoY": "+43%",
    "vsBudget": "-33%",
    "totalPOS": 40,
    "newOpenings": 5,
    "newOpeningsTarget": 10
  }
]

### POST /api/purchases
- Request:
{
  "quarter": "Q1 2025",
  "lastYearSales": 15000,
  "purchases": 12000,
  "budget": 18000,
  "actualSales": 21500,
  "totalPOS": 40,
  "newOpenings": 5,
  "newOpeningsTarget": 10
}
- Response (201): { "message": "Purchase data saved" }

### GET /api/purchases/export
- Response: plik CSV (UTF-8).

---

## 4. Media
### GET /api/media
- Response:
[
  {
    "id": 1,
    "fileName": "SKU123_manual.pdf",
    "path": "/PRODUCTS/SKU123/",
    "size": "1.2MB",
    "uploadedAt": "2025-09-13T10:00:00Z"
  }
]

### GET /api/media/{id}/download
- Response: plik binarny.

### GET /api/media/search?sku=SKU123
- Response: lista plików dla danego SKU.

---

## 5. Admin
### GET /api/admin/users
- Response:
[
  { "id": 1, "username": "user1", "role": "Distributor", "isLocked": false },
  { "id": 2, "username": "manager1", "role": "ExportManager", "isLocked": true }
]

### POST /api/admin/users
- Request:
{
  "username": "newuser",
  "role": "Distributor",
  "password": "InitPass!123"
}
- Response (201): { "message": "User created" }

### PUT /api/admin/users/{id}/role
- Request:
{
  "role": "Admin"
}
- Response (200): { "message": "Role updated" }

### GET /api/admin/logs
- Response:
[
  { "id": 1, "userId": 1, "action": "LOGIN", "timestamp": "2025-09-13T10:00:00Z" },
  { "id": 2, "userId": 1, "action": "EXPORT_CSV", "timestamp": "2025-09-13T10:15:00Z" }
]

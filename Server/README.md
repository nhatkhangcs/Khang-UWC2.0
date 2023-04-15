## api for se-uwc

Request type and format for the api:

### Auth

- Create account: POST `/api/auth/createAccount`

  ```json
  {
    "name": "Poem McPlaceholder",
    "gender": "Male",
    "dob": "1976/12/09",
    "address": "London",
    "phone": "0123456789",
    "nationality": "Thailand",
    "hired_on": "2014/07/05",
    "role": "Collector",
    "salary": 20,
    "username": "poem.m1976",
    "password": "password"
  }
  ```

- Log in: POST `/api/auth/login`
  ```json
  {
    "username": "poem.m1976",
    "password": "password"
  }
  ```
  - Response trả về sẽ là access token và refresh token của lần đăng nhập này
- Log out: DELETE `/api/auth/logout`
  ```json
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InBvZW0ubTE5NzYiLCJyb2xlIjoiQ29sbGVjdG9yIiwiaWF0IjoxNjcwMTU4NjAwfQ.tay1TeFpxdwsImSeKN_gB-UN5aiUVmwMyI-XCc5LiBI"
  }
  ```
  - Payload là refresh token được cấp khi log in
- Ask for access token regrant: POST `/api/auth/token`
  ```json
  {
    "token": "yadjhgsafgygfuasbvadfvadvfh"
  }
  ```
  - Payload là refresh token được cấp khi log in

Các request dưới đây đều cần Authorization Bearer Token là access token được cấp khi log in để được nhận response

### Map

- Input waypoints for a collector: POST `/api/map/waypoints`
  ```json
  {
    "collectorId": "1a2b3c4d5e",
    "points": [
      { "latitude": 10.764754028189921, "longitude": 106.66325831951859 },
      { "latitude": 10.765303544543384, "longitude": 106.66596254624939 },
      { "latitude": 10.765955387947566, "longitude": 106.66580824269722 }
    ]
  }
  ```
- Get current position of a collector: GET `/api/map/currentPosition/:collectorId` (Ex: GET `/api/map/currentPosition/1a2b3c4d5e`)
- Get current position of every collector: GET `/api/map/allCurrentPosition`

### MCP

- Insert a new MCP: POST `/api/mcp/add`
  ```json
  {
    "address": "262 Nguyễn Tiểu La, Quận 10",
    "capacity": 105,
    "latitude": 10.7660997698857,
    "longitude": 106.665916767231
  }
  ```
- Get all MCP: GET `/api/mcp/all`
- Get all MCP current status: GET `/api/mcp/current/all`
- Get info of an MCP by MCP_id: GET `/api/mcp/:id` (Ex: GET `/api/mcp/1bde7f5293`)
- Get current state of MCP: GET `/api/mcp/current/:id` (Ex: GET `/api/mcp/current/1bde7f5293`)
- Get current state of MCP (in %): GET `/api/mcp/current/percentage/:id` (Ex: GET `/api/mcp/current/percentage/1bde7f5293`)
- Reset current state: PUT `/api/current/reset/:id` (Ex: PUT `/api/current/reset/1bde7f5293`)

### Vehicle

- Add vehicle: POST `/api/vehicle/add`
  ```json
  {
    "id": "51F-627.24",
    "category": "Truck",
    "model": "BRUH-BRUH",
    "weight": 200,
    "capacity": 215,
    "fuel_consumption": 3
  }
  ```
- Get all vehicles: GET `/api/vehicle/all`
- Get vehicles by type: POST `/api/vehicle/type`
  ```json
  {
    "type": "Trolley"
  }
  ```
- Get info of vehicle by id: GET `/api/vehicle/info/:id` (Ex: GET `/api/vehicle/info/51F-627.24`)

### Maintenance log

- Add a log: POST `/api/maintainLog/add`
  ```json
  {
    "vehicle_id": "51F-627.24",
    "createdAt": "2022/12/04 12:30:45 (field này có cũng được, ko có thì DB tự động thêm ở thời điểm insert vào)",
    "detail": "Engine broke down",
    "cost": 2000
  }
  ```
- Get all maintenance log: GET `/api/maintainLog/all`
- Get detailed info of a log by id: GET `/api/maintainLog/info/:id` (Ex: GET `/api/maintainLog/info/edb45a238f`)
- Get log of a vehicle by its id: GET `/api/maintainLog/vehicle/:id` (Ex: GET `/api/maintainLog/vehicle/51F-627.24`)

### Task

- Add a task: POST `/api/task/add`
  ```json
  {
    "employee_id": "1a2b3c4d5e",
    "mcp_id": "1bde7f5293",
    "vehicle_id": "51F-627.24 (field này chỉ có khi nào có vehicle để assign nha)",
    "timeToDo": "2022/12/04 08:47:45",
    "checkin": 1,
    "checkout": 0
  }
  ```
- Get info of a task: GET `/api/task/info/:taskId` (Ex: GET `/api/task/info/5d425ea987aeb09`)
- Get info of tasks of an employee: GET `/api/task/info/employee/:id` (Ex: GET `/api/task/info/employee/1a2b3c4d5e`)
- Delete a task from DB: DELETE `/api/task/remove/:taskId`(Ex: DELETE `/api/task/remove/5d425ea987aeb09`)

### Setting

- Add a setting: POST `/api/setting/add`
  ```json
  {
    "user_id": "1a2b3c4d5e",
    "theme_colour": 1,
    "dark_theme": 1,
    "colour_blind": 1,
    "reduced_motion": 0,
    "language": "english",
    "message_notify": 1,
    "employees_login": 0,
    "employees_logout": 0,
    "mcp_full_notify": 1,
    "mcp_empty_notify": 0,
    "maintain_log_update": 0,
    "update_notify": 1,
    "online_status": "online",
    "auto_logout": 1,
    "auto_send_crash_log": 0
  }
  ```
- Get setting info of an employee: GET `/api/setting/config/:employeeId` (Ex: GET `/api/setting/config/1a2b3c4d5e`)
- Update setting: PUT `/api/setting/config/:employeeId` (Ex: PUT `/api/setting/config/1a2b3c4d5e`)
  - Sửa cái gì thì để field đó trong json payload thôi
    ```json
    {
      "language": "vietnamese",
      "message_notify": 0
    }
    ```

### Message

- Add message: POST `/api/message/add`
  ```json
  {
    "sender_id": "1a2b3c4d5e",
    "receiver_id": "3d452b69a7",
    "sentAt": "2022/11/30 23:55:19 (field này có cũng được, ko có thì DB tự động thêm ở thời điểm insert vào)",
    "content": "You have done a garbage job today!!!!!!"
  }
  ```
- Get all message of 2 accounts: POST `/api/message`
  ```json
  {
    "user1": "1a2b3c4d5e",
    "user2": "3d452b69a7"
  }
  ```
  - user1, user2 để theo thứ tự nào cũng được

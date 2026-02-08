<p align="center">
  <img src="https://socialify.git.ci/ivanzlatinov1/HVACrate/image?description=1&font=Rokkitt&language=1&name=1&owner=1&pattern=Brick+Wall&theme=Dark" alt="HVACrate Project Banner">
</p>

---

# 🔥 HVACrate ❄️

**HVACrate** is a powerful application designed to help users calculate heat loss and cooling loads for individual rooms within a building. By entering essential room data—such as dimensions, heat transfer coefficients, and material densities — you can accurately determine energy requirements for HVAC systems.

---

## 🧐 Features

Here are some of the app's key features:

- Calculate **heat loss** and **cooling loads** for individual rooms.
- Input detailed information about each room’s **walls, ceiling, floor, and windows**.
- Automatically compute infiltration losses and cooling demands.
- Organize your work into **multiple projects** and **buildings**, with **floor-based room access**.
- Full **CRUD operations** on projects, buildings, floors, and rooms.

---
## 💻 Download from the site

1. **Head over to the HVACrate site**  
   👉 https://hvacrate.onrender.com/

2. **Pick your operating system**  
   - Supported: **Windows**  & **Linux** 
   - Download the app

3. **Run the app**  
   - Follow the instructions given on the site and you’re good to go ✨

---
## ⚙️ Installation Guide (ASP.NET Core MVC - C#)

To get HVACrate up and running on your local machine from this repository you need to:

1. **Clone the repository**:
 
   ```bash
   git clone https://github.com/ivanzlatinov1/HVACrate.git
   cd HVACrate
   ```
   
3. **Install prerequisites**:
   - [.NET SDK 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
   - [PostgreSQL](https://www.postgresql.org/download/)

4. **Set up the database**:
   - Create a PostgreSQL database manually or via a GUI tool like pgAdmin.
   - Update your connection string in `appsettings.json`:

     ```json
     "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=HVACrate;Username=your_username;Password=your_password"
     },
     ```
5. **Restore Dependencies**:
   
   ```bash
   dotnet restore
   ```

6. **Apply Database Migrations**:

   ```bash
   dotnet ef database update
   ```
7. **Run The Application**:

   ```bash
   dotnet run
   ```
---

## 🧰 Technologies Used

- **ASP.NET Core MVC (6.0)**
- **Electron.NET**
- **PostgreSQL**

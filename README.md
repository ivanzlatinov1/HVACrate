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

## 📸 Project Screenshots

<img src="https://media.discordapp.net/attachments/552165958635094019/1402543369237954570/image.png?ex=68944bcd&amp;is=6892fa4d&amp;hm=8d1760f67b8676a52420815ad86bfce375306768e4e33b0de422ee8e65e79fc8&amp;=&amp;format=webp&amp;quality=lossless&amp;width=1860&amp;height=688" alt="project-screenshot" width="1200">

<img src="https://media.discordapp.net/attachments/552165958635094019/1402543658741399552/image.png?ex=68944c12&amp;is=6892fa92&amp;hm=1cb59c32d68e6ae390896c230010860ed9597a0500ff7e81a8cb596e74c4446a&amp;=&amp;format=webp&amp;quality=lossless&amp;width=1860&amp;height=826" alt="project-screenshot" width="1200">

<img src="https://cdn.discordapp.com/attachments/552165958635094019/1402544422557716670/image.png?ex=68944cc8&amp;is=6892fb48&amp;hm=0a4d163f73ab631c6f773b344c51e5e204ccfd9c5729b3e8d7f1c9eb5a1947a4&amp;" alt="project-screenshot" width="1200">

---

## ⚙️ Installation Guide (ASP.NET Core MVC - C#)

To get HVACrate up and running on your local machine:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/ivanzlatinov1/HVACrate.git
   cd HVACrate
   ```
2. **Install prerequisites**:
   - [.NET SDK 6.0+](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
   - [Node.js (for Electron integration)](https://nodejs.org/)
   - [PostgreSQL](https://www.postgresql.org/download/)

3. **Set up the database**:
   - Create a PostgreSQL database manually or via a GUI tool like pgAdmin.
   - Update your connection string in `appsettings.json`:

     ```json
     "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=HVACrate;Username=your_username;Password=your_password"
     },
     ```

4. **Apply Entity Framework migrations**:

   ```bash
   dotnet ef database update
   ```
5. **Run the application**:

   ```bash
   dotnet run
   ```
---

## 🧰 Technologies Used

- **C#**
- **ASP.NET Core MVC (6.0+)**
- **Electron**
- **PostgreSQL**

---

## 🤝 Contributing

Contributions are welcome!

If you'd like to improve the project or have suggestions, feel free to fork the repository, open issues, or submit a pull request.

For direct communication or questions, please contact me at:

📧 **ivanzlatinov006@gmail.com**



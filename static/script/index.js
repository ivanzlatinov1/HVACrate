document.addEventListener("DOMContentLoaded", function () {
  VANTA.NET({
    el: "#digital",
    mouseControls: true,
    touchControls: true,
    gyroControls: false,
    minHeight: 200.0,
    minWidth: 200.0,
    scale: 1.0,
    scaleMobile: 1.0,
    color: 0x777777,
    backgroundColor: 0x000000,
    points: 14.0,
    maxDistance: 24.0,
    spacing: 17.0,
  });
});

document.getElementById("menu").addEventListener("click", () => {
  options = document.getElementById("options");
  options.style.visibility = "visible";
});
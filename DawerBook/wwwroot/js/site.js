
document.addEventListener("DOMContentLoaded", function () {

    const canvas = document.getElementById("generosChart");

    if (!canvas) {
        return;
    }

    // Obtener los datos enviados desde la vista
    const generos = JSON.parse(canvas.dataset.generos);
    const cantidades = JSON.parse(canvas.dataset.cantidades);

    // Calcular la cantidad total de libros
    const total = cantidades.reduce((a, b) => a + b, 0);

    const colores = [
        "#103c70",
        "#1967b3",
        "#438bd0",
        "#7ba7dc",
        "#afc9e8",
        "#d5e3f2"
    ];

    // Texto ubicado en el centro del gráfico
    const textoCentral = {
        id: "textoCentral",

        afterDraw(chart) {

            const { ctx } = chart;

            const arco = chart.getDatasetMeta(0).data[0];

            if (!arco) {
                return;
            }

            ctx.save();

            ctx.textAlign = "center";
            ctx.textBaseline = "middle";

            ctx.font = "bold 26px Arial";
            ctx.fillStyle = "#153b68";
            ctx.fillText(total, arco.x, arco.y - 9);

            ctx.font = "13px Arial";
            ctx.fillStyle = "#64748b";
            ctx.fillText("Libros", arco.x, arco.y + 15);

            ctx.restore();
        }
    };

    // Crear el gráfico circular
    new Chart(canvas, {
        type: "doughnut",

        data: {
            labels: generos.map((genero, i) => {

                const porcentaje =
                    (cantidades[i] / total * 100).toFixed(1);

                return `${genero} (${porcentaje}%)`;
            }),

            datasets: [{
                data: cantidades,

                backgroundColor: cantidades.map(
                    (_, i) => colores[i % colores.length]
                ),

                borderWidth: 0,
                hoverOffset: 5
            }]
        },

        options: {
            responsive: true,
            maintainAspectRatio: false,
            cutout: "72%",

            plugins: {
                legend: {
                    position: "right",

                    labels: {
                        usePointStyle: true,
                        pointStyle: "circle",
                        padding: 18,

                        font: {
                            size: 12
                        }
                    }
                }
            }
        },

        plugins: [textoCentral]
    });

});

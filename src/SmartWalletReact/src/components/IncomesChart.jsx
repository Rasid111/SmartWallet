import { Bar } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

const IncomeChart = ({ data }) => {
  const getUniqueCurrencies = (data) => {
    const currencies = new Set();
    data.forEach((item) => currencies.add(item.currency));
    return Array.from(currencies);
  };

  const processData = (data) => {
    const incomeTypes = {};
    const currencies = getUniqueCurrencies(data);

    data.forEach((item) => {
      if (!incomeTypes[item.type]) {
        incomeTypes[item.type] = {};
        currencies.forEach((currency) => {
          incomeTypes[item.type][currency] = 0;
        });
      }
      incomeTypes[item.type][item.currency] += item.amount;
    });

    return { incomeTypes, currencies };
  };

  const { incomeTypes: processedData, currencies } = processData(data);
  const incomeTypeNames = Object.keys(processedData);

  const datasets = currencies.map((currency) => ({
    label: currency,
    data: incomeTypeNames.map((type) => processedData[type][currency] || 0),
    backgroundColor: getColorForCurrency(currency),
  }));

  const chartData = {
    labels: incomeTypeNames,
    datasets: datasets,
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: "top",
      },
      title: {
        display: true,
        text: "Income by Type and Currency",
      },
      tooltip: {
        callbacks: {
          label: function (context) {
            return `${context.dataset.label}: ${context.raw}`;
          },
        },
      },
    },
    scales: {
      y: {
        beginAtZero: true,
        title: {
          display: true,
          text: "Amount",
        },
      },
      x: {
        title: {
          display: true,
          text: "Income Type",
        },
      },
    },
  };

  return (
    <>
      <h2>Income Chart</h2>
      <Bar data={chartData} options={options} />
    </>
  );
};

function getColorForCurrency(currency) {
  const currencyColors = {
    AZN: "#3498db",
    USD: "#2ecc71",
    EUR: "#e74c3c",
    GBP: "#f39c12",
    JPY: "#9b59b6",
  };

  return (
    currencyColors[currency] ||
    `#${Math.floor(Math.random() * 16777215).toString(16)}`
  );
}

const IncomesChart = (payments) => {
  return <IncomeChart data={payments.children} />;
};

export default IncomesChart;

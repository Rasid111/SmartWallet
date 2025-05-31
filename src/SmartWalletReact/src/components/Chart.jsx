import React from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

const PaymentChart = ({ data }) => {
  // Получаем уникальные валюты из данных
  const getUniqueCurrencies = (data) => {
    const currencies = new Set();
    data.forEach(item => currencies.add(item.currency));
    return Array.from(currencies);
  };

  // Группируем данные по категориям и валютам
  const processData = (data) => {
    const categories = {};
    const currencies = getUniqueCurrencies(data);
    
    data.forEach(item => {
      if (!categories[item.type]) {
        categories[item.type] = {};
        // Инициализируем все валюты для каждой категории
        currencies.forEach(currency => {
          categories[item.type][currency] = 0;
        });
      }
      categories[item.type][item.currency] += item.amount;
    });
    
    return { categories, currencies };
  };

  const { categories: processedData, currencies } = processData(data);
  const categoryNames = Object.keys(processedData);

  // Создаем наборы данных для Chart.js
  const datasets = currencies.map(currency => ({
    label: currency,
    data: categoryNames.map(category => processedData[category][currency] || 0),
    backgroundColor: getColorForCurrency(currency),
  }));

  const chartData = {
    labels: categoryNames,
    datasets: datasets,
  };

  const options = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top',
      },
      title: {
        display: true,
        text: 'Payments by Category and Currency',
      },
    },
    scales: {
      y: {
        beginAtZero: true,
        title: {
          display: true,
          text: 'Amount',
        },
      },
      x: {
        title: {
          display: true,
          text: 'Payment Category',
        },
      },
    },
  };

  return (
    <div style={{ width: '80%', margin: '0 auto' }}>
      <h2>Payment Statistics</h2>
      <Bar data={chartData} options={options} />
    </div>
  );
};

// Функция для получения цвета в зависимости от валюты
function getColorForCurrency(currency) {
  const currencyColors = {
    AZN: '#3498db',
    EUR: '#2ecc71',
    USD: '#e74c3c',
    GBP: '#f39c12',
    JPY: '#9b59b6',
    // Добавьте другие валюты по необходимости
  };
  
  return currencyColors[currency] || `#${Math.floor(Math.random()*16777215).toString(16)}`;
}

// Sample usage with your data
const MyChart = (payments) => {
  console.log(payments.children);

  return <PaymentChart data={payments.children} />;
};

export default MyChart;

import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';
import { useEffect } from 'react';

// Register ChartJS components
ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

const PaymentChart = ({ data }) => {
  // Process the data to group by type and currency
  const processData = (data) => {
    const categories = {};
    
    data.forEach(item => {
      if (!categories[item.type]) {
        categories[item.type] = {};
      }
      if (!categories[item.type][item.currency]) {
        categories[item.type][item.currency] = 0;
      }
      categories[item.type][item.currency] += item.amount;
    });
    
    return categories;
  };

  const processedData = processData(data);
  const categories = Object.keys(processedData);
  const currencies = ['AZN', 'EUR', 'USD']; // Extract unique currencies

  // Prepare datasets for Chart.js
  const datasets = currencies.map(currency => ({
    label: currency,
    data: categories.map(category => processedData[category][currency] || 0),
    backgroundColor: getRandomColor(),
  }));

  const chartData = {
    labels: categories,
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

// Helper function to generate random colors
function getRandomColor() {
  const letters = '0123456789ABCDEF';
  let color = '#';
  for (let i = 0; i < 6; i++) {
    color += letters[Math.floor(Math.random() * 16)];
  }
  return color;
}

// Sample usage with your data
const MyChart = () => {
  // useEffect(() => {
    
  // });
  // const paymentData = axios.get(`http://loccalhost:5000/api/payment/getpaymentbyuserid/${}`)

  // return <PaymentChart data={paymentData} />;
};

export default MyChart;
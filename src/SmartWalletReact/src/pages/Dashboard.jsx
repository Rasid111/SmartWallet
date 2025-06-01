import { Col, Container, Row } from "react-bootstrap";
import "../styles/Dashboard.scss";
import SpendingsChart from "../components/SpendingsChart";
import IncomesChart from "../components/IncomesChart";
import { useEffect, useState } from "react";
import axios from "axios";

export default function Dashboard() {
  const [id, setId] = useState(localStorage.getItem("id"));
  const [paymentsData, SetPaymentsData] = useState([]);
  const [incomesData, SetIncomesData] = useState([]);
  useEffect(() => {
    console.log(1);
    axios
      .get(`http://localhost:5000/api/Payment/GetPaymentByUserId/${id}`)
      .then((response) => {
        const payments = response.data;
        SetPaymentsData(payments);
      })
      .catch((error) => {
        console.error("Error fetching payment data:", error);
      });
    axios
      .get(`http://localhost:5000/api/Income/GetIncomeByUserId/${id}`)
      .then((response) => {
        const incomes = response.data;
        SetIncomesData(incomes);
      })
      .catch((error) => {
        console.error("Error fetching incomes data:", error);
      });
  }, []);
  console.log(paymentsData);
  console.log(incomesData);
  return (
    <Container>
      <Row>
        <Col xs={6}>
          {paymentsData.length === 0 ? (
            <h2>No payment data available</h2>
          ) : (
            <SpendingsChart>{paymentsData}</SpendingsChart>
          )}
        </Col>
        <Col xs={6}>
          {paymentsData.length === 0 ? (
            <h2>No income data available</h2>
          ) : (
            <IncomesChart>{incomesData}</IncomesChart>
          )}
        </Col>
      </Row>
    </Container>
  );
}

import { Accordion, Button, Col, Container, Row } from "react-bootstrap";
import "../styles/Dashboard.scss";
import SpendingsChart from "../components/SpendingsChart";
import IncomesChart from "../components/IncomesChart";
import { useEffect, useState } from "react";
import axios from "axios";
import AiSuggestions from "../components/AiSuggestions";
import AiInvestments from "../components/AiInvestments";
import QRScanner from "../components/QrScanner";
import AiBestPrice from "../components/AiBestPrice";
import AiPrediction from "../components/AiPrediction";

export default function Dashboard() {
  const [id, setId] = useState(localStorage.getItem("id"));
  const [paymentsData, SetPaymentsData] = useState([]);
  const [incomesData, SetIncomesData] = useState([]);
  const [showQrScanner, setShowQrScanner] = useState(false);
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
      {/* <AiSuggestions></AiSuggestions> */}

      <Accordion>
        <Accordion.Item eventKey="0">
          <Accordion.Header>Investments</Accordion.Header>
          <Accordion.Body>
            <AiInvestments></AiInvestments>
          </Accordion.Body>
        </Accordion.Item>
        <Accordion.Item eventKey="1">
          <Accordion.Header>BestPrice</Accordion.Header>
          <Accordion.Body>
            <AiBestPrice></AiBestPrice>
          </Accordion.Body>
        </Accordion.Item>
        <Accordion.Item eventKey="2">
          <Accordion.Header>Forecast</Accordion.Header>
          <Accordion.Body>
            <AiPrediction></AiPrediction>
          </Accordion.Body>
        </Accordion.Item>
      </Accordion>
      <Row className="text-center justify-content-center mt-4">
        <Col xs={4}>
          <Button onClick={() => setShowQrScanner(!showQrScanner)}>
            Scan QR-code
          </Button>
        </Col>
      </Row>
      <Row hidden={!showQrScanner}>
        <Col xs={12} className="mb-5">
          <QRScanner></QRScanner>
        </Col>
      </Row>
    </Container>
  );
}

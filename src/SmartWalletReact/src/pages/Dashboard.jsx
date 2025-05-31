import { Col, Container, Row } from "react-bootstrap";
import "../styles/Dashboard.scss";
import Chart from "../components/Chart";
import { useEffect, useState } from "react";
import axios from "axios";

export default function Dashboard() {
  const [id, setId] = useState(localStorage.getItem("id"));
  const [fetchedPaymentData, setFetchedPaymentData] = useState([]);
  useEffect(() => {
    console.log(1);
    axios
      .get(`http://localhost:5000/api/Payment/GetPaymentByUserId/${id}`)
      .then((response) => {
        const payments = response.data;
        setFetchedPaymentData(payments);
      })
      .catch((error) => {
        console.error("Error fetching payment data:", error);
      });
  }, []);
  console.log(fetchedPaymentData);
  return (
    <Container>
      <Row>
        <Col xs={6}>
          {fetchedPaymentData.length === 0 ? (
            <h2>No payment data available</h2>
          ) : (
            <Chart>{fetchedPaymentData}</Chart>
          )}
        </Col>
      </Row>
    </Container>
  );
}

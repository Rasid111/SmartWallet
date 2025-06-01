import axios from "axios";
import { useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";

export default function AiSuggestions() {
  const [message, setMessage] = useState("");
  useEffect(() => {
    axios
      .get(
        `http://localhost:5000/api/AiAnalysis/statistics/${localStorage.getItem(
          "id"
        )}`
      )
      .then((response) => {
        const data = response.data;
        if (data && data.advice) {
          setMessage(data.advice);
        } else {
          setMessage("No suggestions available at the moment.");
        }
      })
      .catch((error) => {
        console.error("Error fetching AI suggestions:", error);
        setMessage("Failed to load suggestions. Please try again later.");
      });
  }, []);
  return (
    <Row className="mt-4 brodered">
      <Col xs={12}>{message}</Col>
    </Row>
  );
}

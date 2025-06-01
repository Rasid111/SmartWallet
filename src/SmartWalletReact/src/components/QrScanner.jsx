import { useState, useRef, useEffect } from "react";
import axios from "axios";
import jsQR from "jsqr";

const QRScanner = () => {
  const [scanResult, setScanResult] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const videoRef = useRef(null);
  const canvasRef = useRef(null);
  const streamRef = useRef(null);

  // Инициализация сканера QR-кода
  useEffect(() => {
    let interval;

    const startScanner = async () => {
      try {
        const stream = await navigator.mediaDevices.getUserMedia({
          video: { facingMode: "environment" },
        });
        videoRef.current.srcObject = stream;
        streamRef.current = stream;

        interval = setInterval(scanQRCode, 500);
      } catch (err) {
        setError("Не удалось получить доступ к камере: " + err.message);
      }
    };

    const scanQRCode = () => {
      if (videoRef.current && canvasRef.current) {
        const video = videoRef.current;
        const canvas = canvasRef.current;
        const context = canvas.getContext("2d");

        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        context.drawImage(video, 0, 0, canvas.width, canvas.height);

        const imageData = context.getImageData(
          0,
          0,
          canvas.width,
          canvas.height
        );

        try {
          // Используем jsQR для декодирования QR-кода
          const code = jsQR(imageData.data, imageData.width, imageData.height);
          if (code) {
            console.log(code.data);
            try {
              const jsonData = JSON.parse(code.data.trim());
              console.log("Сканированный QR-код:", jsonData);
              setScanResult(jsonData);
              clearInterval(interval);
              stopCamera();
              sendDataToApi(jsonData);
            } catch (e) {
              setError("QR-код не содержит валидный JSON");
            }
          }
        } catch (e) {
          //   console.error('Ошибка при сканировании QR-кода:', e);
        }
      }
    };

    const stopCamera = () => {
      if (streamRef.current) {
        streamRef.current.getTracks().forEach((track) => track.stop());
      }
    };

    startScanner();

    return () => {
      clearInterval(interval);
      stopCamera();
    };
  }, []);

  const sendDataToApi = async (data) => {
    setLoading(true);
    setError(null);

    try {
      const response = await axios.post(
        "http://localhost:5000/api/Payment/CreatePayment",
        data,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      console.log("Данные успешно отправлены:", response.data);
    } catch (err) {
      setError("Ошибка при отправке данных: " + err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ textAlign: "center" }}>
      <h1>QR-code scanner</h1>

      <div
        style={{
          position: "relative",
          margin: "0 auto",
          width: "100%",
          maxWidth: "500px",
        }}
      >
        <video
          ref={videoRef}
          autoPlay
          playsInline
          muted
          style={{
            width: "100%",
            border: "2px solid #333",
            borderRadius: "8px",
          }}
        />
        <canvas ref={canvasRef} style={{ display: "none" }} />
      </div>

      {loading && <p>Отправка данных...</p>}

      {/* {scanResult && (
        <div style={{ marginTop: "20px" }}>
          <h3>Сканированные данные:</h3>
          <pre>{JSON.stringify(scanResult, null, 2)}</pre>
        </div>
      )} */}

      {error && (
        <div style={{ color: "red", marginTop: "20px" }}>
          <p>{error}</p>
        </div>
      )}
    </div>
  );
};

export default QRScanner;

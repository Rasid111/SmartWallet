import { BrowserRouter, Route, Routes } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import Dashboard from "./pages/Dashboard";
import QRScanner from "./pages/QrScanner";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/dashboard" element={<Dashboard></Dashboard>} />
        <Route path="/qrscanner" element={<QRScanner></QRScanner>} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

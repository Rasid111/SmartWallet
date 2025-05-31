import { BrowserRouter, Route, Routes } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import Dashboard from "./pages/Dashboard";
import MyChart from "./components/Chart";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/dashboard" element={<Dashboard></Dashboard>} />
        <Route path="/chart" element={<MyChart></MyChart>} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

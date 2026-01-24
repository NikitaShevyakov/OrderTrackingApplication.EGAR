import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Container, Typography } from "@mui/material";

import OrdersPage from "./pages/OrdersPage/OrdersPage";
import OrderPage from './pages/OrderPage/OrderPage';

export const App = () => (
    <Router>
      <div className="app">
        <Container>
        <header className="app-header">
          <div className="container">
            <Typography variant="h4" gutterBottom sx={{ mt: 4 }}>
              📦 Система управления заказами
            </Typography>
          </div>
        </header>
        
        <main className="app-main">
          <div className="container">
            <Routes>
              <Route path="/" element={<OrdersPage />} />
              <Route path="/order/:id" element={<OrderPage />} />
              {/* <Route path="*" element={<Navigate to="/" replace />} /> */}
            </Routes>
          </div>
        </main>

        <footer className="app-footer">
          <div className="container">
            <p>© 2026 Система управления заказами. Все права защищены.</p>
          </div>
        </footer>
        </Container>
      </div>
    </Router>
)

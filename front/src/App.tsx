import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { Container, Typography, Box } from "@mui/material";

import OrdersPage from "./pages/OrdersPage/OrdersPage";

export const App = () => (
    <Router>
      <div className="app">
        <Container>
        {/* Общий header для всего приложения */}
        <header className="app-header">
          <div className="container">
            <Typography variant="h4" gutterBottom sx={{ mt: 4 }}>
              📦 Система управления заказами
            </Typography>
            {/* <nav className="main-nav">
              <Button variant="contained">Contained</Button>
              <a href="/" className="nav-link">Главная</a>
              <a href="/create-order" className="nav-link">Создать заказ</a>
            </nav> */}
          </div>
        </header>

        {/* Основное содержимое */}
        <main className="app-main">
          <div className="container">
            <Routes>
              <Route path="/" element={<OrdersPage />} />
              {/* <Route path="/create-order" element={<CreateOrderPage />} /> */}
              {/* <Route path="/order/:id" element={<OrderPage />} /> */}
              {/* Редирект для несуществующих маршрутов */}
              {/* <Route path="*" element={<Navigate to="/" replace />} /> */}
            </Routes>
          </div>
        </main>

        {/* Footer приложения */}
        <footer className="app-footer">
          <div className="container">
            <p>© 2026 Система управления заказами. Все права защищены.</p>
          </div>
        </footer>
        </Container>
      </div>
    </Router>
)

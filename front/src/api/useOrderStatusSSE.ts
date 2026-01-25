import { useEffect, useState } from "react";
import { OrderStatusChangedEvent } from "../types/order";

export function useOrderStatusSSE(orderId: number | null) {
  const [event, setEvent] = useState<OrderStatusChangedEvent | null>(null);
  const BASE_URL = import.meta.env.VITE_API_OTA_URL;

  useEffect(() => {
    if (!orderId) return;

    const url = `${BASE_URL}/orders/${orderId}/stream`;
    const es = new EventSource(url);

    es.onmessage = (msg) => {
      try {
        const data = JSON.parse(msg.data);
        setEvent(data);
      } catch (e) {
        console.error("SSE parse error", e);
      }
    };

    es.onerror = (err) => {
      console.error("SSE error", err);
      es.close();
    };

    return () => es.close();
  }, [orderId]);

  return event;
}
import { useEffect, useState, useCallback } from "react";

export type AlertState = {
  show: boolean;
  severity: "success" | "error";
  text: string;
};

export default function useAutoDismissAlert(initialTimeout = 5000) {
  const [alert, setAlert] = useState<AlertState>({ show: false, severity: "success", text: "" });
  const showAlert = useCallback((severity: "success" | "error", text: string) => {
    setAlert({ show: true, severity, text });
  }, []);
  const hideAlert = useCallback(() => setAlert(prev => ({ ...prev, show: false })), []);

  useEffect(() => {
    if (!alert.show) return;
    const t = window.setTimeout(() => setAlert(prev => ({ ...prev, show: false })), initialTimeout);
    return () => window.clearTimeout(t);
  }, [alert.show, initialTimeout]);

  return { alert, showAlert, hideAlert, setAlert } as const;
}
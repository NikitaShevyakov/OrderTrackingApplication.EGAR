export const formatDateTime = (dateString: string, withSeconds: boolean = false) => {
  const date = new Date(dateString);

  const datePart = date.toLocaleDateString("ru-RU", {
    day: "2-digit",
    month: "2-digit",
    year: "numeric",
  });

  const timePart = date.toLocaleTimeString("ru-RU", {
    hour: "2-digit",
    minute: "2-digit",
    second: withSeconds ? "2-digit" : undefined,
    hour12: false,
  });

  return `${datePart} ${timePart}`;
};

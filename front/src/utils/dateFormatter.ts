export const formatDateTime = (dateString: string) => {
  const date = new Date(dateString);

  const datePart = date.toLocaleDateString('ru-RU', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric'
  });

  const timePart = date.toLocaleTimeString('ru-RU', {
    hour: '2-digit',
    minute: '2-digit',
    hour12: false // 24-hours format
  });

  return `${datePart} ${timePart}`;
};
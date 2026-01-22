export function buildQueryParams(filters) {
  const params = new URLSearchParams();

  Object.entries(filters).forEach(([key, value]) => {
    if (value !== null && value !== "" && value !== undefined) {
      params.append(key, value);
    }
  });

  return params.toString();
}
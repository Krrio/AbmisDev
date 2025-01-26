const API_URL = "http://localhost:5185/api";

export const apiRequest = async (
  endpoint: string,
  method: string,
  body?: any
) => {
  const headers: HeadersInit = {
    "Content-Type": "application/json",
  };

  if (localStorage.getItem("token")) {
    headers["Authorization"] = `Bearer ${localStorage.getItem("token")}`;
  }

  const response = await fetch(`${API_URL}${endpoint}`, {
    method,
    headers,
    body: body ? JSON.stringify(body) : undefined,
  });

  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.message || "Coś poszło nie tak!");
  }

  return response.json();
};


export const logout = () => {
  localStorage.removeItem("token");
};

// test alertu świadczącego o problemie z wylogowywnaiem 

// export const logout = async (): Promise<void> => {
//   // Symulacja błędu
//   throw new Error("Failed to log out due to a server issue.");
// };

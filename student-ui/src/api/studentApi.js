import axios from "axios";

const BASE_URL = "https://localhost:7234/api/Student";

export const getAllStudents = async () => {
  const response = await axios.get(`${BASE_URL}/All`, {
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
  });
  return response.data;
};

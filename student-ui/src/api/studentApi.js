import axios from "axios";

const BASE_URL = "https://localhost:7234/api/testingendpoint";

export const getAllStudents = async () => {
  const response = await axios.get(`${BASE_URL}`, {
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
  });
  return response.data;
};

import axios from "axios";
import { VITE_API_WARM } from "../utils/apiUtils";

export const WarmApi = () => {
  axios.get(`${VITE_API_WARM}`);
  return <></>;
};

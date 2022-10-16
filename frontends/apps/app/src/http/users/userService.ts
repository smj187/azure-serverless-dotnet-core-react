import { HttpClient } from "../http";

export interface AccountTier {
  value: number;
  description: string;
}

export const getAccountTiers = async () => {
  const response = await HttpClient.get<AccountTier[]>("/api/v1/users/list-tiers");
  return response.data;
};

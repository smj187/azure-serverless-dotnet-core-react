import { HttpClient } from "../base/baseHttpClient"

export interface AccountTier {
  value: number
  description: string
}

export interface Entry {
  type: string
  value: string
  credits: number
  time: Date
}

export interface CreditHistory {
  date: Date
  entries: Entry[]
}

export interface User {
  b2cObjectId: string
  accountTier: AccountTier
  historyStartDate: Date
  historyResetDate: Date
  creditHistory: CreditHistory[]
  totalAvailableCredits: number
  remainingAvailableCredits: number
  lastActive: Date
  id: string
  createdAt: Date
  modifiedAt?: Date | null
}

interface PatchUserAccountTierRequest {
  userId: string
  value: number
}

export const getAccountTiers = async () => {
  const response = await HttpClient.get<AccountTier[]>(
    "/id/api/v1/users/list-tiers"
  )
  return response.data
}

export const getUsers = async () => {
  const response = await HttpClient.get<Array<User>>(
    "/id/api/v1/users/list-users"
  )

  return response.data
}

export const findUser = async (userId: string) => {
  const r = await HttpClient.get<User>(`/id/api/v1/users/find/${userId}`)
  return r.data
}

export const patchUserAccountTier = async (
  userId: string,
  newAccountTierValue: number
) => {
  const r = await HttpClient.patch<User>(
    "/id/api/v1/users/account-tier",
    JSON.stringify({
      userId,
      value: newAccountTierValue
    } as PatchUserAccountTierRequest)
  )

  return r.data
}

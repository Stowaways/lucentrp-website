import { fetchFromServer } from "../utils/http";

/**
 * Create a user account.
 * 
 * @param {*} account The account information.
 * @returns A promise to the request's response.
 */
export const create = async (account) => {
    return await fetchFromServer('/api/v1/User/Create', 'POST', account);
}

/**
 * Login to the user account.
 * 
 * @param {*} account The account information.
 * @returns A promise to the request's response.
 */
export const login = async (account) => {
    return await fetchFromServer('/api/v1/User/Create', 'POST', account);
}

/**
 * Logout of the user account.
 * 
 * @returns A promise to the request's response.
 */
export const logout = async () => {
    return await fetchFromServer('/api/v1/User/Logout', 'POST');
}

/**
 * Check if the sender is authentication.
 * 
 * @returns A promise to the requests's response.
 */
export const checkAuthentication = async () => {
    return await fetchFromServer('/api/v1/User/CheckAuthentication', 'POST');
}
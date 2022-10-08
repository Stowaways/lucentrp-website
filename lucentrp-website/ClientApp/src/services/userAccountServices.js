import { fetchFromServer } from "../utils/http";

/**
 * Create a user account.
 * 
 * @param {object} account The account information.
 * @returns A promise to the request's response.
 */
export const create = async (account) => fetchFromServer('/api/v1/User/Create', 'POST', false, account);

/**
 * Login to the user account.
 * 
 * @param {object} account The account information.
 * @returns {object} A promise to the request's response.
 */
export const login = async (account) => {
    const response = await fetchFromServer('/api/v1/User/Login', 'POST', false, account);

    // If the login attempt was successful.
    if (response.status === 200) {
        const body = response.json();
        window.localStorage.setItem("csrfToken", body.csrfToken)
    }

    return response;
};

/**
 * Logout of the user account.
 * 
 * @returns {object} A promise to the request's response.
 */
export const logout = async () => fetchFromServer('/api/v1/User/Logout', 'POST', false);

/**
 * Check if the sender is authentication.
 * 
 * @returns {object} A promise to the requests's response.
 */
export const checkAuthentication = async () => fetchFromServer('/api/v1/User/CheckAuthentication', 'POST', false);
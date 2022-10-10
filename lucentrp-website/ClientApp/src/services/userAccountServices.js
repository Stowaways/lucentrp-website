import { fetchFromServer } from "../utils/http";

/**
 * Create a user account.
 * 
 * @param {object} account The account information.
 * @returns A promise to the request's response.
 */
export const create = async (account) => fetchFromServer('/api/v1/User/Create', 'POST', false, account);

/**
 * Get information about the requesting account.
 * 
 * @returns A promise to the request's response.
 */
export const getSelf = async () => fetchFromServer('/api/v1/User/GetSelf', 'GET', true);

/**
 * Get a user by their id.
 * 
 * @param {number} id The id of the user to get. 
 * @returns A promise to the request's response.
 */
export const getByID = async (id) => fetchFromServer(`/api/v1/User/GetByID/${id}`, 'GET', true);

/**
 * Get as user by their username.
 * 
 * @param {string} username The username of the user to get.
 * @returns A promise to the request's response.
 */
export const getByUsername = async (username) => fetchFromServer(`/api/v1/User/GetByUsername/${username}`, 'GET', false);

/**
 * Get as user by their email address.
 * 
 * @param {string} email The email address of the user to get.
 * @returns A promise to the request's response.
 */
export const getByEmail = async (email) => fetchFromServer(`/api/v1/User/GetByEmail/${email}`, 'GET', false);

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
        const body = await response.json();
        window.localStorage.setItem("csrfToken", body.csrfToken);
        window.localStorage.setItem("account", { ...body.accountData, isLoggedIn: true});
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

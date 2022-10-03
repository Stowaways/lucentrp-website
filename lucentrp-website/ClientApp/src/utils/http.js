/**
 * Exported modules.
 */
module.exports = {
    /**
     * Fetch data from the server.
     * 
     * @param {*} url The url of the resource to fetch.
     * @param {*} method The request method.
     * @param {*} body The body of the request.
     * @returns A promise to the response.
     */
    fetchFromServer: async (url, method, body) => {
        return await fetch(url, {
            method: method,
            mode: 'no-cors',
            credentials: 'include',
            redirect: 'follow',
            referrerPolicy: 'no-referrer',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        }).json();
    }
}
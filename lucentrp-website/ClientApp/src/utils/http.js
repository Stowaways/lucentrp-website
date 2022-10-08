/**
 * Fetch a resource from the server.
 * 
 * @param {string} url The url of the resource to fetch.
 * @param {string} method The HTTP method to use.
 * @param {boolean} antiCsrf If anti cross-site request forgery measures should be used.
 * @param {object} body The body of the request.
 * @returns {object} A promise to the response.
 */
export async function fetchFromServer(url, method, antiCsrf, body) {
    let headers = {
        'Content-Type': 'application/json',
    }

    if (antiCsrf)
        headers.CsrfToken = window.localStorage.getItem('csrfToken');

    return fetch(url, {
        method: method,
        mode: 'same-origin',
        redirect: 'follow',
        referrerPolicy: 'strict-origin-when-cross-origin',
        headers: headers,
        body: JSON.stringify(body)
    });
}
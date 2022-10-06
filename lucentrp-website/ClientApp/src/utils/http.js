/**
 * Fetch a resource from the server.
 * 
 * @param {*} url The url of the resource to fetch.
 * @param {*} method The HTTP method to use.
 * @param {*} body The body of the request.
 * @returns A promise to the response.
 */
export async function fetchFromServer(url, method, body) {
    return await fetch(url, {
        method: method,
        mode: 'same-origin',
        redirect: 'follow',
        referrerPolicy: 'strict-origin-when-cross-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    });
}
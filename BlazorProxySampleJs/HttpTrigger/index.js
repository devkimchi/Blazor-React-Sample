const axios = require('axios');

module.exports = async function (context, req) {
    let baseUri = process.env.API__BASE_URI;
    let endpoint = process.env.API__ENDPOINT
    let authKey = process.env.API__AUTH_KEY === undefined ? '' : process.env.API__AUTH_KEY;
    let count = req.query.count === undefined ? 0 : req.query.count;
    let requestUri = baseUri + endpoint + '?count=' + count + '&code=' + authKey;

    let response = await axios.get(requestUri);
    let result = { "text": response.data.message };

    context.res = {
        // status: 200, /* Defaults to 200 */
        body: result
    };
}
import axios from 'axios';
import * as React from 'react';
import ReactDOM from 'react-dom';

export async function getHello(requestUri) {
    let response = await axios.get(requestUri);
    let message = response.data.text;

    const Hello = () => React.createElement(
        'div',
        null,
        'Hello, ' + message
    );

    ReactDOM.render(Hello(), document.getElementById('reactHello'));
}

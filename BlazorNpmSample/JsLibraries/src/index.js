import { renderProgressBar } from './progressbar';
import { getHello } from './apirequest';

export function RenderProgressBar(count) {
    return renderProgressBar(count);
}

export async function GetHello(requestUri) {
    return await getHello(requestUri);
}

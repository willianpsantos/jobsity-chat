import SocketIOSocketClient from "./SocketIOSocketClient";
import { settings as appSettings } from "../settings";

export default class SocketClientFactory
{
    static get clientsTypes() {
        return [
            SocketIOSocketClient
        ];
    }

    static get instance() {
        return SocketClientFactory._instance;
    }

    static set instance(value) {
        SocketClientFactory._instance =value;
    }

    static create(settings) {        
        let clientType = SocketClientFactory.clientsTypes.find(t => t.vendor == appSettings.socketVendor);

        if(!clientType) {
            return undefined;
        }

        if(SocketClientFactory.instance && SocketClientFactory.instance instanceof clientType)
            return SocketClientFactory.instance;

        let instance = new clientType(settings);
        SocketClientFactory.instance = instance;

        return instance;
    }
}
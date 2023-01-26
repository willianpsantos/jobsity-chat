export default class SocketClient
{
    static get vendor() {
        return "";
    }

    get opened() {
        return this._opened;
    }

    set opened(value) {
        this._opened = value;
    }

    constructor(settings) {
        this._settings = settings;
        this._socket = undefined;
        this._eventReplaceRegex = /\{{1}[\w]+\}{1}/g;
        this._opened = false;
        this._addedEvents = [];
    }

    open(callback) {
        
    }

    addUserMessageReceivedEventListener(userId, callback) {
        
    }

    addChatRoomMessageReceivedEventListener(chatRoomId, callback) {

    }

    emitEvent(eventName, data) {
        
    }
}
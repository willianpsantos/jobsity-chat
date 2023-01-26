import { io } from "socket.io-client";
import SocketClient from "./SocketClient";

export default class SocketIOSocketClient extends SocketClient
{
    static get vendor() {
        return "socket.io-client";
    }

    open(callback) {
        if (this.opened && this._io) {
            callback && callback({ client: this, socket: this._io });
            return;
        }
        
        const { serverUrl, serverPath, reconnection } = this._settings;
        const self = this;

        self._io = io(serverUrl, {
            secure: false,
            autoConnect: false,
            path: serverPath,
            reconnection
        });

        self._io.on('connect', function(socket) {        
            self.opened = true;
            callback && callback({ client: self, socket: self._io });
        });
        
        self._io.connect(serverUrl);

        return self;
    }

    addUserMessageReceivedEventListener(userId, callback) {
        if (!this._io)
            throw new Error('Connection is closed. Call open method.');

        const { sendMessageSingleUserEventName } = this._settings.events;
        const eventName = sendMessageSingleUserEventName.replace(this._eventReplaceRegex, userId);

        if (this._addedEvents.includes(eventName)) {
            return;
        }

        this._addedEvents.push(eventName);
        this._io.on(eventName, callback);
    }

    addChatRoomMessageReceivedEventListener(chatRoomId, callback) {
        if (!this._io)
            throw new Error('Connection is closed. Call open method.');

        const { sendMessageChatRoomEventName } = this._settings.events;
        const eventName = sendMessageChatRoomEventName.replace(this._eventReplaceRegex, chatRoomId);

        if (this._addedEvents.includes(eventName)) {
            return;
        }

        this._addedEvents.push(eventName);
        this._io.on(eventName, callback);
    }

    emitEvent(eventName, data) {
        if (!this._io)
            throw new Error('Connection is closed. Call open method.');

        this._io.emit(eventName, data);
    }
}
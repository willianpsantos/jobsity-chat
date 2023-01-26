import '../css/messages.css';

import React, { useEffect, useState } from "react";
import { MessagesList } from "./MessagesList";
import { MessageInput } from "./MessageInput";
import SocketClientFactory from "../socket/SocketClientFactory"; 
import SettingsService from "../services/SettingsService";
import AuthService from "../services/AuthService";
import { settings } from "../settings";
import MessagesStore from '../stores/MessagesStore';
import MessageService from '../services/MessageService';

export function Messages(props) {    
    const [ receiverId, setReceiverId ] = useState();
    const [ receiverType, setReceiverType ] = useState();
    const [ messages, setMessages ] = useState([]);
    const [ lastMessageId, setLastMessageId ] = useState();

    const initializeSocket = (socketSettings) => {        
        const authService = new AuthService(settings.apiUrl);

        if(!authService.isAuthenticated()) {
            return;
        }

        const socketClient = SocketClientFactory.create(socketSettings);        

        socketClient.open(({ client, socket }) => {            
            const authKey = authService.getAuthKey();

            client.addUserMessageReceivedEventListener(authKey, (e) => {
                MessagesStore.open().then(async (openResult) => {
                    await MessagesStore.saveMessage(e);
                    const list = await MessagesStore.getMessages(e);

                    setMessages([]);
                    setMessages(list.data);
                });
            });

            if( (receiverId && receiverType) || (props.receiverId && props.receiverType)) {
                client.addChatRoomMessageReceivedEventListener(receiverId ? receiverId : props.receiverId, (e) => {                    
                    setReceiverId('');
                    setReceiverType(0);

                    if(e.stockQuote) {
                        alert(e.message);
                    }
                });
            }
        });
    };

    useEffect(() => {
        if (!props.receiverId || !props.receiverType) {
            setReceiverId(null);
            setReceiverType(null);
            return;
        }

        setReceiverId(props.receiverId);
        setReceiverType(props.receiverType);

        const settingsService = new SettingsService(settings.apiUrl);

        settingsService
            .getSocketSettings()
            .then(response => initializeSocket(response));

        MessagesStore.open().then(async (result) => {
            const messageService = new MessageService(settings.apiUrl);
            const messages = await messageService.getLastestMessages(props.receiverId, props.receiverType);
        
            setMessages([]);
            setMessages(messages?.data);

            await MessagesStore.open();

            for(let msg of messages?.data) {
                await MessagesStore.saveMessage(msg);
            }
        });
    }, [props.receiverId]);
    
    useEffect(() => {
        if (!props.receiverId || !props.receiverType) {
            setReceiverId(null);
            setReceiverType(null);
            return;
        }

        setReceiverId(props.receiverId);
        setReceiverType(props.receiverType);

        const settingsService = new SettingsService(settings.apiUrl);

        settingsService
            .getSocketSettings()
            .then(response => initializeSocket(response));

        MessagesStore.open().then(async (result) => {
            const messageService = new MessageService(settings.apiUrl);
            const messages = await messageService.getLastestMessages(props.receiverId, props.receiverType);
        
            setMessages([]);
            setMessages(messages?.data);

            await MessagesStore.open();

            for(let msg of messages?.data) {
                await MessagesStore.saveMessage(msg);
            }
        });
    }, [receiverId]);

    return (
        <div className="messages-container">
            <MessagesList 
                key={0} 
                messages={messages}>                    
            </MessagesList>

            <MessageInput 
                key={1}
                receiverId={receiverId ?? props.receiverId}
                receiverType={receiverType ?? props.receiverType}>                    
            </MessageInput>
        </div>
    );
}
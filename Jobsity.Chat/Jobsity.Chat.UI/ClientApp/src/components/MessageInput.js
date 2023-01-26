import React, { useState, useEffect } from "react";
import MessageService from "../services/MessageService";
import AuthService from "../services/AuthService";
import MessageReceiverType from "../constants/MessageReceiverType";
import { settings } from "../settings";

export function MessageInput(props) {
    const messageService = new MessageService(settings.apiUrl);
    const authService = new AuthService(settings.apiUrl);

    const [message, setMessage] = useState('');
    const [chatId, setChatId] = useState('');
    const [lastMessageId, setLastMessageId] = useState('');
    const [receiverId, setReceiverId] = useState();
    const [receiverType, setReceiverType] = useState();

    useEffect(() => {
        setReceiverId(props.receiverId);
        setReceiverType(props.receiverType);
    }, 
    [props.receiverId, props.receiverType]);

    const sendMessage = async (e) => {
        const senderUserId = authService.getAuthKey();
        const { chatUid, receiverUid, receiverTypecode } = e.target.parentElement.dataset;

        const response = await messageService.saveAndSend({ 
            chatId: chatUid,
            chatRoomId: receiverTypecode == MessageReceiverType.ChatRoom ? receiverUid : null,
            userId: receiverTypecode == MessageReceiverType.User ? receiverUid : null,
            senderUserId,
            message
        });

        setChatId(response.chatId);
        setReceiverId(receiverUid);
        setReceiverType(receiverTypecode);
        setLastMessageId(response.id);
        setMessage('');

        document.scrollingElement.scrollTo(0, document.scrollingElement.scrollHeight);
    }

    const onTextInputKeyPress = (e) => {
        if(e.keyCode == 13 || e.charCode == 13) {
            e.preventDefault();
            sendMessage(e);
        }
    }

    return (
        <div className="message-input" 
             style={{ gridArea: 'messageinput' }}
             data-chat-uid={chatId}
             data-message-uid={lastMessageId}
             data-receiver-uid={receiverId ?? props?.receiverId}
             data-receiver-typecode={receiverType ?? props?.receiverType}>

            <textarea 
                rows={3}
                placeholder="Type your message here and press ENTER or click SEND button"
                className="form-control"
                value={message}
                disabled={!receiverId && !props?.receiverId}
                onChange={(e) => setMessage(e.target.value)}
                onKeyUp={(e) => onTextInputKeyPress(e)}></textarea>

            <button 
                type="button" 
                className="btn btn-primary" 
                onClick={sendMessage}
                disabled={!receiverId && !props?.receiverId}>
                Send
            </button>
        </div>
    )
}
import React, { useState, useEffect } from "react";
import { MessageBaloom } from "./MessageBaloom";
import MessagesStore from "../stores/MessagesStore";

export function MessagesList(props) {    
    const [ messages, setMessages ] = useState([]);

    useEffect(() => {        
        setMessages([]);
        setMessages(props?.messages);
    }, [props.messages]);

    return (
        <div className="messages-list" style={{ gridArea: 'messageslist' }}>
            {
                messages?.reverse()?.map((m,i) => {
                    return <MessageBaloom key={i} message={m}></MessageBaloom>
                })
            }
        </div>
    )
}
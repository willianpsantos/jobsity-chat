import React, { useState, useEffect } from "react";
import AuthService from "../services/AuthService";
import { settings } from "../settings";

export function MessageBaloom(props) {    
    const [message, setMessage] = useState();

    useEffect(() => {
        const msg = props?.message;

        const sentAt =             
            msg?.createdAt
                ? new Date(msg.createdAt)
                : undefined;    

        let formatted = "";
    
        if (sentAt) {
            const formatter = new Intl.DateTimeFormat('en-US', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit', 
                minute: '2-digit'
            });

            formatted = formatter.format(sentAt);
        }

        msg.formattedSentAt = formatted;
    
        const authService = new AuthService(settings.apiUrl);
        const authKey = authService.getAuthKey();

        msg.mine = (msg?.createdBy === authKey);
        msg.senderName = msg?.createdByName;

        setMessage(msg);
    }, [props.message]);    

    return (
        <div className={"message-baloom-container"}>
            <div className={"message-baloom " + (message?.mine ? 'mine' : 'theirs')}>
                <b className="sender"> { message?.senderName } </b> <br/>
                <p className="text"> {message?.message} </p>
                <small className="timestamp"> {message?.formattedSentAt} </small> <br/>
            </div>
        </div>
    );
}
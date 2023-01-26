import React, { useState, useEffect } from "react";
import { ListGroup, ListGroupItem } from "reactstrap";
import RoomParticipantService from "../services/RoomParticipantService";
import { settings } from "../settings";

export function RoomParticipantsList(props) {

    const onParticipantClick = (e) => {
        e.preventDefault();
        const id = e.target.dataset.id; 
        const participant = props?.participants?.find(i => i.id == id);

        props.onParticipantClick && props.onParticipantClick(participant);
    }

    return (
        <ListGroup>
            {props.participants.map(r => 
                <ListGroupItem 
                    key={r.id} 
                    className="justify-content-between" 
                    tag="button"
                    data-id={r.id}
                    onClick={onParticipantClick}
                    active={r.userId == props?.selected}
                    action> 

                    {r.userName}                     
                </ListGroupItem>)}
        </ListGroup>
    );
}
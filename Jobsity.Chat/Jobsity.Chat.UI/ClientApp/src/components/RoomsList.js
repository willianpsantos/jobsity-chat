import React, { useState, useEffect } from "react";
import { ListGroup, ListGroupItem, Badge } from "reactstrap";
import RoomService from "../services/RoomService";
import { settings } from "../settings";

export function RoomsList(props) {
    const roomService = new RoomService(settings.apiUrl);
    const [ rooms, setRooms ] = useState([]);

    useEffect(() => {
        roomService
            .getAllRoomsOfLoggedUser()
            .then(response => setRooms(response));
    }, []);

    const onRoomClick = (e) => {
        e.preventDefault();
        const id = e.target.dataset.id; 
        const room = rooms.find(i => i.id == id);
        
        props?.onRoomClick && props?.onRoomClick(room);
    }

    return (
        <ListGroup>
            {rooms.map(r => 
                <ListGroupItem 
                    key={r.id} 
                    className="justify-content-between" 
                    tag="button"
                    data-id={r.id}
                    action
                    onClick={onRoomClick}
                    active={r.id == props?.selected}> 
                    {r.name} 
                </ListGroupItem>)}
        </ListGroup>
    );
}
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { RoomsList } from './RoomsList';
import { Messages } from './Messages';
import { RoomParticipantsList } from './RoomParticipantsList';
import { settings } from "../settings";

import AuthService from "../services/AuthService";
import RoomParticipantService from "../services/RoomParticipantService";
import MessageReceiverType from "../constants/MessageReceiverType";

export function Home(props) {    
    Home.displayName = props.name;

    const roomParticipantService = new RoomParticipantService(settings.apiUrl);   

    const [ participants, setParticipants ] = useState([]);    
    const [ selectedReceiverId, setSelectedReceiverId ] = useState('');
    const [ selectedReceiverType, setSelectedReceiverType ] = useState(MessageReceiverType.ChatRoom);

    const navigate = useNavigate();    

    useEffect(() => {
      const authService = new AuthService();
      
      if (!authService.isAuthenticated()) {
          navigate('/login');
      }
    }, []);

    const onRoomClick = async (room) => {
      const participants = await roomParticipantService.getRoomParticipants(room.id);

      setParticipants(participants);
      setSelectedReceiverId(room.id);
      setSelectedReceiverType(MessageReceiverType.ChatRoom);
    };    

    const onParticipantClick = async (participant) => {
      setSelectedReceiverId(participant.userId);
      setSelectedReceiverType(MessageReceiverType.User);
    }
  
    return (
        <div className='app-container'>
            <div style={{ gridArea: 'rooms' }}>
              <RoomsList onRoomClick={onRoomClick} selected={selectedReceiverId}>                
              </RoomsList>
            </div>

            <div style={{ gridArea: 'messages' }}>
              <Messages                 
                receiverId={selectedReceiverId}
                receiverType={selectedReceiverType}>                  
              </Messages>
            </div>

            <div style={{ gridArea: 'participants' }}>
              <RoomParticipantsList 
                participants={participants}
                onParticipantClick={onParticipantClick}
                selected={selectedReceiverId}>                  
              </RoomParticipantsList>
            </div>
        </div>
    );
}

import Service from './Service';

export default class RoomParticipantService extends Service
{
    getRoomParticipants(roomId) {
        return this.get({ url: `/chat/room/participant/${roomId}/room` });
    }
}
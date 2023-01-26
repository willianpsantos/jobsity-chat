import Service from "./Service";

export default class RoomService extends Service
{
    getAllRoomsOfLoggedUser() {          
        return this.get({ url: '/chat/room/user' });
    }
}